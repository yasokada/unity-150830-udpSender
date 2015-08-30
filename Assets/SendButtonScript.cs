using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/*
 * v0.1 2015/08/30
 *   - only send w/o try catch
 */

public class SendButtonScript : MonoBehaviour {

	public InputField IFipadr;
	public InputField IFport;
	public InputField IFmsg; // message to send
	public Text rcvText; // recieved text

	UdpClient client;
	int port;
	string lastRcvd;

	void Start() {
		IFipadr.text = "192.168.10.";
		IFport.text = "6000";
	}

	void Update() {
		rcvText.text = lastRcvd;
	}

	string getIpadr() {
		return IFipadr.text;
	}
	int getPort() {
		int res = Convert.ToInt16 (IFport.text);
		if (res < 0) {
			res = 0;
		}
		return res;
	}

	void procComm() {
		port = getPort();
		string ipadr = getIpadr ();

		client = new UdpClient ();
		client.Connect (ipadr, port);

		// send
		string sendstr = IFmsg.text + System.Environment.NewLine;
		byte[] data = ASCIIEncoding.ASCII.GetBytes (sendstr);
		client.Send (data, data.Length);

		// receive
		IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, 0);
		data = client.Receive (ref remoteIP);
		string text = Encoding.ASCII.GetString(data);
		lastRcvd = text;				

		client.Close ();
	}
	
	public void onClick() {
		Debug.Log ("on click");
		procComm ();
	}
}
