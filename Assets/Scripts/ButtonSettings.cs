// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSettings : MonoBehaviour {

	public static int staticLevelInfo = 1;
	public static int staticLevelPage = 1;
	private int howManyLevelsThereIs = 144;
	private int nextLevel = LevelManager.lastPlayedGameLevel+1;
	//public string nextLevelScene;

	public void resetLevels()
	{
		PlayerPrefs.DeleteAll();
		staticLevelInfo = 1;
		staticLevelPage = 1;
		SceneManager.LoadScene("MainMenu");
	}

	void Awake()
	{
		if (PlayerPrefs.HasKey ("LastLevel")) 
		{
			staticLevelInfo = PlayerPrefs.GetInt ("LastLevel", staticLevelInfo);

		}
		else
			PlayerPrefs.SetInt ("LastLevel", 1);
			
		if (PlayerPrefs.HasKey ("LevelPageNo")) 
		{
			staticLevelPage = PlayerPrefs.GetInt ("LevelPageNo", staticLevelInfo);
		}
		if (!PlayerPrefs.HasKey ("attemptCount"))
			PlayerPrefs.SetInt ("attemptCount", 0);
		if (!PlayerPrefs.HasKey ("attemptIsLocked"))
			PlayerPrefs.SetInt ("attemptIsLocked", 0);
		Debug.Log("==================================== staticLevelInfo ==================================== " + staticLevelInfo);
	}

	public void ButtonNextLevelScene()
	{

		PlayerPrefs.SetInt ("nextLevelSound", 1); // muzik icin.

		/*
		 
		 Need next section informations.
		 * */
		if(nextLevel <= howManyLevelsThereIs)
		{
			setNextLevel(nextLevel);

			Debug.Log("NextLevel=" + nextLevel.ToString());

			SceneManager.LoadScene ("GameScene");//

			if(staticLevelInfo <= nextLevel)
			{
				staticLevelInfo = nextLevel;
				PlayerPrefs.SetInt ("LastLevel", staticLevelInfo);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("completedAllLevels", 1);
			nextLevel--;
			SceneManager.LoadScene ("MainMenu");
		}
	}
	public void ButtonMenu()
	{
		StartCoroutine (SoundCasualButton ());
		/*
		GameObject btn1 =  new GameObject();
		btn1 = GameObject.FindGameObjectWithTag("Finish");
		Debug.Log(btn1.GetComponent<LevelManager>().refColorCount);
		Destroy(btn1);*/

		SceneManager.LoadScene ("LevelMenu" + staticLevelPage);
	}

	public void setNextLevel(int myNextLevel)
	{
		LevelManager.lvlColorCount = LevelManager.allLevelsArray[myNextLevel-1,0];
		LevelManager.lvlColumnCount = LevelManager.allLevelsArray[myNextLevel-1,1];
		LevelManager.lvlGameRowCount = LevelManager.allLevelsArray[myNextLevel-1,2];
	}

	public void goBackToMainMenu()
	{
		StartCoroutine (SoundCasualButton ());
		
		SceneManager.LoadScene ("MainMenu");
	}
	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}
}