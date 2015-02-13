﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // Incoming data from the client.
    public static string data = null;

    public static void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];
        int port = 4547;

        // Establish the local endpoint for the socket.
        // Dns.GetHostName returns the name of the 
        // host running the application.
        //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //IPAddress ipAddress = ipHostInfo.AddressList[0];
        //IPEndPoint localEndPoint = new IPEndPoint(IPAddress, port);

        TcpListener serverSocket = new TcpListener(port);
        int reqCount = 0;
        TcpClient clientSocket = default(TcpClient);
        serverSocket.Start();

        //Console.WriteLine("server initiated");
        //clientSocket = serverSocket.AcceptTcpClient();
        //Console.WriteLine("client connected");
        reqCount = 0;

        // Create a TCP/IP socket.
        //Socket listener = new Socket(AddressFamily.InterNetwork,
        //  SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.
        try
        {
            while (true)
            {
                //increment request counter
                reqCount++;
                Console.WriteLine("server initiated");
                //accept incoming connection
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("client connected");

                //buffer
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new Byte[10025];

                //read in length of bytes to be received
                int bytesRead = networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                Console.WriteLine(bytesRead);

                //read in bytes to string
                string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, bytesRead);
                Console.WriteLine("data: " + dataFromClient);
                string serverResponse = "data from client: " + dataFromClient;

                //write data received to a txt file 
                System.IO.File.WriteAllText(@"C:\Users\Ed\Documents\Visual Studio 2013\Projects\CS3D3_proj1\Data\dataReceived.txt", dataFromClient);

                Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);

                //handle response and flush connection
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
                Console.WriteLine("server response: " + serverResponse);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}