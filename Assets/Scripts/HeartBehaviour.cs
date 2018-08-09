using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour {

	public float movementSpeed;	// How fast the heart will move up and down
	public float rotationSpeed;	// How fast the heart will spin around
	public float minHeight;		// The minimum height of the heart
	public float maxHeight;		// The maximum height of the heart
	public int lifeAmt;			// How many lives a given object restores

	// FixedUpdate used to move the heart up and down, plus spin it
	void FixedUpdate () {
		// Rotate heart
		transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);

		// Move the key up and down using the ping pong method
		transform.position = new Vector3(transform.position.x, PingPong(Time.time*movementSpeed, minHeight, maxHeight), transform.position.z);
	}

	float PingPong (float t, float min, float max) {
		return Mathf.PingPong (t, max-min) + min;
	}
}