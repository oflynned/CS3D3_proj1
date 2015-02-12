using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient() {
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];

        // Connect to a remote device.
        try {
            // Establish the remote endpoint for the socket.
            // This example uses port 11000 on the local computer.
            int port = 8080;
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress,port);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp );

            // Connect the socket to the remote endpoint. Catch any errors.
            try {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());
                Console.WriteLine("connected to socket");
                System.Threading.Thread.Sleep(3000);

                //byte[] msg;
                //byte[] line = Console.ReadLine();
                //string msg = string.Parse(line);
                //msg = Encoding.ASCII.GetBytes(msg.ToString());


                // Encode the data string into a byte array.
                //read in user defined data
               // string line = Console.ReadLine();
               // byte[] msg = byte[].Parse(line);

                byte[] msg = Encoding.ASCII.GetBytes("Such test very socket much byte wow <EOF>");
                Console.WriteLine("encoded message to bytes");
                System.Threading.Thread.Sleep(3000);

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);
                Console.WriteLine("sent to socket");
                System.Threading.Thread.Sleep(3000);

                // Receive the response from the remote device.
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes,0,bytesRec));
                Console.WriteLine("received response from server");
                System.Threading.Thread.Sleep(3000);

                // Release the socket.
                Console.WriteLine("about to shut down");
                System.Threading.Thread.Sleep(3000);
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                
            } catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
            } catch (SocketException se) {
                Console.WriteLine("SocketException : {0}",se.ToString());
            } catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        } catch (Exception e) {
            Console.WriteLine( e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        bool done = false;
        while (!done) { 
        Console.WriteLine("Do you want to send a message?");
        string question = Console.ReadLine();
        switch (question)
            {
                case ("yes"):
                case ("y"):
                    StartClient();
                    break;

                case ("no"):
                case ("n"):
                    done = true;
                    break;
            }
        }
        return 0;
    }
}