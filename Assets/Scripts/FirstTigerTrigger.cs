using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTigerTrigger : MonoBehaviour {
	GameObject player;
	GameObject tiger;
	GameObject tiger2;
	GameObject tracker;
	public bool second;
	public float groundHeight;
	public bool hasTwoTigers;
	public bool triggered = false;
	public float baseSpeed;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().TigerSpeed();
		tracker = new GameObject ();

		if (!hasTwoTigers) {
			player = GameObject.FindWithTag("Player");

			if (second) {
				tiger = GameObject.FindWithTag ("Tiger2");
			} else {
				tiger = GameObject.FindWithTag ("Tiger1");
			}
		} else {
			player = GameObject.FindWithTag("Player");
			tiger = GameObject.FindWithTag ("Tiger1");
			tiger2 = GameObject.FindWithTag ("Tiger2");
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!hasTwoTigers) {
			if (triggered) {
				tracker.transform.position = new Vector3 (player.transform.position.x, groundHeight, player.transform.position.z);
				tiger.transform.LookAt (tracker.transform);
				tiger.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * baseSpeed* speed);
			} else {
				tiger.GetComponent<Animator> ().Play ("idle");
			}
		} else {
			if (triggered) {
				tracker.transform.position = new Vector3 (player.transform.position.x, groundHeight, player.transform.position.z);
				tiger.transform.LookAt (tracker.transform);
				tiger.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * baseSpeed* speed);
				tiger2.transform.LookAt (tracker.transform);
				tiger2.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * baseSpeed* speed);
			} else {
				tiger.GetComponent<Animator> ().Play ("idle");
				tiger2.GetComponent<Animator> ().Play ("idle");
			}
		}
	}

	// Trigger the tiger to attack
	public void trigger () {
		if (!hasTwoTigers) {
			if (!triggered) {
				gameObject.GetComponent<AudioSource> ().Play ();
				triggered = true;
				tiger.GetComponent<Animator> ().Play ("run");
			}
		} else {
			if (!triggered) {
				gameObject.GetComponent<AudioSource> ().Play ();
				triggered = true;
				tiger.GetComponent<Animator> ().Play ("run");
				tiger2.GetComponent<Animator> ().Play ("run");
			}
		}
	}

	// Tiger will stop attacking once you leave the attack zone
	public void untrigger () {
		if (triggered) {
			triggered = false;
		}
	}
}
