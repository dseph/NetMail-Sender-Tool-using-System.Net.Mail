using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace NetMailSample.Forms
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            linkLabel1.Text = "NetMail Sender Website";
            linkLabel1.Links.Add(0, 22, "http://netmailsender.codeplex.com/");
            this.lblVersion.Text = Application.ProductVersion.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
