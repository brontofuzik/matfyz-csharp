using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace NetChat {
	public partial class AppForm : Form {
        class ClientInfo
        {
            public Socket socket;
            public string name;
            public byte[] buffer;
            public StringBuilder stringBuffer;

            public ClientInfo(Socket sock)
            {
                this.socket = sock;
                this.buffer = new byte[receiveBufferSize];
                this.name = "";
                this.stringBuffer = new StringBuilder();
            }
        }

        enum AppState { Client, Server, NotSet }
        
        // Note: Consider using Decoder.GetChars() - compare with Encoding.GetString() or GetChars().
		//		 Decoder can be acquired via Encoding.UTF8.GetDecoder() call.
		//
		// Note: Always lock shared (among multiple threads) data structures:
		//
		//			lock (sharedObjectReference) {
		//				... critical section ...
		//			}
		//
		//		 Avoid deadlocks!!!
		//
		// Command "message" format - everything is always in UTF-8:
		//     MSG:user_name:message_text\n
		//
		private const string CommandMessage = "MSG:";
        private const string CommandLogin = "LOG:";
        private const string CommandLogout = "OUT";
		private const char UserMessageDelimiter = ':';
		private const char CommandTerminator = '\n';

        private const int receiveBufferSize = 1024;

		private const int BasePort = 14241;

        private AppState AppStatus = AppState.NotSet;

		// Used only in server mode
		private Socket m_serverSocket = null;
        private List<ClientInfo> clientList = null;
        string UserName = "";

		// Used only in client mode
        private ClientInfo User;
                
		public AppForm() {
			InitializeComponent();
		}

		private void btnHostChat_Click(object sender, EventArgs e) {
            if (AppStatus != AppState.NotSet) return;
            
            m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            AppStatus = AppState.Server;
            clientList = new List<ClientInfo>();

			// Replace the following IPAddress.Loopback with IPAddress.Any,
			// if you want to listen on all computer's interfaces.
			m_serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, BasePort));

			m_serverSocket.Listen(10);

            // TO DO: Call Socket.BeginAccept(AcceptCallback, ...)
            //		  Note: Asynchronous call is a necessity as we cannot block the main thread here,
            //				until a client connection arrives.
            //		  Note: Do not forget that any asynchronous call might be completed synchrounously.
            //			    If important check using:
            //					IAsyncResult ar = ... .BeginAccept(AcceptCallback, ...);
            //					if (ar.CompletedSynchronously) ...

            IAsyncResult ar = m_serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            if (ar.CompletedSynchronously)
            {
                // TO DO: Something
            }

            UserName = tbxUserName.Text;

            Connecting();
            Connected();
		}

		private void AcceptCallback(IAsyncResult ar) {
            // TO DO: Call Socket.EndAccept(ar).
            //        Do not forget to handle exceptions thrown by EndAccept
            //		  (i.e. exceptions that would be normally thrown by calling the sync Accept method).

            // TO DO: Call Socket.BeginReceive(..., ServerReceiveCallback, ...).
            try
            {
                Socket clientSocket = m_serverSocket.EndAccept(ar);

                // Waiting for other clients
                m_serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

                // Create a client
                ClientInfo client = new ClientInfo(clientSocket);

                // Start receiving data
                clientSocket.BeginReceive(client.buffer, 0, receiveBufferSize, SocketFlags.None, new AsyncCallback(ServerReceiveCallback), client);
            }
            catch
            {
                // ...
            }            
		}

		private void ServerReceiveCallback(IAsyncResult ar) 
        {
            // TO DO: Call Socket.EndReceive(ar).
            //        Do not forget to handle exceptions thrown by EndReceive
            //		  (i.e. exceptions that would be normally thrown by calling the sync Receive method).

            // TO DO: If received a complete "CommandMessage", distribute it to all clients.
            //        Message can be sent either synchronously (via Socket.Send) or async (via Socket.BeginSend).

            // TO DO: Call Socket.BeginReceive(..., ServerReceiveCallback, ...).
            
            try
            {
                ClientInfo client = (ClientInfo)ar.AsyncState;
                
                lock (client)
                {                    
                    int readedbytes = client.socket.EndReceive(ar);

                    char[] readedchars = new char[Encoding.UTF8.GetDecoder().GetCharCount(client.buffer, 0, readedbytes)];

                    Encoding.UTF8.GetDecoder().GetChars(client.buffer, 0, readedbytes, readedchars, 0);

                    client.stringBuffer.Append(readedchars);

                    string[] commands = client.stringBuffer.ToString().Split(CommandTerminator);

                    if (commands.Length > 0)
                    {
                        for (int i = 0; i < commands.Length - 1; i++)
                        {
                            var item = commands[i];
                            string proc;

                            if (item.IndexOf(CommandLogin) == 0)
                            {
                                proc = item.Remove(0, CommandLogin.Length);
                                if (proc.Contains(':') || (proc == ""))
                                {
                                    // ... Name not alowed
                                }
                                else
                                {
                                    client.name = proc;
                                    clientList.Add(client);
                                }
                            }
                            else if (item.IndexOf(CommandMessage) == 0)
                            {
                                proc = item.Remove(0, CommandMessage.Length);

                                SubmitMsg smsg = new SubmitMsg(SubmitMessage);
                                smsg.BeginInvoke(client, proc, null, null);
                            }
                            else if (item.IndexOf(CommandLogout) == 0)
                            {
                                client.name = "";
                                clientList.Remove(client);
                            }
                        }

                        client.stringBuffer = new StringBuilder(commands[commands.Length - 1]);
                                                
                        if (client.name != "")
                        {
                            // Continue receiving data
                            client.socket.BeginReceive(client.buffer, 0, receiveBufferSize, SocketFlags.None, new AsyncCallback(ServerReceiveCallback), client);
                        }
                        else
                        {
                            // Disconnect
                            client.socket.Shutdown(SocketShutdown.Both);
                            client.socket.Close();
                        }
                    }
                    else
                    {
                        // Continue receiving data
                        client.socket.BeginReceive(client.buffer, 0, receiveBufferSize, SocketFlags.None, new AsyncCallback(ServerReceiveCallback), client);
                    } 
                }
            }
            catch
            {
                // ...
            }
		}

        delegate void SubmitMsg(ClientInfo sender, string message);

        private void SubmitMessage(ClientInfo sender, string message)
        {
            string name;

            lock (sender)
            {
                if (sender.name == "") return;
                name = sender.name;
            }

            AddMessageToHistory(name, message);
            SendMessageToAll(name, message);
        }

        private void SendMessageToAll(string name, string message)
        {
            SendToAll(CommandMessage, name + ":" + message);
        }

        private void SendToAll(string cmd, string par)
        {
            par = cmd + par + CommandTerminator;
            char[] msg = par.ToCharArray();
            int bytecount = Encoding.UTF8.GetEncoder().GetByteCount(msg, 0, msg.Length, true);
            byte[] convmsg = new byte[bytecount];
            Encoding.UTF8.GetEncoder().GetBytes(msg, 0, msg.Length, convmsg, 0, true);

            foreach (var item in clientList)
            {
                lock (item)
                {
                    item.socket.BeginSend(convmsg, 0, convmsg.Length, SocketFlags.None, new AsyncCallback(OnSendMessage), item);
                }
            }
        }

        private void OnSendMessage(IAsyncResult ar)
        {
            try
            {
                ClientInfo client = (ClientInfo)ar.AsyncState;
                lock (client)
                {
                    client.socket.EndSend(ar);
                }
            }
            catch
            {
                // ...
            }
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            if (AppStatus != AppState.NotSet) return;

            AppStatus = AppState.Client;

            IPAddress ip = null;

			// Try to acquire one IPv4 address of the target (server) host.
			try {
				IPAddress[] addrs;
				addrs = Dns.GetHostAddresses(tbxServerName.Text);

				// Try to find an IPv4 address
				foreach (IPAddress a in addrs) {
					if (a.AddressFamily == AddressFamily.InterNetwork) {
						ip = a;
						break;
					}
				}

				// No IPv4 addresses, so if possible get the first IPv6 address.
				// Note: Server socket is IPv4 only anyway - so this test is only for compatibility with future servers.
				if (ip == null && addrs.Length > 0 && addrs[0].AddressFamily == AddressFamily.InterNetworkV6) {
					ip = addrs[0];
				}
			} catch {
				// Do leave ip set to null.
			}

			if (ip == null) {
				// TO DO: Report error to user.
			}

			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = new ClientInfo(socket) {name = tbxUserName.Text};
			
            socket.BeginConnect(new IPEndPoint(ip, BasePort), new AsyncCallback(ConnectCallback), null);
            Connecting();
            Connected();
		}

        private void Connecting()
        {
            tbxUserName.Enabled = false;
            btnHostChat.Enabled = false;
            btnConnect.Enabled = false;
            tbxServerName.Enabled = false;
        }

        private void Connected()
        {
            tbxMessage.Enabled = true;
            btnSend.Enabled = true;
        }

		private void ConnectCallback(IAsyncResult ar) {
			try
            {
                lock (User)
                {
                    User.socket.EndConnect(ar);

                    char[] loginmsg = (CommandLogin + User.name + CommandTerminator).ToCharArray();
                    byte[] convmsg = new byte[Encoding.UTF8.GetEncoder().GetByteCount(loginmsg, 0, loginmsg.Length, true)];
                    Encoding.UTF8.GetEncoder().GetBytes(loginmsg, 0, loginmsg.Length, convmsg, 0, true);
                    User.socket.BeginSend(convmsg, 0, convmsg.Length, SocketFlags.None, new AsyncCallback(OnSendMessage), User);

                    User.socket.BeginReceive(User.buffer, 0, User.buffer.Length, SocketFlags.None, new AsyncCallback(ClientReceiveCallback), null); 
                }
            }
            catch
            {
                // ...
            } 
		}

        private void ClientReceiveCallback(IAsyncResult ar)
        {
            try
            {
                lock (User)
                {
                    int readedbytes = User.socket.EndReceive(ar);
                    char[] msg = new char[Encoding.UTF8.GetDecoder().GetCharCount(User.buffer, 0, readedbytes)];
                    Encoding.UTF8.GetDecoder().GetChars(User.buffer, 0, readedbytes, msg, 0, true);
                    User.stringBuffer.Append(msg);

                    string[] commands = User.stringBuffer.ToString().Split(CommandTerminator);

                    if (commands.Length > 0)
                    {
                        for (int i = 0; i < commands.Length - 1; i++)
                        {
                            var item = commands[i];

                            if (item.IndexOf(CommandMessage) == 0)
                            {
                                string name;
                                string message;
                                string proc = item.Remove(0, CommandMessage.Length);

                                if (proc.IndexOf(':') == -1) continue;
                                else
                                {
                                    name = proc.Substring(0, proc.IndexOf(':'));
                                    message = proc.Substring(proc.IndexOf(':') + 1);
                                    AddMessageToHistory(name, message);
                                }
                            }
                        }

                        User.stringBuffer = new StringBuilder(commands[commands.Length - 1]);
                    }
                    

                    User.socket.BeginReceive(User.buffer, 0, User.buffer.Length, SocketFlags.None, new AsyncCallback(ClientReceiveCallback), null); 
                }
            }
            catch
            {
                // ...
            }
        }


		//
		// Messages from server will be probably received and processed in other than the main thread,
		// so this situation needs to be correctly handled here.
		// If not: In Debug mode (not in Release!) you should always get HERE an exception in wrong thread.
		//

		private delegate void AddMessageDelegate(string userName, string message);

		private void AddMessageToHistory(string userName, string message) {
			if (InvokeRequired) {
				Invoke(new AddMessageDelegate(this.AddMessageToHistory), userName, message);
			} else {
				// Accessing a WinForms control always from the main thread.
				tbxHistory.Text += userName + " says:" + Environment.NewLine + "\t" + message + Environment.NewLine;
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
            try
            {
                if (AppStatus == AppState.Server)
                {

                    // Close server socket first - so that new clients cannot be asynchoronously accepted during
                    // the following stage of closing client sockets.
                    if (m_serverSocket != null) m_serverSocket.Close();
                    foreach (var item in clientList)
                    {
                        lock (item)
                        {
                            item.socket.Shutdown(SocketShutdown.Both);
                            item.socket.Close();
                        }
                    }
                }
                else if (AppStatus == AppState.Client)
                {
                    lock (User)
                    {
                        User.socket.Shutdown(SocketShutdown.Both);
                        User.socket.Close();
                    }
                }
                else
                {
                    // Do nothing
                }
            }
            catch
            {
                // ...
            }
		}

		private void btnSend_Click(object sender, EventArgs e) {
            string message = tbxMessage.Text;
            
            if (AppStatus == AppState.Server)
            {
                AddMessageToHistory(UserName, message);
                SendMessageToAll(UserName, message);
            }
            else if (AppStatus == AppState.Client)
            {
                char[] loginmsg = (CommandMessage + message + CommandTerminator).ToCharArray();
                byte[] convmsg = new byte[Encoding.UTF8.GetEncoder().GetByteCount(loginmsg, 0, loginmsg.Length, true)];
                Encoding.UTF8.GetEncoder().GetBytes(loginmsg, 0, loginmsg.Length, convmsg, 0, true);
                User.socket.BeginSend(convmsg, 0, convmsg.Length, SocketFlags.None, new AsyncCallback(OnSendMessage), User);
            }

            tbxMessage.Text = "";
		}
	}
}
