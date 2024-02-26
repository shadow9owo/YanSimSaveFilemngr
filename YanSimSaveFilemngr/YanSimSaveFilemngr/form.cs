using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace YanSimSaveFilemngr
{
    public partial class form : Form
    {
        //true pacifist mode
        [DllImport("ntdll.dll")]
        private static extern uint NtRaiseHardError(int ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        [DllImport("ntdll.dll")]
        private static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        private static int defkills;
        private static bool truepacifistenabled = false;
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
            MessageBox.Show("this tool works by overwriting the values in the registry. (basically modifing the yandere simulator savefile)\n\nThis tool is pretty simple as it basically just takes an advantage of playerprefs being unsecure.\n(not encoded or encrypted thoe tbh theres no need for that)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_MasksBanned");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_MasksBanned"), 0);
            }
            else
            {
                MessageBox.Show("err masksbanned value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_NoJournalist");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_NoJournalist"), 1);
            }
            else
            {
                MessageBox.Show("err nojournalist value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_CanBefriendCouncil");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_CanBefriendCouncil"), 1);
            }
            else
            {
                MessageBox.Show("err canbefriendcouncil value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_HighSecurity");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_HighSecurity"), 0);
            }
            else
            {
                MessageBox.Show("err highsecurity value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0x40 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        //infinite money
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                DialogResult dr = MessageBox.Show("are you sure you want to uncheck this checkbox?\nas if youl uncheck this checkbox your money value will be reseted to the default value", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    checkBox1.Checked = true;
                }else
                {
                    string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money");
                    if (output != null)
                    {
                        try
                        {
                            byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x40 };
                            savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money"), binaryData);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }

                    }
                }
            }
            else 
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money");
                if (output != null)
                {
                    try
                    {
                        byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x97, 0x60 };
                        savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money"), binaryData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0xC0 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != " ")
            {
                Int32.TryParse(textBox1.Text, out int a);
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_BonusStudyPoints_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_BonusStudyPoints_"), a);
                }
                else
                {
                    MessageBox.Show("err Bonusstudypoints value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                timer1.Enabled = false;
                truepacifistenabled = false;
            }else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to enable true pacifist?\n\nenabling true pacifist will make it so if you kill someone\n(according to the playerprefs)\nthen your pc will bluescreen\n\nare you really sure you want to turn on truepacifist?");

                if (dr == DialogResult.Yes)
                {
                    timer1.Enabled = true;
                    truepacifistenabled = true;
                    defkills = savemngr.GetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Kills"));
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (savemngr.GetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Kills")) > defkills)
            {
                var i = 0xC0000022;
                bool t1;
                uint t2;
                RtlAdjustPrivilege(19, true, false, out t1);
                NtRaiseHardError((int)i, 0, 0, IntPtr.Zero, 6, out t2);
            }
        }
    }
}