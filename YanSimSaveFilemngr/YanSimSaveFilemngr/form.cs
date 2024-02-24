using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace YanSimSaveFilemngr
{
    public partial class form : Form
    {
        private static int savei;
        public form(int saveint)
        {
            savei = saveint;
            InitializeComponent();
        }

        private void form_Load(object sender, EventArgs e)
        {
            label2.Text = "current save slot int: " + savei.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            credits credits = new credits();
            credits.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("this tool works by overwriting the values in the registry. (basically modifing the yandere simulator savefile)\n\nThis tool is pretty simple as it basically just takes an advantage of playerprefs being unsecure.\n(not encoded or encrypted thoe tbh theres no need for that)","INFO",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox1.Text, out int a);
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "Money");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "Money"), a);
            }else
            {
                MessageBox.Show("err money value not found or is null","err",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox2.Text, out int a);
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "SchoolAtmosphere");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "SchoolAtmosphere"), a);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox3.Text, out int a);
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "BonusStudyPoints");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "BonusStudyPoints"), a);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "MasksBanned");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "MasksBanned"), 0);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "NoJournalist");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "NoJournalist"), 1);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "CanBefriendCouncil");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "CanBefriendCouncil"), 1);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "HighSecurity");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "HighSecurity"), 0);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox4.Text, out int a);
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "Reputation");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "Reputation"), a);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox5.Text, out int a);
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "SpeedBonus");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "SpeedBonus"), a);
            }
            else
            {
                MessageBox.Show("err SchoolAtmosphere value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
