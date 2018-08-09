using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDeathTrap : MonoBehaviour {

	public bool spinningOtherWay;
	private float multiplier;

	void Start () {
		multiplier = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().SpinMultiplier();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (spinningOtherWay) {
			transform.Rotate (Vector3.forward * 80*multiplier * Time.deltaTime);
		} else {
			transform.Rotate (-Vector3.forward * 80*multiplier * Time.deltaTime);
		}
	}
}
