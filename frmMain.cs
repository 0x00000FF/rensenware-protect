using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
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

        private bool Check()
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

                            return true;
                        }
                    }
            }

            return false;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(IVFilePath))
                {
                    if (File.GetAttributes(IVFilePath) == FileAttributes.Hidden)
                        File.SetAttributes(IVFilePath, FileAttributes.Normal);

                    if (File.ReadAllBytes(IVFilePath).Length != 16)
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
                    if (File.GetAttributes(KeyFilePath) == FileAttributes.Hidden)
                        File.SetAttributes(KeyFilePath, FileAttributes.Normal);

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
            catch
            {
                MessageBox.Show("There's problem to create pseudo key/iv files.\nTry again with administrator privileges.");
            }
        }
    }
}
