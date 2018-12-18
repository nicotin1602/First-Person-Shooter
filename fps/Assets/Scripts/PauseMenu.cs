using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PauseMenu : MonoBehaviour {

	public static bool IsOn = false;

	private NetworkManager networkManager;

	public void leaveRoom(){
		//networkManager.
	}

	public void Start(){
		networkManager = NetworkManager.singleton;
	}

}
