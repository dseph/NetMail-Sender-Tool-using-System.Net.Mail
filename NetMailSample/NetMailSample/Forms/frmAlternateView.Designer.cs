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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnModifyContentType = new System.Windows.Forms.Button();
            this.btnDeleteAttachment = new System.Windows.Forms.Button();
            this.dGridInlineAttachments = new System.Windows.Forms.DataGridView();
            this.colFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboTransferEncoding = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCalSample = new System.Windows.Forms.Button();
            this.cboAltViewContentType = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtAltViewBody = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridInlineAttachments)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(331, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 81);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add File To Message Attachments";
            // 
            // btnAddLR
            // 
            this.btnAddLR.Image = global::NetMailSample.Properties.Resources.AddMark_10580;
            this.btnAddLR.Location = new System.Drawing.Point(382, 48);
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
            this.txtCid.Size = new System.Drawing.Size(305, 20);
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
            this.txtLinkedResPath.Size = new System.Drawing.Size(305, 20);
            this.txtLinkedResPath.TabIndex = 1;
            // 
            // btnLinkedResBrowse
            // 
            this.btnLinkedResBrowse.Image = global::NetMailSample.Properties.Resources.OpenAttachment_13115;
            this.btnLinkedResBrowse.Location = new System.Drawing.Point(382, 18);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboTransferEncoding);
            this.groupBox4.Location = new System.Drawing.Point(155, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 81);
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
            "QuotedPrintable",
            "Unknown"});
            this.cboTransferEncoding.Location = new System.Drawing.Point(6, 19);
            this.cboTransferEncoding.Name = "cboTransferEncoding";
            this.cboTransferEncoding.Size = new System.Drawing.Size(152, 21);
            this.cboTransferEncoding.TabIndex = 0;
            this.cboTransferEncoding.Text = "QuotedPrintable";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCalSample);
            this.groupBox3.Controls.Add(this.cboAltViewContentType);
            this.groupBox3.Location = new System.Drawing.Point(12, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(137, 81);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Content-Type";
            // 
            // btnCalSample
            // 
            this.btnCalSample.Location = new System.Drawing.Point(6, 47);
            this.btnCalSample.Name = "btnCalSample";
            this.btnCalSample.Size = new System.Drawing.Size(114, 23);
            this.btnCalSample.TabIndex = 12;
            this.btnCalSample.Text = "Insert vCal Sample";
            this.btnCalSample.UseVisualStyleBackColor = true;
            this.btnCalSample.Click += new System.EventHandler(this.btnCalSample_Click);
            // 
            // cboAltViewContentType
            // 
            this.cboAltViewContentType.FormattingEnabled = true;
            this.cboAltViewContentType.Items.AddRange(new object[] {
            "HTML",
            "PlainText",
            "vCalendar"});
            this.cboAltViewContentType.Location = new System.Drawing.Point(6, 19);
            this.cboAltViewContentType.Name = "cboAltViewContentType";
            this.cboAltViewContentType.Size = new System.Drawing.Size(114, 21);
            this.cboAltViewContentType.TabIndex = 0;
            this.cboAltViewContentType.Text = "HTML";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAltViewBody);
            this.groupBox5.Location = new System.Drawing.Point(12, 91);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(385, 351);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Alt View Body";
            // 
            // txtAltViewBody
            // 
            this.txtAltViewBody.Location = new System.Drawing.Point(3, 16);
            this.txtAltViewBody.Multiline = true;
            this.txtAltViewBody.Name = "txtAltViewBody";
            this.txtAltViewBody.Size = new System.Drawing.Size(376, 329);
            this.txtAltViewBody.TabIndex = 0;
            // 
            // frmAlternateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 454);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
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
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGridInlineAttachments)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddAlternateViews;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLinkedResPath;
        private System.Windows.Forms.Button btnLinkedResBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtCid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddLR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnModifyContentType;
        private System.Windows.Forms.Button btnDeleteAttachment;
        private System.Windows.Forms.DataGridView dGridInlineAttachments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboTransferEncoding;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboAltViewContentType;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtAltViewBody;
        private System.Windows.Forms.Button btnCalSample;
    }
}