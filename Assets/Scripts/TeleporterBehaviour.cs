using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBehaviour : MonoBehaviour {

	public float destinationX, destinationY, destinationZ;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (-Vector3.up * 75* Time.deltaTime);
	}

	public Vector3 getDestVector () {	
		// Returns the destination of the teleporter
		return new Vector3(destinationX, destinationY, destinationZ);
	}
}
