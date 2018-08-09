using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWaterBehaviour : MonoBehaviour {
	private float movementSpeed;	// How fast the will will move up and down
	public float minHeight;		// The minimum height of the water
	public float maxHeight;		// The maximum height of the water

	void Start () {
		movementSpeed = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().WaterSpeed();
	}

	// FixedUpdate used to move the water up and down
	void FixedUpdate () {
		// Move the water up and down using the ping pong method
		transform.position = new Vector3(transform.position.x, PingPong(Time.time*movementSpeed, minHeight, maxHeight), transform.position.z);
	}

	float PingPong (float t, float min, float max) {
		return Mathf.PingPong (t, max-min) + min;
	}
}
