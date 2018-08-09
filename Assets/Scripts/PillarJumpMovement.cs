using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarJumpMovement : MonoBehaviour {

	public float movementSpeed;	// How fast the pillars will move up and down
	private float multiplier;
	public float minHeight;		// The minimum height of the pillar
	public float maxHeight;		// The maximum height of the pillar
		
	void Start () {
		multiplier = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().PoleMovementMultiplier();
	}

	// FixedUpdate Used to oscillate the pillars
	void FixedUpdate () {
		// Move the pillar up and down using the ping pong method
		transform.position = new Vector3(transform.position.x, PingPong(Time.time*movementSpeed*multiplier, minHeight, maxHeight), transform.position.z);
	}

	float PingPong (float t, float min, float max) {
		return Mathf.PingPong (t, max-min) + min;
	}
}
