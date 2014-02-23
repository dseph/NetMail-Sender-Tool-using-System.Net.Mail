using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmEditContentType : Form
    {
        public string attContentType, origValue, cid;
        public bool isInline;

        /// <summary>
        /// form constructor
        /// </summary>
        /// <param name="origVal">this is the original value of the attachment content type</param>
        public frmEditContentType(string origVal)
        {
            InitializeComponent();
            origValue = origVal;
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            attContentType = NetMailSample.Common.FileUtilities.GetContentType(cboContentType.Text);
            if (cboInline.Checked)
            {
                isInline = true;
                cid = txtCid.Text;
            }
            this.Close();
        }

        /// <summary>
        /// if we cancel the form, we need to return the original value to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            attContentType = NetMailSample.Common.FileUtilities.GetContentType(origValue);
            this.Close();
        }

        private void cboInline_CheckedChanged(object sender, EventArgs e)
        {
            if (cboInline.Checked)
            {
                txtCid.Enabled = true;
            }
            else
            {
                txtCid.Enabled = false;
            }
        }
    }
}
