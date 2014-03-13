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
        public string newContentType, newCid;
        public string origContentType;
        public bool isInline;

        /// <summary>
        /// form constructor
        /// </summary>
        /// <param name="contentType">this is the original value of the attachment content type</param>
        public frmEditContentType(string contentType)
        {
            InitializeComponent();
            origContentType = contentType;
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            newContentType = NetMailSample.Common.FileUtilities.GetContentType(cboContentType.Text);
            if (cboInline.Checked)
            {
                isInline = true;
                newCid = txtCid.Text;
            }
            else
            {
                isInline = false;
                newCid = txtCid.Text;
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
            newContentType = NetMailSample.Common.FileUtilities.GetContentType(origContentType);
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
