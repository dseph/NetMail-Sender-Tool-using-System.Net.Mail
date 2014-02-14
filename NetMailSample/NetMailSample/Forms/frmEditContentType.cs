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
        public string attContentType, origValue;

        /// <summary>
        /// constructor for the form
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
    }
}
