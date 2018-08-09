using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimations : MonoBehaviour {
	private GameObject tiger, character;
	public float timeBetweenAnimations;
	private float characterTime = 2.5f;
	private float tigerTime = 5.0f;
	public float characterAnimationDuration, tigerAnimationDuration;

	// Use this for initialization
	void Start () {
		character = GameObject.FindWithTag("Player");
		tiger = GameObject.FindWithTag("Tiger");
	}
	
	// Call animations depending on elapsed time
	void FixedUpdate () {
		// Character animates by itself
		if (characterTime >= timeBetweenAnimations) {
			character.GetComponent<Animation> ().Play ("Lumbering");
			characterTime = 0.0f;
		} else if (characterTime >= characterAnimationDuration) {
			character.GetComponent<Animation> ().Play ("Idle");
			characterTime += Time.deltaTime;
		} else {
			characterTime += Time.deltaTime;
		}

		// Tiger will animate by itself also
		if (tigerTime >= timeBetweenAnimations) {
			tiger.GetComponent<Animator> ().Play ("sound");
			tigerTime = 0.0f;
		} else if (tigerTime >= tigerAnimationDuration) {
			tiger.GetComponent<Animator> ().Play ("idle");
			tigerTime += Time.deltaTime;
		} else {
			tigerTime += Time.deltaTime;
		}
	}
}
