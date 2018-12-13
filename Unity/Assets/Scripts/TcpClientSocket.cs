using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour
{

    TcpClient mSocket;
    public NetworkStream mStream;
    StreamReader mReader;
    StreamWriter mWriter;
    bool socketReady = false;
    public Int32 port = 5001;
    public String host = "Local IP of Arduino";
    public bool lightStatus;


    void Start()
    {
        setupSocket();
    }

    void Update()
    {
        //testing only
        while (mStream.DataAvailable){                 
            string dataReceived = readSocket();

            if(dataReceived == "Light on"){            
                lightStatus = true;
            }

            if(dataReceived == "Light off"){
                lightStatus = false;
            }
        }
    }

    public void setupSocket(){
        try
        {
            mSocket = new TcpClient(host, port);
            mStream = mSocket.GetStream();
            mWriter = new StreamWriter(mStream);
            mReader = new StreamReader(mStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);
        }
    }

    public void writeSocket(string str)
    {
        if (!socketReady){
            return;
        }
        String tmpString = str;
        mWriter.Write(tmpString);
        mWriter.Flush();


    }

    public String readSocket()
    {
        if(!socketReady){
            return "";
        }
        if(mStream.DataAvailable){
            return mReader.ReadLine();
        }
        return "NoData";
    }

    public void closeSocket()
    {
        if (!socketReady){
            return;
        }
        mWriter.Close();
        mReader.Close();
        mSocket.Close();
        socketReady = false;
    }
}