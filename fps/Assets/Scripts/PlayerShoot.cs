using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public Inventory inventory;

    public Transform gameManager;
    private DamageScript _damageScript;

    private const string PLAYER_TAG = "Player";

    private int _damage;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	[SerializeField]
	float distance;

	[SerializeField]
	private LayerMask GroundMask;


    void Start()
	{
		if (cam == null) 
		{
			Debug.LogError ("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}

        inventory = GetComponentInChildren<Inventory>();

        _damageScript = GetComponent<DamageScript>();
    }

	void Update()
	{
		if (Input.GetButtonDown ("Fire1")) 
		{
            
			Shoot ();
		}
	}

	[Client]
	void Shoot(){
		RaycastHit _hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out _hit, mask)) {
			if (_hit.collider.tag == PLAYER_TAG) {
                distance = Vector3.Distance(cam.transform.position, _hit.transform.position);
                _damage =_damageScript.calculateDamage(inventory.handHeld, distance);
                CmdPlayerShot (_hit.collider.name, _damage);
            }
		}
        //inventory.handHeld.lifeTime = inventory.handHeld.lifeTime - 1;
        //wait frequenz???
    }

	[Command]
	void CmdPlayerShot(string _playerID, int _damage){
		Debug.Log (_playerID + " has been shot!");

		PlayerManager _player = GameManager.GetPlayer (_playerID);
		_player.RpcTakeDamage (_damage);
	}


}
