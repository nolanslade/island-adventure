using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatformBehaviourWithRetract : MonoBehaviour {
	public bool retractable;
	private bool retracting = false;
	public float minHeight, maxHeight;
	private bool moving = false;
	private bool goingUp;
	public bool startsAtTop;
	public float movementSpeed;
	private float restTime = 3;
	private float currentTime = 0;

	// Start function to init components
	void Start () {
		// Determine where the elevator starts (top or bottom)
		if (startsAtTop) {
			goingUp = false;
		} else {
			goingUp = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (startsAtTop) {
			if (!retracting) {
				if (moving && goingUp && transform.position.y >= maxHeight) {
					moving = false;
					goingUp = false;
				} else if (moving && !goingUp && transform.position.y <= minHeight) {
					moving = false;
					goingUp = true;
				}

				if (moving && goingUp) {
					transform.Translate (new Vector3 (0, 1, 0) * Time.deltaTime * movementSpeed);
				} else if (moving && !goingUp) {
					transform.Translate (new Vector3 (0, -1, 0) * Time.deltaTime * movementSpeed);
				} else {
					currentTime += Time.deltaTime;
				}

				// Retract if it is required
				if (retractable) {
					if (!moving && goingUp && transform.position.y <= minHeight) {
						retracting = true;
					}
				}
			} else {
				if (transform.position.y < maxHeight) {
					// Retract up for retry if the player happens to fail the obstacle
					transform.Translate (new Vector3 (0, 1, 0) * Time.deltaTime * movementSpeed);
				} else {
					retracting = false;
					goingUp = false;
				}
			}
		} else {
			// Starts at the bottom initially, moves up then retracts
			if (!retracting) {
				// Stop the platform and switch direction once it reaches the bottom/top
				if (moving && goingUp && transform.position.y >= maxHeight) {
					moving 	= false;
					goingUp = false;
				} else if (moving && !goingUp && transform.position.y <= minHeight) {
					moving 	= false;
					goingUp = true;
				}

				// Movement
				if (moving && goingUp) {
					transform.Translate (new Vector3 (0, 1, 0) * Time.deltaTime * movementSpeed);
				} else if (moving && !goingUp) {
					transform.Translate (new Vector3 (0, -1, 0) * Time.deltaTime * movementSpeed);
				} else {
					currentTime += Time.deltaTime;
				}

				// Retract if it is required
				if (retractable) {
					if (!moving && !goingUp && transform.position.y >= maxHeight) {
						retracting = true;
					}
				}
			} else {

				// **************************************

				if (transform.position.y > minHeight) {
					// Retract up for retry if the player happens to fail the obstacle
					transform.Translate (new Vector3 (0, -1, 0) * Time.deltaTime * movementSpeed);
				} else {
					retracting = false;
					goingUp = true;
				}
			}
		}
	}

	// On Collision, the elevator platform moves up or down
	void OnCollisionEnter (Collision col) {
		if (!moving && currentTime > restTime) {
			moving = true;
			currentTime = 0;
		}
	}
}
