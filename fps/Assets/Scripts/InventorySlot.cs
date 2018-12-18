
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	Item item;

	public void AddItem(Item newItem){
		Debug.Log("Adding Item");
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void ClearSlot (){

		Debug.Log("Removing Item");
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

}
