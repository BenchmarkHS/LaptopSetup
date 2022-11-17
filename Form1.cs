using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.DirectoryServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Xml.Linq;

namespace LaptopSetup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSetUser_Click(object sender, EventArgs e)
        {
            bool success = true;
            try
            {
                RunCommand($"/C net user {txtUsername.Text} {txtPassword.Text} /add");
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show($"ERROR: {ex}");
            }
            finally
            {
                if (success)
                {
                    MessageBox.Show($"User: {txtUsername.Text} created!");
                    txtPassword.Text = null;
                    txtUsername.Text = null;
                }
            }
        }

        private void btnSetAdminPass_Click(object sender, EventArgs e)
        {
            bool success = true;
            try
            {
                RunCommand($"/C net user Administrator {txtAdminPass.Text}");
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show($"ERROR: {ex}");
            }
            finally
            {
                if (success)
                {
                    MessageBox.Show($"Admin Password Set!");
                    txtPassword.Text = null;
                    txtUsername.Text = null;
                }
            }
        }

        private void RunCommand(string strCmdText)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", strCmdText);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
