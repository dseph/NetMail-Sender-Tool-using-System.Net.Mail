namespace NetMailSample.Forms
{
    partial class frmAlternateView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlternateView));
            this.richTxtHtmlView = new System.Windows.Forms.RichTextBox();
            this.txtBoxPlainView = new System.Windows.Forms.TextBox();
            this.btnAddAlternateViews = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddLR = new System.Windows.Forms.Button();
            this.txtCid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLinkedResPath = new System.Windows.Forms.TextBox();
            this.btnLinkedResBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabHtml = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnModifyContentType = new System.Windows.Forms.Button();
            this.btnDeleteAttachment = new System.Windows.Forms.Button();
            this.dGridInlineAttachments = new System.Windows.Forms.DataGridView();
            this.colFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboTransferEncoding = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tabHtml.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridInlineAttachments)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTxtHtmlView
            // 
            this.richTxtHtmlView.Location = new System.Drawing.Point(6, 6);
            this.richTxtHtmlView.Name = "richTxtHtmlView";
            this.richTxtHtmlView.Size = new System.Drawing.Size(350, 264);
            this.richTxtHtmlView.TabIndex = 2;
            this.richTxtHtmlView.Text = "";
            // 
            // txtBoxPlainView
            // 
            this.txtBoxPlainView.Location = new System.Drawing.Point(6, 6);
            this.txtBoxPlainView.Multiline = true;
            this.txtBoxPlainView.Name = "txtBoxPlainView";
            this.txtBoxPlainView.Size = new System.Drawing.Size(350, 264);
            this.txtBoxPlainView.TabIndex = 3;
            // 
            // btnAddAlternateViews
            // 
            this.btnAddAlternateViews.Location = new System.Drawing.Point(619, 421);
            this.btnAddAlternateViews.Name = "btnAddAlternateViews";
            this.btnAddAlternateViews.Size = new System.Drawing.Size(68, 23);
            this.btnAddAlternateViews.TabIndex = 4;
            this.btnAddAlternateViews.Text = "OK";
            this.btnAddAlternateViews.UseVisualStyleBackColor = true;
            this.btnAddAlternateViews.Click += new System.EventHandler(this.btnAddAlternateViews_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(693, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddLR);
            this.groupBox1.Controls.Add(this.txtCid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLinkedResPath);
            this.groupBox1.Controls.Add(this.btnLinkedResBrowse);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 81);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add File To Message Attachments";
            // 
            // btnAddLR
            // 
            this.btnAddLR.Image = global::NetMailSample.Properties.Resources.AddMark_10580;
            this.btnAddLR.Location = new System.Drawing.Point(446, 49);
            this.btnAddLR.Name = "btnAddLR";
            this.btnAddLR.Size = new System.Drawing.Size(43, 23);
            this.btnAddLR.TabIndex = 9;
            this.btnAddLR.UseVisualStyleBackColor = true;
            this.btnAddLR.Click += new System.EventHandler(this.btnAddLR_Click);
            // 
            // txtCid
            // 
            this.txtCid.Location = new System.Drawing.Point(71, 50);
            this.txtCid.Name = "txtCid";
            this.txtCid.Size = new System.Drawing.Size(369, 20);
            this.txtCid.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Content Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Path:";
            // 
            // txtLinkedResPath
            // 
            this.txtLinkedResPath.Location = new System.Drawing.Point(71, 20);
            this.txtLinkedResPath.Name = "txtLinkedResPath";
            this.txtLinkedResPath.Size = new System.Drawing.Size(369, 20);
            this.txtLinkedResPath.TabIndex = 1;
            // 
            // btnLinkedResBrowse
            // 
            this.btnLinkedResBrowse.Image = global::NetMailSample.Properties.Resources.OpenAttachment_13115;
            this.btnLinkedResBrowse.Location = new System.Drawing.Point(446, 20);
            this.btnLinkedResBrowse.Name = "btnLinkedResBrowse";
            this.btnLinkedResBrowse.Size = new System.Drawing.Size(43, 23);
            this.btnLinkedResBrowse.TabIndex = 0;
            this.btnLinkedResBrowse.UseVisualStyleBackColor = true;
            this.btnLinkedResBrowse.Click += new System.EventHandler(this.btnLinkedResBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Title = "Open Linked Resource";
            // 
            // tabHtml
            // 
            this.tabHtml.Controls.Add(this.tabPage1);
            this.tabHtml.Controls.Add(this.tabPage2);
            this.tabHtml.Location = new System.Drawing.Point(9, 16);
            this.tabHtml.Name = "tabHtml";
            this.tabHtml.SelectedIndex = 0;
            this.tabHtml.Size = new System.Drawing.Size(370, 302);
            this.tabHtml.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTxtHtmlView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Html";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtBoxPlainView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plain Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnModifyContentType);
            this.groupBox2.Controls.Add(this.btnDeleteAttachment);
            this.groupBox2.Controls.Add(this.dGridInlineAttachments);
            this.groupBox2.Location = new System.Drawing.Point(403, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 324);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message Attachments (select row to modify or delete)";
            // 
            // btnModifyContentType
            // 
            this.btnModifyContentType.Image = global::NetMailSample.Properties.Resources.EditTitleString_357;
            this.btnModifyContentType.Location = new System.Drawing.Point(55, 296);
            this.btnModifyContentType.Name = "btnModifyContentType";
            this.btnModifyContentType.Size = new System.Drawing.Size(43, 23);
            this.btnModifyContentType.TabIndex = 11;
            this.btnModifyContentType.UseVisualStyleBackColor = true;
            this.btnModifyContentType.Click += new System.EventHandler(this.btnModifyContentType_Click);
            // 
            // btnDeleteAttachment
            // 
            this.btnDeleteAttachment.Image = global::NetMailSample.Properties.Resources.Clearallrequests_8816;
            this.btnDeleteAttachment.Location = new System.Drawing.Point(6, 296);
            this.btnDeleteAttachment.Name = "btnDeleteAttachment";
            this.btnDeleteAttachment.Size = new System.Drawing.Size(43, 23);
            this.btnDeleteAttachment.TabIndex = 10;
            this.btnDeleteAttachment.UseVisualStyleBackColor = true;
            this.btnDeleteAttachment.Click += new System.EventHandler(this.btnDeleteAttachment_Click);
            // 
            // dGridInlineAttachments
            // 
            this.dGridInlineAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridInlineAttachments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFilePath,
            this.colCid,
            this.colContentType});
            this.dGridInlineAttachments.Location = new System.Drawing.Point(6, 16);
            this.dGridInlineAttachments.MultiSelect = false;
            this.dGridInlineAttachments.Name = "dGridInlineAttachments";
            this.dGridInlineAttachments.Size = new System.Drawing.Size(347, 274);
            this.dGridInlineAttachments.TabIndex = 0;
            // 
            // colFilePath
            // 
            this.colFilePath.HeaderText = "Path";
            this.colFilePath.Name = "colFilePath";
            // 
            // colCid
            // 
            this.colCid.HeaderText = "Content Id";
            this.colCid.Name = "colCid";
            // 
            // colContentType
            // 
            this.colContentType.HeaderText = "Content Type";
            this.colContentType.Name = "colContentType";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabHtml);
            this.groupBox3.Location = new System.Drawing.Point(12, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 324);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alternate View";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboTransferEncoding);
            this.groupBox4.Location = new System.Drawing.Point(521, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 73);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Transfer-Encoding";
            // 
            // cboTransferEncoding
            // 
            this.cboTransferEncoding.FormattingEnabled = true;
            this.cboTransferEncoding.Items.AddRange(new object[] {
            "SevenBit",
            "Base64",
            "QuotedPrintable"});
            this.cboTransferEncoding.Location = new System.Drawing.Point(6, 19);
            this.cboTransferEncoding.Name = "cboTransferEncoding";
            this.cboTransferEncoding.Size = new System.Drawing.Size(223, 21);
            this.cboTransferEncoding.TabIndex = 0;
            this.cboTransferEncoding.Text = "QuotedPrintable";
            // 
            // frmAlternateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 454);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddAlternateViews);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlternateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alternate Views";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabHtml.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGridInlineAttachments)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtHtmlView;
        private System.Windows.Forms.TextBox txtBoxPlainView;
        private System.Windows.Forms.Button btnAddAlternateViews;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLinkedResPath;
        private System.Windows.Forms.Button btnLinkedResBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtCid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabHtml;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnAddLR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnModifyContentType;
        private System.Windows.Forms.Button btnDeleteAttachment;
        private System.Windows.Forms.DataGridView dGridInlineAttachments;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboTransferEncoding;
    }
}