using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingLogBehaviour : MonoBehaviour {

	public float rotationSpeed;	// How fast the log will spin around
	private float multiplier;

	void Start () {
		multiplier = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().SpinMultiplier();
	}

	// Spin the log here in fixed update
	void FixedUpdate () {
		// Rotate log
		transform.Rotate (-Vector3.up * rotationSpeed*multiplier * Time.deltaTime);
	}
}
