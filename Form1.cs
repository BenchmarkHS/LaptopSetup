using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
            try
            {
                string strCmdText;
                strCmdText = $"net user {txtUsername.Text} {txtPassword.Text} /add";
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", strCmdText);
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                //Process.Start("CMD.exe", strCmdText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex}");
            }
            finally
            {
                MessageBox.Show($"User: {txtUsername.Text} created!");
                txtPassword.Text = null;
                txtUsername.Text = null; 
            }
        }
    }
}
