using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerCustom : NetworkManager {

	public void StartupHost(){
		Debug.Log ("Host Called");
		SetPort ();
		NetworkManager.singleton.StartHost();
	}

	public void JoinGame(){
		Debug.Log ("client Called");
		SetIPAdress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();

	}

	void SetIPAdress(){
		string ipAdress = GameObject.Find ("InputFieldIPAdress").transform.Find ("Text").GetComponent<Text> ().text;
		Debug.Log ("IP: " + ipAdress);
		NetworkManager.singleton.networkAddress = ipAdress;
	}


	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}
}
