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
    public partial class btnRestart : Form
    {
        public btnRestart()
        {
            InitializeComponent();
        }

        private void RunCommand(string strCmdText)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", strCmdText);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void RunCommandWithOutput(string strCmdText)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", strCmdText);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            if (string.IsNullOrWhiteSpace(output)) return;
            MessageBox.Show(output);
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
                    txtAdminPass.Text = null;
                }
            }
        }

        private void btnSetComputerName_Click(object sender, EventArgs e)
        {
            bool success = true;
            try
            {
                RunCommand($"/C wmic computersystem where name='{Environment.MachineName}' call rename name='{txtComputerName.Text}'");
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
                    MessageBox.Show($"Computer Renamed!");
                    txtComputerName.Text = null;
                }
            }
        }

        private void btnSetDomain_Click(object sender, EventArgs e)
        {
            bool success = true;
            try
            {
                RunCommandWithOutput($"/C wmic computersystem where name='{Environment.MachineName}' call joindomainorworkgroup fjoinoptions=3 name='corp.awsusa.com' username='AWS\\{txtSelfUsername.Text}' Password='{txtSelfPassword.Text}'");
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
                    MessageBox.Show($"Computer joined to domain!");
                    txtSelfPassword.Text = null;
                    txtSelfUsername.Text = null;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
