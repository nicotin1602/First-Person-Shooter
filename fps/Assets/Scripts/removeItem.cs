using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class removeItem : NetworkBehaviour {

    [Command]
    public void CmdItemPickedUp(GameObject _item)
    {
        Debug.Log(_item.name + " has been picked up!");
        NetworkServer.Destroy(_item);


    }

}
