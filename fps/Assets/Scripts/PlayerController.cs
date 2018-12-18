using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    public int inventoryIndicator = 0;

    [SerializeField]
	private float normalSpeed = 3f;
	[SerializeField]
	private float sprintSpeed = 6f;

    [SerializeField]
	private float MouseSesitivity = 3f;
    [SerializeField]
    private float jumpForce = 100f;
    [SerializeField]
    private float sprintEnergy = 100f;

	[SerializeField]
	private float distance;

	[SerializeField]
	private LayerMask gmask;

	[SerializeField]
	private LayerMask imask;

	[SerializeField]
	private Camera cam;

	private const string Interacable_Tag = "Interactable";

	private Animator animator;

    private PlayerMotor motor;

	private Collider col;

    private Inventory inventory;

	private GameObject _item;

	private float movSpeed;
	private float oldSpeed;
	private float speed; 

	public bool canPickUp;

	public Interactable focus;
	private Interactable interactable;



	void Start () {
		motor = GetComponent<PlayerMotor> ();
		animator = GetComponent<Animator> ();
		col = GetComponent<Collider> ();
        
	}

	void Update (){

		if (PauseMenu.IsOn){
			if (Cursor.lockState != CursorLockMode.None) {
				Cursor.lockState = CursorLockMode.None;
			}

			motor.Move(Vector3.zero);
			motor.Rotate(Vector3.zero);
			motor.rotateCamera(new Vector3(0f,0f,0f));

			return;
		}



		if (Cursor.lockState != CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		speed = normalSpeed;
		if(Input.GetButton("Sprint")){
			speed = sprintSpeed;
		}


		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");
        float _wheel = Input.GetAxis("MouseWheel");

		movSpeed = _zMov * speed;
		if (movSpeed != oldSpeed) {
			animator.SetFloat ("Speed", movSpeed);
			oldSpeed = movSpeed;
		}

        if (_wheel != 0)
        {
            if (_wheel < 0 && inventoryIndicator >= 0 && inventoryIndicator <= 4)
            {
                inventoryIndicator = inventoryIndicator + 1;
            }
            else if (_wheel > 0 && inventoryIndicator >= 0 && inventoryIndicator <= 4)
            {
                inventoryIndicator = inventoryIndicator - 1;
            }

            if (inventoryIndicator < 0) { inventoryIndicator = 4; }
            if (inventoryIndicator > 4) { inventoryIndicator = 0; }

            inventory.select(inventoryIndicator);
        }

        Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

		motor.Move(_velocity);

		float _yRot = Input.GetAxisRaw ("Mouse X");

		Vector3 _rotation = new Vector3 (0f, _yRot * MouseSesitivity, 0f);

		motor.Rotate (_rotation);

		float _xRot = Input.GetAxisRaw ("Mouse Y");

		Vector3 _cameraRotation = new Vector3 (_xRot, 0f, 0f) * MouseSesitivity;

		motor.rotateCamera (-_cameraRotation);

        Vector3 _jumpForce = Vector3.zero;
        //Apply Jump force
		if (Input.GetButton("Jump") && GroundCheck()){
			_jumpForce = Vector3.up * jumpForce;
        }
        motor.applyJumpForce(_jumpForce);
		CheckForInteractable ();
	}

    public void setInventory(Inventory _inventory)
    {
        inventory = _inventory;
    }

	public bool getSignState(){
		return canPickUp;
	}

	void CheckForInteractable(){
		
		_item = null;
		RaycastHit _hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out _hit, 3f, imask)) {
			if (_hit.collider.tag == Interacable_Tag && !PickupSign.IsOn) {
				_item = _hit.collider.gameObject;
				canPickUp = true;
				interactable = _hit.collider.GetComponent<Interactable> ();
			}
		}else if (_item == null && PickupSign.IsOn) {
			canPickUp = false;
			if (focus != null) {
				focus.OnDefocused(transform);
			}
		}
			
		if (Input.GetKeyDown(KeyCode.F) && canPickUp && interactable != null){
			SetFocus (interactable);
		}
	}

	bool GroundCheck()
	{
		RaycastHit _hit;
		bool isGrounded;
		isGrounded = Physics.Raycast (transform.position, Vector3.down, out _hit, col.bounds.extents.y + distance, gmask);
		animator.SetBool ("jumping", isGrounded);
		return isGrounded;
	}

	void SetFocus(Interactable newFocus){

		if (newFocus != focus) {
			
			focus = newFocus;
		}
		if (focus != null) {
			focus.OnFocused(transform);
		}

	}





}
