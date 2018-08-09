using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningCrossPlatform : MonoBehaviour {

	public bool flipped;

	// Update is called once per frame
	void FixedUpdate () {
		if (!flipped)
			transform.Rotate (-Vector3.up * 20 * Time.deltaTime);
		else 
			transform.Rotate (Vector3.forward * 20 * Time.deltaTime);
	}
}
