using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	// All start adventure/play again buttons use this method to load first level
	public void Play() 
	{
		SceneManager.LoadScene("SceneOne");
	}


	// Main menu can provide instructions
	public void Instructions() 
	{
		SceneManager.LoadScene("Instruction_Scene");
	}


	// Back to main menu from instructions
	public void MainMenu() 
	{
		SceneManager.LoadScene("Main_Menu");
	}
}
