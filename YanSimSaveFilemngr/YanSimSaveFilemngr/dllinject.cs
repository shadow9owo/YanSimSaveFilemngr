using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YanSimSaveFilemngr
{
    class dllinject
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [Flags]
        public enum MemoryProtection
        {
            ExecuteReadWrite = 0x40
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        public static bool InjectDll(string processName, string dllPath)
        {
            Process targetProcess = Process.GetProcessesByName(processName)[0];
            if (targetProcess == null)
            {
                Console.WriteLine($"Could not find process with name {processName}");
                return false;
            }

            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to open handle to process {processName}");
                return false;
            }

            IntPtr remoteMem = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)dllPath.Length, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            if (remoteMem == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to allocate memory in process {processName}");
                CloseHandle(hProcess);
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(dllPath);
            int bytesWritten;
            if (!WriteProcessMemory(hProcess, remoteMem, bytes, (uint)bytes.Length, out bytesWritten))
            {
                Console.WriteLine($"Failed to write DLL path to process {processName}");
                CloseHandle(hProcess);
                return false;
            }

            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (loadLibraryAddr == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to get address of LoadLibraryA function in process {processName}");
                CloseHandle(hProcess);
                return false;
            }

            IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, loadLibraryAddr, remoteMem, 0, IntPtr.Zero);
            if (hThread == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to create remote thread in process {processName}");
                CloseHandle(hProcess);
                return false;
            }

            WaitForSingleObject(hThread, 0xFFFFFFFF);

            CloseHandle(hThread);
            CloseHandle(hProcess);
            return true;
        }

    }
}
