using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceHeadBehavior : MonoBehaviour {

	// Movement modifier applied to directional movement.
	public float maxPlayerSpeed = 2.0f;

	Vector3 movement;

	// What the current speed of our player is
	private float currentSpeed = 0.0f;

	int floorMask;
	float camRayLength = 100.0f;

	/*
	 * Allows us to have multiple inputs and supports keyboard, 
	 * joystick, etc.
	 */
	public List<KeyCode> upButton;
	public List<KeyCode> downButton;
	public List<KeyCode> leftButton;
	public List<KeyCode> rightButton;

	// The last movement that we've made
	private Vector3 lastMovement = new Vector3();

	private Rigidbody rb;

	// float camRayLength = 100.0f;

	void Awake () {
		floorMask = LayerMask.GetMask ("Floor");
		rb = GetComponent <Rigidbody > ();
	}







	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		float moveVertical = -Input.GetAxisRaw ("Horizontal");
		float moveHorizontal = Input.GetAxisRaw ("Vertical");

		movement.Set (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * maxPlayerSpeed);
		movement = movement.normalized * maxPlayerSpeed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);

		Turning ();
	}

	void Turning () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rb.MoveRotation (newRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate player to face mouse
		// Rotation ();
		// Move the player's body
		// Movement ();
	}

	// Will rotate the ship to face the mouse.
	void Rotation()
	{
		// We need to tell where the mouse is relative to the 
		// player
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(worldPos);

		/*
	   * Get the differences from each axis (stands for 
	   * deltaX and deltaY)
	   */
		float dx = this.transform.position.x - worldPos.x;
		float dy = this.transform.position.z - worldPos.z;

		// Get the angle between the two objects
		float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

		/* 
	    * The transform's rotation property uses a Quaternion, 
	    * so we need to convert the angle in a Vector 
	    * (The Z axis is for rotation for 2D).
	  */
		Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

		// Assign the ship's rotation
		this.transform.rotation = rot;
	}

	// Will move the player based off of the input key.
	void Movement() {
		// the movement that needs to occur this frame
		Vector3 movement = new Vector3 ();

		// Check for input
		movement += MoveIfPressed (upButton, Vector3.up);
		movement += MoveIfPressed (downButton, Vector3.down);
		movement += MoveIfPressed (leftButton, Vector3.left);
		movement += MoveIfPressed (rightButton, Vector3.right);

		/*
		 * If we pressed multiple buttons, make sure we're only
		 * moving the same length
		 */
		movement.Normalize ();

		// check if we pressed anything
		if (movement.magnitude > 0) {
			// If we did, move in that direction
			currentSpeed = maxPlayerSpeed;
			this.transform.Translate (movement * Time.deltaTime * maxPlayerSpeed, Space.World);
			lastMovement = movement;
		} else {
			// otherwise move in the direction we were going.
			this.transform.Translate (lastMovement * Time.deltaTime * currentSpeed, Space.World);

			// slow down over time
			currentSpeed *= 0.9f;
		}
	}

	/*
	 * Will return the movement if any of the key is pressed,
	 * otherwise it will return (0, 0, 0)
	 */
	Vector3 MoveIfPressed (List<KeyCode> keyList, Vector3 Movement) {
		// check each key in our list
		foreach (KeyCode element in keyList) {
			if (Input.GetKey (element)) {
				/*
				 * It was pressed, so we leave the function
				 * with the movement applied
				 */
				return Movement;
			}
		}
		return Vector3.zero;
	}
}
