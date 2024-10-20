namespace ChatAppGUI
{
    partial class ServerForm
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
            btnStart = new Button();
            chatServerLogs = new RichTextBox();
            btnStop = new Button();
            btnExecuteCommand = new Button();
            txtCommand = new TextBox();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(22, 25);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(131, 54);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start Server";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // chatServerLogs
            // 
            chatServerLogs.Location = new Point(171, 25);
            chatServerLogs.Name = "chatServerLogs";
            chatServerLogs.Size = new Size(286, 203);
            chatServerLogs.TabIndex = 1;
            chatServerLogs.Text = "";
            // 
            // btnStop
            // 
            btnStop.Location = new Point(22, 85);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(131, 54);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop Server";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnExecuteCommand
            // 
            btnExecuteCommand.Location = new Point(171, 261);
            btnExecuteCommand.Name = "btnExecuteCommand";
            btnExecuteCommand.Size = new Size(286, 28);
            btnExecuteCommand.TabIndex = 3;
            btnExecuteCommand.Text = "Execute Command";
            btnExecuteCommand.UseVisualStyleBackColor = true;
            btnExecuteCommand.Click += btnExecuteCommand_Click;
            // 
            // txtCommand
            // 
            txtCommand.Location = new Point(171, 234);
            txtCommand.Name = "txtCommand";
            txtCommand.Size = new Size(286, 23);
            txtCommand.TabIndex = 4;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 301);
            Controls.Add(txtCommand);
            Controls.Add(btnExecuteCommand);
            Controls.Add(btnStop);
            Controls.Add(chatServerLogs);
            Controls.Add(btnStart);
            Name = "ServerForm";
            Text = "ServerForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private RichTextBox chatServerLogs;
        private Button btnStop;
        private Button btnExecuteCommand;
        private TextBox txtCommand;
    }
}