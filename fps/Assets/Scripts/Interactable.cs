using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 1f;
	public Transform interactionTransform;

	bool isFocused = false;
	Transform player;

	bool hasInteracted = false;


	public virtual void Interact(){
	}

	void Update(){
		if (isFocused && !hasInteracted) {
			float distance = Vector3.Distance (player.position, transform.position);
			if (distance <= radius) {
				Interact ();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused (Transform playerTransform){
		isFocused = true;
		player = playerTransform;
		hasInteracted = false;
	}
	public void OnDefocused (Transform playerTransform){
		isFocused = false;
		player = null;
		hasInteracted = false;
	}

	void OnDrawGizmosSelected(){

		if (interactionTransform == null) {
			interactionTransform = transform;
		}
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, radius);
	}

}
