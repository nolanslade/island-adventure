using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour {

	public float movementSpeed;	// How fast the key will move up and down
	public float rotationSpeed;	// How fast the key will spin around
	public float minHeight;		// The minimum height of the key
	public float maxHeight;		// The maximum height of the key

	// FixedUpdate used to move the key up and down, plus spin it
	void FixedUpdate () {
		// Rotate key
		transform.Rotate (-Vector3.up * rotationSpeed * Time.deltaTime);

		// Move the key up and down using the ping pong method
		transform.position = new Vector3(transform.position.x, PingPong(Time.time*movementSpeed, minHeight, maxHeight), transform.position.z);
	}

	float PingPong (float t, float min, float max) {
		return Mathf.PingPong (t, max-min) + min;
	}
}
