﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour {

	[SyncVar]
	private bool _isDead = false;
	public bool isDead {
		get { return _isDead; }
		protected set { _isDead = value; }
	}
	
	[SerializeField]
	private int maxHealth = 100;

	[SerializeField]
	private int regenHealth = 100;

	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;

	[SyncVar]
	private int currentHealth;

    public GameObject damageTextPrefab;

	public float getCurrentHealth(){
		if (currentHealth <= 0) {
			currentHealth = 0;
		}
		return (float) currentHealth / maxHealth;
	}

	public void Setup(){
		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled [i] = disableOnDeath [i].enabled;
		}

		SetDefaults ();
	}

	void Update(){
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.K)) {
			RpcTakeDamage (40);
		}
	}

	[ClientRpc]
	public void RpcTakeDamage(int _amount){

		if (isDead)
			return;

		currentHealth -= _amount;

        showDamage(_amount);

		Debug.Log (transform.name + " now has " + currentHealth + " health");

		if (currentHealth <= 0) {
			Die ();
		}
	}
	private void Die(){
		isDead = true;

		//Disable Components
		for (int i = 0; i < wasEnabled.Length; i++) {
			disableOnDeath [i].enabled = false;
		}

		Collider _col = GetComponent<Collider> ();

		if (_col != null)
			_col.enabled = false;

		Debug.Log (transform.name + " is Dead");

		// Call Respwan
		StartCoroutine(Respawn());

	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds (GameManager.instance.matchSettings.respawnTime);

		SetDefaults ();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;

		Debug.Log (transform.name + " respawned!");

	}

	public void SetDefaults () {

		isDead = false;

		currentHealth = maxHealth;
		for (int i = 0; i < wasEnabled.Length; i++) {
			disableOnDeath [i].enabled = wasEnabled [i];
		}

		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = true;
	}

    void showDamage(int _amount)
    {
        GameObject indicator = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        indicator.transform.Find("Text").GetComponent<TextMesh>().text = _amount.ToString();
    }

}
