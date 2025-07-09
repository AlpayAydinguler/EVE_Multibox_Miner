namespace EVE_Multibox_Miner.UI
{
    partial class ClientProfileControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblClientName = new System.Windows.Forms.Label();
            this.btnSetWindow = new System.Windows.Forms.Button();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnSetCompression = new System.Windows.Forms.Button();
            this.btnSetCompressButton = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientName.Location = new System.Drawing.Point(10, 10);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(85, 15);
            this.lblClientName.TabIndex = 0;
            this.lblClientName.Text = "Client Name";
            // 
            // btnSetWindow
            // 
            this.btnSetWindow.Location = new System.Drawing.Point(13, 40);
            this.btnSetWindow.Name = "btnSetWindow";
            this.btnSetWindow.Size = new System.Drawing.Size(120, 30);
            this.btnSetWindow.TabIndex = 1;
            this.btnSetWindow.Text = "Set Window Area";
            this.btnSetWindow.UseVisualStyleBackColor = true;
            this.btnSetWindow.Click += new System.EventHandler(this.btnSetWindow_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(140, 48);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(47, 13);
            this.lblPosition.TabIndex = 2;
            this.lblPosition.Text = "Not set";
            // 
            // btnSetCompression
            // 
            this.btnSetCompression.Location = new System.Drawing.Point(13, 80);
            this.btnSetCompression.Name = "btnSetCompression";
            this.btnSetCompression.Size = new System.Drawing.Size(120, 30);
            this.btnSetCompression.TabIndex = 3;
            this.btnSetCompression.Text = "Set Ore Box";
            this.btnSetCompression.UseVisualStyleBackColor = true;
            this.btnSetCompression.Click += new System.EventHandler(this.btnSetCompression_Click);
            // 
            // btnSetCompressButton
            // 
            this.btnSetCompressButton.Location = new System.Drawing.Point(13, 120);
            this.btnSetCompressButton.Name = "btnSetCompressButton";
            this.btnSetCompressButton.Size = new System.Drawing.Size(120, 30);
            this.btnSetCompressButton.TabIndex = 4;
            this.btnSetCompressButton.Text = "Set Compress Button";
            this.btnSetCompressButton.UseVisualStyleBackColor = true;
            this.btnSetCompressButton.Click += new System.EventHandler(this.btnSetCompressButton_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.LightGray;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(140, 120);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 30);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Mining";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClientProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSetCompressButton);
            this.Controls.Add(this.btnSetCompression);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.btnSetWindow);
            this.Controls.Add(this.lblClientName);
            this.Name = "ClientProfileControl";
            this.Size = new System.Drawing.Size(280, 170);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Button btnSetWindow;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnSetCompression;
        private System.Windows.Forms.Button btnSetCompressButton;
        private System.Windows.Forms.Label lblStatus;
    }
}