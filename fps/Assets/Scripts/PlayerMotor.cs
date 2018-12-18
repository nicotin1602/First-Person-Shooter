using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {


	[SerializeField]
	private Camera cam;


	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;


	private Rigidbody rb;

	public Animator animator;


	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 _velocity){
		velocity = _velocity;
	}
	public void Rotate(Vector3 _rotation){
		rotation = _rotation;
	}
	public void rotateCamera(Vector3 _cameraRotation){
		cameraRotation = _cameraRotation;
	}
    public void applyJumpForce(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
    }

	void FixedUpdate(){
		PerformMovement();
		PerformRotation();

	}

	void PerformMovement(){
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);

		}
		if (jumpForce != Vector3.zero)
        {
			rb.AddForce (jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
		}
	}

	void PerformRotation(){
		rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));

		if (cam != null) {
			cam.transform.Rotate(cameraRotation); 
		}
			
	}







}
