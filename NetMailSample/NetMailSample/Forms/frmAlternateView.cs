using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmAlternateView : Form
    {
        public string cid, cidPath;
        public DataTable inlineTable = new DataTable();

        public frmAlternateView()
        {
            InitializeComponent();

            // set initial values for the html and plain textboxes
            richTxtHtmlView.Text = NetMailSample.Properties.Settings.Default.AltViewHtml;
            txtBoxPlainView.Text = NetMailSample.Properties.Settings.Default.AltViewPlain;
            cboTransferEncoding.Text = NetMailSample.Properties.Settings.Default.BodyTransferEncoding;
        }

        /// <summary>
        /// This button adds the html and plain text Alt Views to 
        /// the main forms string values, which will get added when the message is sent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAlternateViews_Click(object sender, EventArgs e)
        {
            if (txtBoxPlainView.Text != "" && richTxtHtmlView.Text != "")
            {
                NetMailSample.Properties.Settings.Default.AltViewHtml = richTxtHtmlView.Text;
                NetMailSample.Properties.Settings.Default.AltViewPlain = txtBoxPlainView.Text;
                NetMailSample.Properties.Settings.Default.BodyTransferEncoding = cboTransferEncoding.Text;
            }
            else if(richTxtHtmlView.Text != "" && txtBoxPlainView.Text == "")
            {
                txtBoxPlainView.Text = richTxtHtmlView.Text;
            }
            else if(richTxtHtmlView.Text == "" && txtBoxPlainView.Text != "")
            {
                richTxtHtmlView.Text = txtBoxPlainView.Text;
            }
            
            inlineTable.Columns.Add("Path", typeof(string));
            inlineTable.Columns.Add("Cid", typeof(string));
            inlineTable.Columns.Add("ContentType", typeof(string));

            foreach (DataGridViewRow rowAtt in dGridInlineAttachments.Rows)
            {
                if (rowAtt.Cells[0].Value != null)
                {
                    inlineTable.Rows.Add(rowAtt.Cells[0].Value, rowAtt.Cells[1].Value, rowAtt.Cells[2].Value);
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLinkedResBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtLinkedResPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnAddLR_Click(object sender, EventArgs e)
        {
            if (txtLinkedResPath.Text != "" && txtCid.Text != "")
            {
                int n = dGridInlineAttachments.Rows.Add();
                dGridInlineAttachments.Rows[n].Cells[0].Value = txtLinkedResPath.Text;
                dGridInlineAttachments.Rows[n].Cells[1].Value = txtCid.Text;
                dGridInlineAttachments.Rows[n].Cells[2].Value = MediaTypeNames.Application.Octet;
                txtLinkedResPath.Text = "";
                txtCid.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Both ContentId and File Path are required to add a linked resource.",
                "Invalid Linked Resource", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            if (dGridInlineAttachments.SelectedRows.Count > 0) { dGridInlineAttachments.Rows.RemoveAt(dGridInlineAttachments.SelectedRows[0].Index); }
        }

        private void btnModifyContentType_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGridInlineAttachments.SelectedRows.Count > 0)
                {
                    NetMailSample.Forms.frmEditContentType mEditContentType = new Forms.frmEditContentType(dGridInlineAttachments.SelectedRows[0].Cells[2].Value.ToString());
                    mEditContentType.Owner = this;
                    mEditContentType.ShowDialog(this);
                    dGridInlineAttachments.SelectedRows[0].Cells[2].Value = mEditContentType.attContentType;
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }
    }
}
