using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YanSimSaveFilemngr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("A save file int is the number of the save file youre using\n\n\n(the first save file slot on the top is 1 the last one on the bottom is 3)","testtitle",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure this is the correct save file slot?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                form form = new form(1);
                form.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure this is the correct save file slot?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                form form = new form(2);
                form.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure this is the correct save file slot?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                form form = new form(3);
                form.Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "ver : " + Application.ProductVersion;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (savemngr.ExportToRegistry("output.reg"))
            {
                MessageBox.Show("save file was exported successfully into the current directory","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("an error accured while trying to export the save files", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
