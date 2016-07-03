#define _DEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

public class Client
{
	static readonly Client mInstance = new Client();
	public static Client instance {
		get {
			return mInstance;
		}
	}

	private struct asd {
		string recv_str;
		int soc_num;
	}

	private Socket mSocket;
	private Socket mCbSocket;

	// 220.69.74.191/30000 : 외부 서버(hadado server), 112/58094 : 외부 서버(soda server)
	#if _DEBUG
	public string ipAddress = "192.168.0.17";
	#else
	public string ipAddress = "220.69.74.191";
	#endif
	public const int kPort = 30000;

	//   private int sendDataLength;                     // Send Data Length. (byte)
	//   private int receiveDataLength;                     // Receive Data Length. (byte)

	private byte[] sendByte;                        // Data encoding to send. ( to Change bytes)
	private byte[] receiveByte = new byte[2000];    // Receive data by this array to save.
	//   private string receiveString;                     // Receive bytes to Change string.

	private Queue<string> mRecvQueue;
	public Queue<string> recvQueue {
		get {
			return mRecvQueue;
		}
	}

	public void init () {
		mRecvQueue = new Queue<string> ();
		mRecvQueue.Clear ();

		mSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // tcp 통신 개방
		mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5000);
		mSocket.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
		beginConnect();

		Debug.Log ("Client initialized");
	}

	public void beginConnect () {
		try {
			mSocket.BeginConnect (ipAddress, kPort, new AsyncCallback(connectCallback), mSocket);
		}
		catch(SocketException ex) {
			// connect error
			Debug.Log ("connection fail. " + ex.NativeErrorCode);
			init ();
		}
	}

	public void beginSend(string _message) {
		try {
			if (mSocket.Connected) {
				sendByte = Encoding.Default.GetBytes (_message);
				mSocket.BeginSend (sendByte, 0, sendByte.Length, SocketFlags.None, new AsyncCallback(sendCallback), _message);
			}
		}
		catch (SocketException ex) {
			Debug.Log ("Send error : " + ex.Message);
			// send error
		}
	}

	// 콜백 재등록
	public void receive() {
		mCbSocket.BeginReceive (receiveByte, 0, receiveByte.Length, SocketFlags.None, new AsyncCallback (onReceiveCallback), mCbSocket);
	}

	#region callback
	private void connectCallback(IAsyncResult _iar) {
		try {
			Socket tempSocket = _iar.AsyncState as Socket;
			IPEndPoint svrEP = tempSocket.RemoteEndPoint as IPEndPoint;

			Debug.Log (svrEP.Address + " connection success.");

			tempSocket.EndConnect(_iar);
			mCbSocket = tempSocket;
			mCbSocket.BeginReceive (receiveByte, 0, receiveByte.Length, SocketFlags.None, new AsyncCallback(onReceiveCallback), mCbSocket);
		}
		catch(SocketException ex) {
			// connect error
			if (ex.SocketErrorCode == SocketError.NotConnected) {
				Debug.Log ("Connection fail Callback " + ex.Message);
				beginConnect ();
			}
		}
	}

	private void sendCallback(IAsyncResult _iar) {
		string message = _iar.AsyncState as string;
		Debug.Log ("Send success Callback : " + message);
	}

	private void onReceiveCallback(IAsyncResult _iar) {
		try {
			Socket tempSocket = _iar.AsyncState as Socket;
			int readSize = tempSocket.EndReceive(_iar);
			if (readSize != 0) {
				string message = Encoding.Default.GetString (receiveByte);
				Debug.Log ("Receive : " + message);

				// Queue에 저장
				mRecvQueue.Enqueue (message);
				// 받은 메시지 여기서 처리
				//GameManager.instance.socket.receive(message);
			}
			receive ();
		}
		catch (SocketException ex) {
			if (ex.SocketErrorCode == SocketError.ConnectionReset)
				beginConnect ();
		}
	}
	#endregion

	void OnApplicationQuit ()
	{
		if (mSocket.Connected) {
			mSocket.Close ();
			mSocket = null;
		}
		if (mCbSocket.Connected) {
			mCbSocket.Close ();
			mCbSocket = null;
		}
	}
}