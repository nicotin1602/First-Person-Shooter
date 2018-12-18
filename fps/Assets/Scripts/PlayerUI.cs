using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject pickupSign;

    [SerializeField]
    private PlayerManager player;
	private PlayerController controller;

	public void SetPlayer(PlayerManager _player){
		player = _player;
		controller = player.GetComponent<PlayerController> ();
	}


	void SetHealthBar(float _ammount){
		healthBarFill.localScale = new Vector3 (_ammount, 1f, 1f);
	}

	void Start(){
		PauseMenu.IsOn = false;
		PickupSign.IsOn = false;
	}


	void Update () {
		SetHealthBar (player.getCurrentHealth());

		SetPickupSign (controller.canPickUp);

		if (Input.GetKeyDown (KeyCode.Escape)) {
			TogglePauseMenu ();
		}
		
	}

	void SetPickupSign (bool state){
		if (state) {
			pickupSign.SetActive (true);
			PickupSign.IsOn = pickupSign.activeSelf;
		} else {
			pickupSign.SetActive (false);
			PickupSign.IsOn = pickupSign.activeSelf;
		}
	}

	public void TogglePauseMenu (){
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;

	}
	public void TogglePickupSign (){
		Debug.Log ("Toggeling Sign");
		pickupSign.SetActive (!pickupSign.activeSelf);
		PickupSign.IsOn = pickupSign.activeSelf;
		

	}

    public void ShowDamage(int damage)
    {
        GameObject.Find("HitIndicator").transform.Find("Text").GetComponent<Text>().text = damage.ToString();
    }

}
