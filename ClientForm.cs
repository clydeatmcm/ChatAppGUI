using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAppGUI
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string clientAlias;

        public ClientForm()
        {
            InitializeComponent();
            lblStatus.Text = "Disconnected"; // Set default status
            lblStatus.ForeColor = System.Drawing.Color.Red; // Set color for disconnected
            
            clientAlias = "Client" + Guid.NewGuid().ToString().Substring(0, 4);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

        }

        private void StartReceiving()
        {
            var readThread = new Thread(() =>
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Server disconnected
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke((MethodInvoker)(() => Log(message)));

                    if (message.StartsWith("BROADCAST:"))
                    {
                        string broadcastMessage = message.Substring(10).Trim(); // Extract the broadcast message
                        Log($"Broadcast from server: {broadcastMessage}"); // Log it in the client
                    }
                }

                Invoke((MethodInvoker)(() => UpdateStatusDisconnected()));
            });
            readThread.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void Log(string message)
        {
            lstMessages.AppendText(message + Environment.NewLine);
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            string host = txtHost.Text; // Get host from the textbox
            int port;

            // Validate port input
            if (!int.TryParse(txtPort.Text, out port) || port <= 0 || port > 65535)
            {
                MessageBox.Show("Please enter a valid port number (1-65535).");
                return;
            }

            try
            {
                client = new TcpClient(host, port); // Connect to the specified host and port
                stream = client.GetStream();
                Log("Connected to server.");
                lblStatus.Text = "Connected"; // Update status label
                lblStatus.ForeColor = System.Drawing.Color.Green; // Change color to green
                StartReceiving();
                SetAlias(clientAlias);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}");
            }
        }

        private void SetAlias(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                byte[] aliasBuffer = Encoding.UTF8.GetBytes($"SET_ALIAS:{alias}");
                stream.Write(aliasBuffer, 0, aliasBuffer.Length);
                clientAlias = alias;
                Log($"Alias set to: {alias}");
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                byte[] msgBuffer = Encoding.UTF8.GetBytes($"{clientAlias}: {message}");
                stream.Write(msgBuffer, 0, msgBuffer.Length);
                txtMessage.Clear();
            }
        }

        private void UpdateStatusDisconnected()
        {
            lblStatus.Text = "Disconnected"; // Update status label
            lblStatus.ForeColor = System.Drawing.Color.Red; // Change color back to red
        }
    }
}
