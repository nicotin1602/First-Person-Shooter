using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(removeItem))]
public class ItemPickup : Interactable {

	public Item item;

    public override void Interact(){
        base.Interact ();
        PickUp();
	}

	void PickUp(){
		bool wasPickedUp = Inventory.instance.Add (item);
        if (wasPickedUp) {
            GameManager.instance.CmdItemPickedUp(gameObject);
        }
	}

    
}
