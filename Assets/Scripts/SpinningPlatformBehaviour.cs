using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatformBehaviour : MonoBehaviour {

	public float rotationSpeed;
	private float multiplier;

	void Start () {
		multiplier = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().SpinMultiplier();
	}

	// FixedUpdate Used rotate the platforms
	void FixedUpdate () {
		transform.Rotate (Vector3.right * rotationSpeed*multiplier * Time.deltaTime);
	}
}
