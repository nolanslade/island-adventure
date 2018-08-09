using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRotateAnimation : MonoBehaviour {

	public float rotationSpeed;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (-Vector3.up * rotationSpeed * Time.deltaTime);
	}
}
