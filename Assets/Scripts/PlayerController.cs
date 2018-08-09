using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;				// Forward/backward movement speed
	public float rotationSpeed;				// Character rotation speed
	public float jumpPower;					// Standard jump strength
	public int numberOfLives;				// Player starts with 5 lives
	private bool boost;						// Whether or not boosted jumps are enabled
	public int currentLevel;				// The level that the player has reached
	private float waitTime = 3;
	private float elapsedTime = 0.0f;
	private bool countdown = false;			// For ending the game
	private bool hasKey = false;			// Whether or not the first gateway key has been picked up
	private bool hasBlueKey = false;		// Coloured keys for second stage / third stage
	private bool hasRedKey = false;
	private bool hasGreenKey = false;
	private bool hasWhiteKey = false;
	private bool hasBlackKey = false;
	public GameObject HeartUI;
	public GameObject GroundDetect;
	public bool cheatModeEnabled;
	public float spawnPointX, spawnPointY, spawnPointZ;
	private float boostMod;

	void Start () {
		currentLevel = 1;
	}

	// Update is called once per frame
	void Update () {
		if (HeartUI.GetComponent<HeartUIManager>().GetNumLives() < 0) {
			GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle> ().resetCompletionist ();
			SceneManager.LoadScene("Game_Over");
		}


		if (countdown) {
			// Wait a few seconds then go to exit screen
			GameObject playerObj = GameObject.FindWithTag ("Player");
			playerObj.GetComponent<Animation> ().Play ("Idle");

			// Once enough time has passed, finish by loading the completion screen
			if (elapsedTime > waitTime) {
				SceneManager.LoadScene ("Game_Complete");
			} else {
				elapsedTime += Time.deltaTime;
			}
		} else {
			// Switch to run animation on arrow key forward/backward
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
				GetComponent<Animation> ().Play ("Walk");
			} else {
				GetComponent<Animation> ().Play ("Idle");
			}

			// Move the character forwards/backwards using arrow keys
			transform.Translate (new Vector3(0, 0, Input.GetAxis("Vertical")) * Time.deltaTime * movementSpeed);

			// Rotate the character left/right also using arrow keys
			if (Input.GetKey(KeyCode.LeftArrow)) {
				transform.Rotate (-Vector3.up * rotationSpeed * Time.deltaTime);
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
			}

			// Jump activated by the space bar
			if (Input.GetKeyDown ("space")) {
				jump();
			} 
		}
	}

	void jump() { 
		if (!cheatModeEnabled) {
			if (GroundDetect.GetComponent<groundDetector> ().isOnGround ()) {
				if (!boost) {
					gameObject.GetComponents<AudioSource> () [2].Play (); // Jump/step effect
					GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower);
				} else { 
					gameObject.GetComponents<AudioSource> () [3].Play (); // Big Jump effect
					GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower * boostMod);
				}
			}
		} else {
			if (!boost) {
				gameObject.GetComponents<AudioSource> () [2].Play (); // Jump/step effect
				GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower);
			} else { 
				gameObject.GetComponents<AudioSource> () [3].Play (); // Big Jump effect
				GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower * boostMod);
			}
		}
	} 

	// Player Collision Detection
	void OnCollisionEnter(Collision col){
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Boost_Pad")) {
			// Activate boost
			boost = true;
			// Set boost modifier
			boostMod = col.collider.gameObject.GetComponent<BoostPadBehaviour>().boostModifier;
		} else if (boost && !(col.collider.gameObject.layer == LayerMask.NameToLayer("Boost_Pad"))) {
			// Deactivate boost only if it is activated previously, and we have collided with something else
			boost = false;
		}

		// Player 'dies' on collision with water hazards
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Water_Hazard")) {	//Check if collided object is a water hazard
			// Reduce the number of lives by 1
			//numberOfLives--; ****************************************************
			HeartUI.GetComponent<HeartUIManager>().SubtractLife();

			// Set the player to the last checkpoint
			gameObject.GetComponents<AudioSource>()[0].Play(); // Death sound effect
			transform.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
		}	

		// If the player collides with the key, they gain the ability to pass through the end-of-level gateway
		if (col.collider.gameObject.layer == LayerMask.NameToLayer ("Key")) {
			hasKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play(); // Key pickup sound effect
			Destroy (col.collider.gameObject);	// Remove the key from the scene, as it has been picked up
		}
			

		// Player has successfully completed the stage upon collision with the gateway
		// assuming they have obtained the key(s) to unlock it
		if (col.collider.gameObject.layer == LayerMask.NameToLayer ("Gateway")) {
			/* First Level */
			if (currentLevel == 1) {
				if (!hasKey) {
					// Play no key error sound effect
					col.collider.gameObject.GetComponents<AudioSource>()[1].Play(); // Error sound effect
				} else {
					// Level has been beaten
					currentLevel++;

					// Set new spawn position for next level
					Vector3 newPos = new Vector3 (-1700f, 68f, 809f);
					spawnPointX = newPos.x;
					spawnPointY = newPos.y;
					spawnPointZ = newPos.z;

					// Play success sound effect
					col.collider.gameObject.GetComponents<AudioSource>()[0].Play(); // Success sound effect

					// Go to start of next stage using new spain point
					transform.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
				}
			}



			/* Second Level */
			else if (currentLevel == 2) {
				// Has not obtained all keys yet
				if (!(hasRedKey && hasBlueKey && hasGreenKey)) {
					// Play no key error sound effect
					col.collider.gameObject.GetComponents<AudioSource>()[1].Play(); // Error sound effect
				}

				// Has all keys
				else {
					SceneManager.LoadScene ("FinalLevel", LoadSceneMode.Single);
				}
			}
				


			/* Final Level */
			else if (currentLevel == 0) {
				if (!(hasBlackKey && hasWhiteKey)) {
					// Play no key error sound effect
					col.collider.gameObject.GetComponents<AudioSource>()[1].Play(); // Error sound effect
				} else {

					// Play success sound effect
					col.collider.gameObject.GetComponents<AudioSource>()[0].Play(); // Success sound effect

				}
			}
		}
	}

	void OnTriggerExit (Collider col) {
		// Deactivate level 1 tigers
		if (col.gameObject.tag.Equals ("FirstTigerTrigger") || col.gameObject.tag.Equals ("SecondTigerTrigger")) {
			col.gameObject.GetComponent<FirstTigerTrigger> ().untrigger ();
		}
	}

	void OnTriggerStay (Collider col) {
		// Enable level 1 tigers
		if (col.gameObject.tag.Equals ("FirstTigerTrigger") || col.gameObject.tag.Equals ("SecondTigerTrigger")) {
			col.gameObject.GetComponent<FirstTigerTrigger> ().trigger ();
		}
	}

	void OnTriggerEnter (Collider col) {
		// First level lock
		if (col.gameObject.tag.Equals("level_one_gate_lock")) {
			if (!hasKey) {
				// Error sound
				col.gameObject.GetComponent<AudioSource> ().Play ();
			} else {
				// Remove lock and bars
				Destroy (col.gameObject);
				gameObject.GetComponents<AudioSource> ()[1].Play ();
			}
		}

		// Final Lock
		if (col.gameObject.layer == LayerMask.NameToLayer("Lock") && !hasKey) {
			if (!hasWhiteKey || !hasBlackKey) {
				// Error sound
				col.gameObject.GetComponent<AudioSource> ().Play ();
			} else {
				// Remove lock and bars
				Destroy (col.gameObject);

				// Play success sound
				GameObject successObject = GameObject.FindWithTag("SuccessSound");
				successObject.GetComponent<AudioSource> ().Play ();

				countdown = true;
			}
		}

		// Teleporters
		if (col.gameObject.layer == LayerMask.NameToLayer("Teleporter")) {
			gameObject.GetComponents<AudioSource>()[4].Play(); // Teleporter sound effect
			Vector3 destination = col.gameObject.GetComponent<TeleporterBehaviour>().getDestVector();
			transform.position = destination;
		}

		// Tiger traps
		if (col.gameObject.layer == LayerMask.NameToLayer("Tiger_Trigger_Two")) {
			col.gameObject.GetComponent<AudioSource> ().Play ();
			if (!col.gameObject.GetComponent<TigerTriggerBehaviour> ().triggered) {
				col.gameObject.GetComponent<TigerTriggerBehaviour> ().trigger ();
			} //else {
			//	col.gameObject.GetComponent<TigerTriggerBehaviour>().untrigger();
			//}
		}

		// Key Pickups
		if (col.gameObject.layer == LayerMask.NameToLayer ("Red_Key")) {
			hasRedKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play();
			Destroy (col.GetComponent<Collider>().gameObject);	// Destroy Key
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("Blue_Key")) {
			hasBlueKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play();
			Destroy (col.GetComponent<Collider>().gameObject);	// Destroy Key
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("Green_Key")) {
			hasGreenKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play();
			Destroy (col.GetComponent<Collider>().gameObject);	// Destroy Key
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("White_Key")) {
			hasWhiteKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play();
			Destroy (col.GetComponent<Collider>().gameObject);	// Destroy Key
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("Black_Key")) {
			hasBlackKey = true;
			gameObject.GetComponents<AudioSource>()[1].Play();
			Destroy (col.GetComponent<Collider>().gameObject);	// Destroy Key
		}

		// Disable particle system on item pickup
		if (col.gameObject.layer == LayerMask.NameToLayer ("Pickup_Particle")) {
			col.GetComponent<ParticleSystem>().Stop ();
		}

		// Checkpoint setting
		if (col.gameObject.layer == LayerMask.NameToLayer ("Checkpoint")) {
			// Reset the player's spawn point so that it represents their checkpoint
			Vector3 temp = col.gameObject.GetComponent<CheckPointBehaviour>().getCheckPointData();
			spawnPointX = temp.x;
			spawnPointY = temp.y;
			spawnPointZ = temp.z;
		}

		// If the player collides with a heart, they gain a life
		if (col.gameObject.layer == LayerMask.NameToLayer ("Extra_Life")) {

			GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle> ().HeartPickup();
			// Increase lives by 1
			//numberOfLives++; ********************************************
			HeartUI.GetComponent<HeartUIManager>().AddLife(col.gameObject.GetComponent<HeartBehaviour>().lifeAmt);

			gameObject.GetComponents<AudioSource>()[1].Play(); // Heart pickup effect

			//gameObject.GetComponents<AudioSource>()[].Play(); // Heart pickup sound effect
			Destroy (col.GetComponent<Collider>().gameObject);	// Remove the heart from the scene, as it has been picked up
		}
	}
}
