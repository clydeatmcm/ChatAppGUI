namespace ChatAppGUI
{
    partial class ClientForm
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
            btnSend = new Button();
            btnConnect = new Button();
            lstMessages = new RichTextBox();
            txtMessage = new TextBox();
            txtHost = new TextBox();
            txtPort = new TextBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(288, 109);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(105, 45);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click_1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(288, 24);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(105, 45);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click_1;
            // 
            // lstMessages
            // 
            lstMessages.Location = new Point(22, 138);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(260, 115);
            lstMessages.TabIndex = 2;
            lstMessages.Text = "";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(22, 109);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(260, 23);
            txtMessage.TabIndex = 3;
            // 
            // txtHost
            // 
            txtHost.Location = new Point(22, 24);
            txtHost.Name = "txtHost";
            txtHost.PlaceholderText = "127.0.0.1";
            txtHost.Size = new Size(260, 23);
            txtHost.TabIndex = 4;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(22, 53);
            txtPort.Name = "txtPort";
            txtPort.PlaceholderText = "5000";
            txtPort.Size = new Size(260, 23);
            txtPort.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(409, 27);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(79, 15);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Disconnected";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 274);
            Controls.Add(lblStatus);
            Controls.Add(txtPort);
            Controls.Add(txtHost);
            Controls.Add(txtMessage);
            Controls.Add(lstMessages);
            Controls.Add(btnConnect);
            Controls.Add(btnSend);
            Name = "ClientForm";
            Text = "ClientForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Button btnConnect;
        private RichTextBox lstMessages;
        private TextBox txtMessage;
        private TextBox txtHost;
        private TextBox txtPort;
        private Label lblStatus;
    }
}