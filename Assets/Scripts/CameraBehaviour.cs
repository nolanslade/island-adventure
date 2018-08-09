using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public float cameraRotationSpeed;

	// Allow the user to interact with the camera angle in FixedUpdate
	void FixedUpdate () {

		// W and S keys used to raise and lower camera, respectively
		if (Input.GetKey(KeyCode.W)) {
			//transform.rotation = new Vector3(transform.rotation.x + cameraRotationSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + cameraRotationSpeed * Time.deltaTime, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		} else if (Input.GetKey(KeyCode.S)) {
			//transform.rotation = new Vector3(transform.rotation.x - cameraRotationSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - cameraRotationSpeed * Time.deltaTime, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		}
	}
}
