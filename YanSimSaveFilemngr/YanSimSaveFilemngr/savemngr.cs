using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

public class savemngr
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SetWindowText(IntPtr hWnd, string lpString);

    public static void injecttxt(int processId, string newTitle)
    {
        Process process = Process.GetProcessById(processId);

        if (process != null && process.MainWindowHandle != IntPtr.Zero)
        {
            SetWindowText(process.MainWindowHandle, newTitle);
        }
        else
        {
            Console.WriteLine("Process not found or does not have a main window.");
        }
    }

    public static int GetInt(string key)
    {
        object value = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandereDev\YandereSimulator", key, null);
        if (value != null && value is int intValue)
        {
            return intValue;
        }

        return 0;
    }

    public static void SetInt(string key, int value)
    {
        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandereDev\YandereSimulator", key, value, RegistryValueKind.DWord);
    }

    public static void SetBinary(string key, byte[] binaryData)
    {
        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandereDev\YandereSimulator", key, binaryData, RegistryValueKind.Binary);
    }


    public static bool ExportToRegistry(string filePath)
    {
        try
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\YandereDev\YandereSimulator"))
            {
                if (key != null)
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("Windows Registry Editor Version 5.00");
                        writer.WriteLine();
                        writer.WriteLine($"[{@"SOFTWARE\YandereDev\YandereSimulator"}]");

                        foreach (string valueName in key.GetValueNames())
                        {
                            object value = key.GetValue(valueName);
                            string valueType = value.GetType().Name.ToLower();
                            string regLine = $"\"{valueName}\"={FromRegistryValue(valueType, value)}";
                            writer.WriteLine(regLine);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting PlayerPrefs to .reg file: {ex.Message}");
            return false;
        }
    }

    private static string FromRegistryValue(string valueType, object value)
    {
        if (valueType == "string")
        {
            return $"\"{value}\"";
        }
        else if (valueType == "byte[]")
        {
            byte[] bytes = (byte[])value;
            return $"hex:{BitConverter.ToString(bytes).Replace("-", "")}";
        }
        else
        {
            return $"{value}";
        }
    }

    public static string FindKeysWithCustomStrings(string searchString1, string searchString2)
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\YandereDev\YandereSimulator"))
        {
            if (key != null)
            {
                foreach (string valueName in key.GetValueNames())
                {
                    string valueNameLower = valueName.ToLower();

                    if (valueNameLower.Contains(searchString1.ToLower()) && valueNameLower.Contains(searchString2.ToLower()))
                    {
                        return valueName;
                    }
                }
            }
            else
            {
                Console.WriteLine("Registry key not found or cannot be accessed.");
            }
        }

        return null;
    }
}
