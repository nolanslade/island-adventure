using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetector : MonoBehaviour {

	public GameObject player;
	private bool isGrounded = false;

	// Returns whether or not the player can jump
	public bool isOnGround() {
		return isGrounded;
	}

	// While the ground detector is touching ground, the player may jump
	void OnTriggerStay (Collider col) {
		// Water should not enable jumping
		if (!(col.gameObject.layer == LayerMask.NameToLayer("Water_Hazard") || col.gameObject.layer == LayerMask.NameToLayer("No_Jump") || col.gameObject.layer == LayerMask.NameToLayer("Pickup_Particle") || col.gameObject.layer == LayerMask.NameToLayer("Particle"))) {
			isGrounded = true;
		}
	}

	// If trigger is cancelled, then we're in the air
	void OnTriggerExit (Collider col) {
		isGrounded = false;
	}
}
