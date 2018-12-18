using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour {

    [SerializeField]
    public Item handHeld;

    private GameObject inHand;
    public Transform ownerInventory;
    public Transform Owner;
    public Transform ownerCamera;


	public static Inventory instance;

	void Awake (){
		if (instance != null) {
			//Debug.LogWarning ("More than one instance of Inventory Found");
		}
		instance = this;
	}

	public delegate void OnItemChanged();

	public OnItemChanged onItemChangedCallback;

	public int space = 5;

	public List<Item> items = new List<Item>();

    private PlayerController controller;

    private void Start()
    {
        ownerInventory = GetComponent<Transform>();
        Owner = ownerInventory.parent.GetComponent<Transform>();
        ownerCamera = Owner.GetChild(0);
        controller = GetComponentInParent<PlayerController>();
        controller.setInventory(this);
    }

    public bool Add (Item item){

		if (!item.isDefaultItem) {
			if (items.Count >= space) {
				Debug.LogWarning ("Not Enough space in Inventory");
				return false;
			}
			items.Add(item);
			if (onItemChangedCallback != null) {
				onItemChangedCallback.Invoke ();
			}
		}
		return true;
	}

    public void select (int inventoryNumber)
    {
        Debug.Log("Selecting item nr." + inventoryNumber );
        if (inventoryNumber < items.Count && items[inventoryNumber] != null)
        {
            if (inHand != null)
            {
                Destroy(inHand);
            }
            handHeld = items[inventoryNumber];
            inHand = Instantiate(handHeld.handheldPrefab, ownerCamera);
        }
    }

	public void Remove (Item item){
		items.Remove(item);
	}


}
