using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (GameObject.FindGameObjectWithTag("heartmanager"));
	}

	public void reClear() {
		Destroy (GameObject.FindGameObjectWithTag("heartmanager"));
	}
}
