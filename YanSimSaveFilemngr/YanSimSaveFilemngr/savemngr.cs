﻿using Microsoft.Win32;
using System;
using System.IO;

namespace YanSimSaveFilemngr
{
    class savemngr
    {
        public static void SetInt(string key, int value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandereDev\YandereSimulator", key, value, RegistryValueKind.DWord);
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            object value = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandereDev\YandereSimulator", key, defaultValue);
            if (value != null && value is int intValue)
            {
                return intValue;
            }
            return defaultValue;
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
                                return $"{valueName}";
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
            }
            

            return null;
        }
        public static bool exportsavestoreg()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\YandereDev\YandereSimulator"))
                {
                    if (key != null)
                    {
                        using (StreamWriter writer = new StreamWriter("output.reg"))
                        {
                            writer.WriteLine("Windows Registry Editor Version 5.00");
                            writer.WriteLine();

                            writer.WriteLine($"[{@"SOFTWARE\YandereDev\YandereSimulator"}]");

                            foreach (string valueName in key.GetValueNames())
                            {
                                object value = key.GetValue(valueName);
                                string valueType = value.GetType().Name.ToLower();
                                string regLine = $"\"{valueName}\"={frmregval(valueType, value)}";
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
            catch
            {
                return false;
            }
        }

        static string frmregval(string valueType, object value)
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
    }
}