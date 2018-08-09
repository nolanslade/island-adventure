using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerTriggerBehaviour : MonoBehaviour {
	GameObject player;
	GameObject tigerOne;
	GameObject tigerTwo;
	public float groundHeight;
	public bool triggered = false;
	public bool triggerAnimationEnabled = false;
	public float speed;

	// Start
	void Start () {
		player = GameObject.FindWithTag("Player");
		tigerOne = GameObject.FindWithTag("Tiger1");
		tigerTwo = GameObject.FindWithTag("Tiger2");
	}


	// If the trap has been triggered, the tigers will start to chase the player
	public void trigger () {
		gameObject.GetComponent<AudioSource>().Play();
		triggered = true;
		tigerOne.GetComponent<Animator> ().Play ("run");
		tigerTwo.GetComponent<Animator> ().Play ("run");
	}

	public void untrigger () {
		triggered = false;
	}

	void FixedUpdate() {
		if (triggered) {
			GameObject tracker = new GameObject ();
			tracker.transform.position = new Vector3 (player.transform.position.x, groundHeight, player.transform.position.z);
			tigerOne.transform.LookAt (tracker.transform);
			tigerTwo.transform.LookAt (tracker.transform);
			tigerOne.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * speed);
			tigerTwo.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * speed);
		} else {
			tigerOne.GetComponent<Animator> ().Play ("idle");
			tigerTwo.GetComponent<Animator> ().Play ("idle");
		}
	}
}
