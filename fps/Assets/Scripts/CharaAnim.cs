using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnim : MonoBehaviour {

	public void setWaiting(){
		gameObject.GetComponent<Animation> ().CrossFade ("Wait", 0.1f);
	}
	public void setWalking(){
		gameObject.GetComponent<Animation> ().CrossFade ("Walk", 0.1f);
	}
	public void setSprinting(){
		gameObject.GetComponent<Animation> ().CrossFade ("Sprint", 0.1f);
	}
	public void setNearFighting(){
		gameObject.GetComponent<Animation> ().CrossFade ("NearAttack", 0.1f);
	}

}
