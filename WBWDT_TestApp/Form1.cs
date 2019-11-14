using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WBWDT_TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowserWithDelayedTrigger1.DocumentCompletedFinally += WebBrowserWithDelayedTrigger1_DocumentCompletedFinally;
            webBrowserWithDelayedTrigger1.Navigate("http://192.168.0.201");
        }

        private void WebBrowserWithDelayedTrigger1_DocumentCompletedFinally()
        {
            System.Diagnostics.Debug.WriteLine("==================== Nixon");
        }
    }
}
