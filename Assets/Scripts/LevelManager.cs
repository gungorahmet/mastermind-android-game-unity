// Developed by Ahmet Gungor

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {


	public static int[ , ] allLevelsArray = 
	{
		{2,1,2},{2,2,4},{2,3,10},{2,3,8},{2,3,6},{2,4,10},{2,4,8},{2,4,6},{3,2,7},{3,3,10},
		{3,3,8},{3,3,6},{3,3,5},{3,3,4},{3,3,3},{4,3,9},{4,3,7},{4,3,5},{4,3,4},{4,3,3},
		{4,4,7},{4,4,6},{4,4,5},{4,4,4},{3,5,7},{3,5,5},{3,5,4},{5,2,7},{5,2,6},{5,2,4},
		{4,2,3},{5,3,6},{5,3,5},{4,5,8},{4,4,6},{4,5,6},{4,5,5},{3,5,5},{5,3,5},{5,5,7},
		{5,5,6},{3,5,5},{5,4,7},{5,4,6},{5,2,5},{3,4,6},{3,5,7},{2,6,10},{2,6,8},{2,5,6},
		{2,6,5},{3,6,9},{3,6,7},{3,6,5},{4,5,9},{4,5,8},{4,5,6},{3,5,4},{4,6,9},{4,6,8},
		{4,6,7},{4,5,6},{3,5,5},{5,6,8},{5,6,7},{4,6,9},{4,5,7},{6,5,8},{6,5,7},{6,4,6},
		{6,4,5},{6,6,9},{6,6,8},{6,6,7},{5,6,7},{6,4,9},{6,4,8},{7,3,9},{7,4,7},{7,5,9},
		{7,6,9},{7,5,8},{3,5,9},{4,6,8},{2,5,8},{2,5,7},{3,5,5},{7,5,6},{7,7,9},{7,7,8},
		{7,7,7},{6,7,6},{7,4,6},{8,5,9},{8,5,8},{7,7,9},{8,5,7},{8,6,9},{8,6,8},{8,6,7},
		{8,4,5},{8,7,9},{8,7,8},{8,6,7},{8,4,6},{2,6,9},{2,7,8},{3,7,7},{3,7,6},{4,7,9},
		{4,7,9},{4,7,8},{4,7,7},{5,7,9},{5,7,8},{5,7,7},{9,3,9},{9,3,7},{9,3,6},{9,2,7},
		{6,7,8},{6,7,7},{4,7,8},{5,7,9},{5,7,8},{9,3,6},{4,7,5},{9,4,6},{8,3,9},{8,4,6},
		{7,2,8},{7,2,7},{6,7,9},{9,4,7},{9,5,9},{9,5,8},{9,6,9},{9,6,8},{9,6,7},{9,7,9},
		{9,6,8},{9,7,9},{9,7,8},{9,7,7}
	};

	public int Level;
	public static int lastPlayedGameLevel;
	public Image imgLock;
	static public int lvlColorCount,lvlColumnCount,lvlGameRowCount;
	int starCount;


	void Awake()
	{

	}

	void Start () 
	{
		getStars();

		if (ButtonSettings.staticLevelInfo >= Level) 
		{
			Levelunlocked ();
		} 
		else 
		{
			Levellocked ();
		}
	}


	public void LevelSelect()
	{
		StartCoroutine (SoundCasualButton ());

		lastPlayedGameLevel = Level;

		lvlColorCount = allLevelsArray[Level-1,0]; //-1 because it's an array.
		lvlColumnCount = allLevelsArray[Level-1,1];
		lvlGameRowCount = allLevelsArray[Level-1,2];

		SceneManager.LoadScene ("GameScene");
	}
		
	void Levellocked()
	{
		GetComponent<Button> ().interactable = false;
		imgLock.enabled = true;
	}
	void Levelunlocked ()
	{
		GetComponent<Button> ().interactable = true;
		imgLock.enabled = false;
	}
	void getStars()
	{
		Sprite infinitySprite = new Sprite();
		infinitySprite = Resources.Load<Sprite>("Sprites/infinity_s");

		Sprite bgSprite = new Sprite();
		bgSprite = Resources.Load<Sprite>("Sprites/Window_01_Blue");


		if (PlayerPrefs.HasKey ("starCountOfLevel" + Level.ToString())) 
		{
			starCount = PlayerPrefs.GetInt ("starCountOfLevel" + Level.ToString(), starCount);

			for(int i = 0 ; i < starCount ; i++)
			{
				this.gameObject.transform.GetChild(1).GetChild(i).GetComponent<Image>().enabled = true;
			}
		}
		else
		{
			this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = infinitySprite;
		}


		if (PlayerPrefs.GetInt("LastLevel") == Level || Level == 1) 
		{
			this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = bgSprite;
		}

	}

	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}
}
