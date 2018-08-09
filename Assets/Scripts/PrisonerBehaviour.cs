using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerBehaviour : MonoBehaviour {
	public bool isFree;
	public float jumpPower;
	public bool isOnGround;

	// Use this for initialization
	void Start () {
		GetComponent<Animation> ().Play ("Idle");
	}
	
	// Manages when the prisoners will start jumping
	void FixedUpdate () {
		if (isOnGround) {
			jump ();
			isOnGround = false;
		}
	}

	void onCollisionEnter (Collision col) {
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
			isOnGround = true;
		}
	}

	// Prisoners will jump for joy when released
	void jump() {
		GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower);
	} 
}
