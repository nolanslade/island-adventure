using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour {

	public float movementSpeed;				// How fast the pillars will move up and down
	public float minX;						// The minimum height of the pillar
	public float maxX;						// The maximum height of the pillar
	public bool movingDifferentDirection;

	// FixedUpdate Used to move the platforms side to side
	void FixedUpdate () {
		// Move the platform side to side using ping pong
		transform.position = new Vector3 (PingPong (Time.time * movementSpeed, maxX, minX), transform.position.y, transform.position.z);
	}

	float PingPong (float t, float min, float max) {
		return Mathf.PingPong (t, max-min) + min;
	}
}
