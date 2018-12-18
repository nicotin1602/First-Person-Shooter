using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {


    public Sprite icon = null;
    public GameObject handheldPrefab;
    public bool isDefaultItem = false;
    public int range = 1; //range of the weapon, area of damage(high)
    public int damage = 1; //how much damage
    public float frequenz; //repeat damage per second
    public float lifetime; //if 0 then destroy item, start by 100

}
