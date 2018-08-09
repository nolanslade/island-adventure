using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour {

	public Sprite heartIcon;		// Heart image
	public Sprite blankIcon;		// Blank image
	public GameObject LifeUI;			// Canvas
	private int numberOfActiveHearts;	// Player's current lives
	//private int maxLives = 7;			// Max lives
	private int maxLives;
	private int initLives;
	private bool changesMade = false;	// Only update pictures if a life has been gained/lost

	// Life objects
	private GameObject heart1;
	private GameObject heart2;
	private GameObject heart3;
	private GameObject heart4;
	private GameObject heart5;
	private GameObject heart6;
	private GameObject heart7;

	// Sets the script and UI to do not destroy to last through all stages
	// Plus sets all heart objects
	void Start () {
		maxLives = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle>().MaxLives();
		initLives = GameObject.FindGameObjectWithTag ("difficultyToggle").GetComponent<DifficultyToggle> ().StartingLives ();

		InitializeGame ();

		print ("Initial lives = " + initLives);
		print ("Max lives = " + maxLives);

		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (LifeUI);

		heart1 = GameObject.FindWithTag ("heart1");
		heart2 = GameObject.FindWithTag ("heart2");
		heart3 = GameObject.FindWithTag ("heart3");
		heart4 = GameObject.FindWithTag ("heart4");
		heart5 = GameObject.FindWithTag ("heart5");
		heart6 = GameObject.FindWithTag ("heart6");
		heart7 = GameObject.FindWithTag ("heart7");

		if (initLives == 0) {
			heart1.GetComponent<Image> ().sprite = blankIcon;
			heart2.GetComponent<Image> ().sprite = blankIcon;
			heart3.GetComponent<Image> ().sprite = blankIcon;
			heart4.GetComponent<Image> ().sprite = blankIcon;
			heart5.GetComponent<Image> ().sprite = blankIcon;
			heart6.GetComponent<Image> ().sprite = blankIcon;
			heart7.GetComponent<Image> ().sprite = blankIcon;
		}
	}


	// Gets current lives=
	public int GetNumLives () {
		return numberOfActiveHearts;
	}


	// Adds hearts
	public void InitializeGame () {
		numberOfActiveHearts = initLives;
	}


	// Removes the UI component when at menus
	public void EndGame () {
		Destroy (LifeUI);
		Destroy (gameObject);
	}


	// Update is called once per frame
	void Update () {
		// Switch the sprite image if necessary to represent lives
		if (changesMade) {
			for (int i = 1; i <= maxLives; i++) {
				// Set to blank
				if (i > numberOfActiveHearts) {
					switch (i) {
					case 1:
						heart1.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 2:
						heart2.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 3:
						heart3.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 4:
						heart4.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 5:
						heart5.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 6:
						heart6.GetComponent<Image> ().sprite = blankIcon;
						break;
					case 7:
						heart7.GetComponent<Image> ().sprite = blankIcon;
						break;
					}
				} 

				// Set to heart
				else {
					switch (i) {
					case 1:
						heart1.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 2:
						heart2.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 3:
						heart3.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 4:
						heart4.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 5:
						heart5.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 6:
						heart6.GetComponent<Image> ().sprite = heartIcon;
						break;
					case 7:
						heart7.GetComponent<Image> ().sprite = heartIcon;
						break;
					}
				}
			}

			changesMade = false;
		}
	}
		

	// Adds a life (or more) if not already full
	public void AddLife (int num) {
		if (numberOfActiveHearts + num <= maxLives) {
			numberOfActiveHearts += num;
		} else {
			numberOfActiveHearts = maxLives;
		}

		changesMade = true;
	}


	// Takes away life
	public void SubtractLife () {
		numberOfActiveHearts--;
		changesMade = true;
	}
}
