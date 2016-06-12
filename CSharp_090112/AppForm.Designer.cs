namespace NetChat {
	partial class AppForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.btnHostChat = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbxServerName = new System.Windows.Forms.TextBox();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxHistory = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(207, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 13);
            label1.TabIndex = 1;
            label1.Text = "Server name:";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 334);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 13);
            label2.TabIndex = 5;
            label2.Text = "User name:";
            // 
            // btnHostChat
            // 
            this.btnHostChat.Location = new System.Drawing.Point(12, 12);
            this.btnHostChat.Name = "btnHostChat";
            this.btnHostChat.Size = new System.Drawing.Size(107, 23);
            this.btnHostChat.TabIndex = 0;
            this.btnHostChat.Text = "Host chat";
            this.btnHostChat.UseVisualStyleBackColor = true;
            this.btnHostChat.Click += new System.EventHandler(this.btnHostChat_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(466, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(107, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbxServerName
            // 
            this.tbxServerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxServerName.Location = new System.Drawing.Point(283, 14);
            this.tbxServerName.Name = "tbxServerName";
            this.tbxServerName.Size = new System.Drawing.Size(177, 20);
            this.tbxServerName.TabIndex = 2;
            // 
            // tbxUserName
            // 
            this.tbxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxUserName.Location = new System.Drawing.Point(76, 331);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(141, 20);
            this.tbxUserName.TabIndex = 6;
            this.tbxUserName.Text = "John";
            // 
            // tbxMessage
            // 
            this.tbxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMessage.Enabled = false;
            this.tbxMessage.Location = new System.Drawing.Point(12, 357);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(448, 20);
            this.tbxMessage.TabIndex = 7;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(466, 355);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(107, 23);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxHistory
            // 
            this.tbxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHistory.BackColor = System.Drawing.SystemColors.Window;
            this.tbxHistory.Enabled = false;
            this.tbxHistory.Location = new System.Drawing.Point(12, 41);
            this.tbxHistory.Multiline = true;
            this.tbxHistory.Name = "tbxHistory";
            this.tbxHistory.ReadOnly = true;
            this.tbxHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxHistory.Size = new System.Drawing.Size(448, 284);
            this.tbxHistory.TabIndex = 4;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 389);
            this.Controls.Add(this.tbxHistory);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(label2);
            this.Controls.Add(this.tbxServerName);
            this.Controls.Add(label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnHostChat);
            this.MinimumSize = new System.Drawing.Size(512, 203);
            this.Name = "AppForm";
            this.Text = "NetChat";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnHostChat;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TextBox tbxServerName;
		private System.Windows.Forms.TextBox tbxUserName;
		private System.Windows.Forms.TextBox tbxMessage;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox tbxHistory;
	}
}

