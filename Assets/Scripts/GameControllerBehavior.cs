using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerBehavior : MonoBehaviour {

	public Transform bank;
	public Transform baseCamp;

	public Transform policeOfficer;
	public Transform policeHead;
	public Transform thief;

	public int numberOfPolice;
	public int totalThieves;
	public int freeThieves;
	public int capturedThieves;

	// Use this for initialization
	void Start () {
		Debug.Log (bank.position + "   " + baseCamp.position);
		for (int i = 0; i < numberOfPolice; i++) {
			float randX = Random.Range (-260.0f, -250.0f);
			float randZ = Random.Range (514.0f, 540.0f);
			Instantiate (policeOfficer, new Vector3 (randX, 249.64f, randZ), this.transform.rotation);
		}
		for (int i = 0; i < totalThieves; i++) {
			float randX = Random.Range (30.0f, 40.0f);
			float randZ = Random.Range (514.0f, 540.0f);
			Instantiate (thief, new Vector3 (randX, 249.64f, randZ), this.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
