using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatAppGUI
{


    public class Chat
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();
        private Dictionary<string, TcpClient> clientDictionary = new Dictionary<string, TcpClient>();
        private Dictionary<string, string> aliasDictionary = new Dictionary<string, string>(); // Map aliases to client IDs
        private bool isRunning;
        private int port;

        public event Action<string> MessageReceived; // Event for message received
        public event Action<string> ClientConnected; // Event for client connection

        public void StartServer(int port)
        {
            this.port = port; // Store the port
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            isRunning = true;

            Thread acceptThread = new Thread(AcceptClients);
            acceptThread.Start();
        }

        public void SetAlias(string clientId, string alias)
        {
            if (!aliasDictionary.ContainsKey(alias))
            {
                aliasDictionary[alias] = clientId; // Map alias to client ID
                MessageReceived?.Invoke($"Client {clientId} set alias to {alias}");
            }
        }

        public string GetHostIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Filter for IPv4
                    return ip.ToString();
            }
            return "Not available";
        }

        public int GetPort()
        {
            return port; // Return the stored port
        }

        private void AcceptClients()
        {
            while (isRunning)
            {
                var client = listener.AcceptTcpClient();
                string clientId = Guid.NewGuid().ToString();
                clients.Add(client);
                clientDictionary[clientId] = client;

                // Trigger the ClientConnected event
                ClientConnected?.Invoke($"Client {clientId} connected.");

                // Example of setting an alias (you can adjust this logic as needed)
                SetAlias(clientId, "Client" + clientId.Substring(0, 4)); // Default alias for demo purposes

                Thread clientThread = new Thread(() => HandleClient(client, clientId));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client, string clientId)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (isRunning)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    MessageReceived?.Invoke($"Received from {clientId}: {message}");

                    if (message.StartsWith("SET_ALIAS:"))
                    {
                        string alias = message.Substring(10).Trim();
                        SetAlias(clientId, alias);
                        MessageReceived?.Invoke($"Client {clientId} set alias to {alias}");
                    }
                    else if (message.StartsWith("PM:"))
                    {
                        // Handle private messages (existing logic)
                    }
                    else if (message.StartsWith("BROADCAST:"))
                    {
                        // Handle broadcast messages (existing logic)
                    }
                }

                // Cleanup
                clients.Remove(client);
                clientDictionary.Remove(clientId);
                aliasDictionary.Remove(aliasDictionary.FirstOrDefault(x => x.Value == clientId).Key); // Remove alias if exists
                client.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Disconnected: {ex.Message}");
            }
            finally
            {
                // Cleanup logic...
            }
        }

        public void StopServer()
        {
            isRunning = false;
            foreach (var client in clients)
            {
                client.Close();
            }
            listener.Stop();
        }

        public void DisconnectClient(string alias)
        {
            if (aliasDictionary.TryGetValue(alias, out string clientId))
            {
                if (clientDictionary.TryGetValue(clientId, out TcpClient client))
                {
                    try
                    {
                        // Notify the client before closing the connection
                        NetworkStream stream = client.GetStream();
                        byte[] msgBuffer = Encoding.UTF8.GetBytes("You have been disconnected.");
                        stream.Write(msgBuffer, 0, msgBuffer.Length);

                        // Close the client connection
                        client.Close();

                        // Remove from all tracking dictionaries
                        clients.Remove(client);
                        clientDictionary.Remove(clientId);
                        aliasDictionary.Remove(alias); // Remove the alias

                        MessageReceived?.Invoke($"Client {alias} has been disconnected.");
                    }
                    catch (Exception ex)
                    {
                        MessageReceived?.Invoke($"Error disconnecting client {alias}: {ex.Message}");
                    }
                }
                else
                {
                    MessageReceived?.Invoke($"No active connection found for client {clientId}.");
                }
            }
            else
            {
                MessageReceived?.Invoke($"No client found with alias: {alias}");
            }
        }


        public void ExecuteCommand(string command)
        {
            try
            {
                if (command.StartsWith("DISCONNECT:"))
                {
                    string alias = command.Substring(11).Trim();
                    DisconnectClient(alias);
                }
                else if (command.StartsWith("BROADCAST:"))
                {
                    string message = command.Substring(10).Trim(); // Extract the message
                    BroadcastMessage(message); // Call the broadcast method
                    MessageReceived?.Invoke($"Broadcasted: {message}");
                }
                else if (command.StartsWith("SEND:"))
                {
                    // Format: SEND: ClientAlias: Your message here
                    var parts = command.Substring(5).Split(new[] { ':' }, 2);
                    if (parts.Length == 2)
                    {
                        string alias = parts[0].Trim();
                        string message = parts[1].Trim();
                        SendMessageToClient(alias, message); // Call the send message method
                    }
                    else
                    {
                        MessageReceived?.Invoke("Invalid SEND command format. Use: SEND: ClientAlias: Your message.");
                    }
                }
                else
                {
                    MessageReceived?.Invoke($"Unknown command: {command}");
                }
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke($"Error executing command: {ex.Message}");
            }
        }


        public void BroadcastMessage(string message)
        {
            string formattedMessage = $"SERVER: {message}";
            byte[] msgBuffer = Encoding.UTF8.GetBytes(formattedMessage);

            foreach (var client in clients)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(msgBuffer, 0, msgBuffer.Length); // Send the message to the client
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it and remove the client if needed)
                    MessageReceived?.Invoke($"Error sending message to client: {ex.Message}");
                }
            }
        }

        public void SendMessageToClient(string alias, string message)
        {
            if (aliasDictionary.TryGetValue(alias, out string clientId))
            {
                if (clientDictionary.TryGetValue(clientId, out TcpClient client))
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        string formattedMessage = $"SERVER: {message}";
                        byte[] msgBuffer = Encoding.UTF8.GetBytes(formattedMessage);
                        stream.Write(msgBuffer, 0, msgBuffer.Length); // Send the message to the specific client
                        MessageReceived?.Invoke($"Message sent to {alias}: {message}");
                    }
                    catch (Exception ex)
                    {
                        MessageReceived?.Invoke($"Error sending message to {alias}: {ex.Message}");
                    }
                }
                else
                {
                    MessageReceived?.Invoke($"No active connection found for client {clientId}.");
                }
            }
            else
            {
                MessageReceived?.Invoke($"No client found with alias: {alias}");
            }
        }


    }



}
