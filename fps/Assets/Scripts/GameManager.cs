
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public static GameManager instance;

	public MatchSettings matchSettings;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("A Manager exists already!!!");
        }
        else
        {
            instance = this;
        }
    }

    [Command]
    public void CmdItemPickedUp(GameObject _item)
    {
        Debug.Log(_item.name + " has been picked up!");
        NetworkServer.Destroy(_item);


    }

    #region Player.tracking




    private const string PLAYER_ID_PREFIX = "Player ";

	private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

	public static void RegisterPlayer (string _netID, PlayerManager _player){
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add (_playerID, _player);
		_player.transform.name = _playerID;
		Debug.Log ("registered Player " + _player);
	}

	public static void unRegisterPlayer(string _playerID){
		players.Remove (_playerID);
	}

	public static PlayerManager GetPlayer (string _playerID){
		return players [_playerID];
	}

//	void OnGUI(){
//		GUILayout.BeginArea(new Rect(200, 200,200,500));
//		GUILayout.BeginVertical ();
//
//		foreach (string _playerID in players.Keys) {
//			GUILayout.Label (_playerID + "  -  " + players [_playerID].transform.name);
//		}
//		GUILayout.EndVertical();
//		GUILayout.EndArea ();
//
//	}

	#endregion
}
