using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAppGUI
{
    public partial class ServerForm : Form
    {
        private Chat chat;
        public ServerForm()
        {
            InitializeComponent();
            chat = new Chat();
            chat.MessageReceived += OnMessageReceived; // Subscribe to the event
            chat.ClientConnected += OnClientConnected; // Subscribe to the client connected event
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            chat.StartServer(5000);
            string hostIP = chat.GetHostIP(); // Get the host IP
            int port = chat.GetPort(); // Get the port
            Log($"Server started at {hostIP}:{port}"); // Log the IP and port
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            chat.StopServer();
            Log("Server stopped.");
        }

        private void Log(string msg)
        {
            if (chatServerLogs.InvokeRequired)
            {
                // If we're not on the UI thread, invoke the method on the UI thread
                chatServerLogs.Invoke((MethodInvoker)(() => Log(msg)));
            }
            else
            {
                // Append the message to the log
                chatServerLogs.AppendText(msg + Environment.NewLine);
            }
        }

        private void OnMessageReceived(string message)
        {
            // Update log with received message from clients
            Log(message);
        }
        private void OnClientConnected(string message)
        {
            // Log when a new client connects
            Log(message);
        }

        private void btnExecuteCommand_Click(object sender, EventArgs e)
        {
            string command = txtCommand.Text.Trim();
            if (!string.IsNullOrEmpty(command))
            {
                chat.ExecuteCommand(command); // Call the command execution method
                txtCommand.Clear(); // Clear the command input
            }
        }
    }
}
