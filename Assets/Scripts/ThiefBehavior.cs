using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefBehavior : MonoBehaviour {

	public Transform enemyPolice;

	private Evade evade;

	private Rigidbody policeRigidBody;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody > ();
		policeRigidBody = enemyPolice.GetComponent <Rigidbody > ();
		evade = new Evade ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 movement = evade.getSteering (policeRigidBody);
		rb.MovePosition (/*transform.position + */ movement);
	}
}
