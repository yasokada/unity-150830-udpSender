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

	void Start() {
		IFipadr.text = "192.168.10.";
		IFport.text = "6000";
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

	void udpSend() {
		UdpClient client;
		int port = getPort();
		string ipadr = getIpadr ();

		client = new UdpClient ();
		client.Connect (ipadr, port);

		string sendstr = IFmsg.text + System.Environment.NewLine;
		byte[] data = ASCIIEncoding.ASCII.GetBytes (sendstr);
		client.Send (data, data.Length);
		client.Close ();
	}

	public void onClick() {
		Debug.Log ("on click");
		udpSend ();
	}
}
