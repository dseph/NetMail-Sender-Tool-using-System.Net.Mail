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
        public string origContentType, origCid;
        public bool isInline;

        /// <summary>
        /// form constructor
        /// </summary>
        /// <param name="contentType">this is the original value of the attachment content type</param>
        /// <param name="altView">this determines if this form was opened from the main form or the altview</param>
        public frmEditContentType(string contentType, string cid)
        {
            InitializeComponent();
            origContentType = contentType;
            origCid = cid;
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
            newCid = origCid;
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
