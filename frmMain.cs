using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rensenware_protection
{
    public partial class frmMain : Form
    {
        internal static readonly string KeyFilePath =
            Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop
                ) + @"\randomkey.bin";
        internal static readonly string IVFilePath =
            Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop
                ) + @"\randomiv.bin";


        public frmMain()
        {
            InitializeComponent();

            Check();
        }

        private void Check()
        {
            if (File.Exists(IVFilePath))
            {
                var IVTest = File.ReadAllBytes(IVFilePath);
                if (IVTest.Length == 16)
                    if (File.Exists(KeyFilePath))
                    {
                        var KeyTest = File.ReadAllBytes(KeyFilePath);
                        if (KeyTest.Length == 32)
                        {
                            Status.ForeColor = Color.LimeGreen;
                            Status.Text = "SAFE for Original Build";
                        }
                    }
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if(File.Exists(IVFilePath))
            {
                if(File.ReadAllBytes(IVFilePath).Length != 16)
                {
                    File.Delete(IVFilePath);
                }

                var _buffer = new byte[16];

                var randomKey = new RNGCryptoServiceProvider();
                randomKey.GetBytes(_buffer);
                File.WriteAllBytes(IVFilePath, _buffer);
            }
            else
            {
                var _buffer = new byte[16];

                var randomKey = new RNGCryptoServiceProvider();
                randomKey.GetBytes(_buffer);
                File.WriteAllBytes(IVFilePath, _buffer);
            }

            if (File.Exists(KeyFilePath))
            {
                if (File.ReadAllBytes(KeyFilePath).Length != 32)
                {
                    File.Delete(KeyFilePath);
                }

                var _buffer = new byte[32];

                var randomKey = new RNGCryptoServiceProvider();
                randomKey.GetBytes(_buffer);
                File.WriteAllBytes(KeyFilePath, _buffer);
            }
            else
            {
                var _buffer = new byte[32];

                var randomKey = new RNGCryptoServiceProvider();
                randomKey.GetBytes(_buffer);
                File.WriteAllBytes(KeyFilePath, _buffer);
            }

            File.SetAttributes(IVFilePath, FileAttributes.Hidden);
            File.SetAttributes(KeyFilePath, FileAttributes.Hidden);

            Check();
        }
    }
}
