using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	[SerializeField]
	GameObject playerUIPrefab;
	private GameObject playerUIInstance;

    [SerializeField]
    GameObject playerInventoryPrefab;
    private GameObject playerInventoryInstance;


	Camera sceneCamera;

	void Start()
	{
		if (!isLocalPlayer)
		{
			DisableComponents ();
			AssignRemoteLayer ();
		} 
		else
		{
			sceneCamera = Camera.main;
			if (sceneCamera != null) 
			{
				sceneCamera.gameObject.SetActive (false);
			}

			playerUIInstance = Instantiate (playerUIPrefab,transform);
            playerUIInstance.name = gameObject.name + "UI";

            playerInventoryInstance = Instantiate(playerInventoryPrefab, transform);
            playerInventoryInstance.name = gameObject.name + "Inventory";

			//Configure player UIU
			PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
			if (ui == null) {
				Debug.LogError ("No Player UI!!!");
			}
			ui.SetPlayer(GetComponent<PlayerManager>());
        }

		GetComponent<PlayerManager>().Setup();
	}

	public override void OnStartClient(){
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		PlayerManager _player = GetComponent<PlayerManager> ();


		GameManager.RegisterPlayer (_netID, _player);

	}

	void AssignRemoteLayer(){
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents(){
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		}
	}

	void OnDisable(){

		Destroy (playerUIInstance);

		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
		GameManager.unRegisterPlayer (transform.name);


	}
}
