// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

	public GameObject howToPlayButton;
	private int howToPlayFast = 8;


	private IEnumerator rotateHowToPlayBtn()
	{

		while (true)
		{
			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				howToPlayButton.transform.Rotate(Vector3.forward * +howToPlayFast * Time.deltaTime , Space.Self);
				yield return null;
			}

			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				howToPlayButton.transform.Rotate(Vector3.forward * -howToPlayFast * Time.deltaTime , Space.Self);
				yield return null;
			}

			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				howToPlayButton.transform.Rotate(Vector3.forward * -howToPlayFast * Time.deltaTime , Space.Self);
				yield return null;
			}
			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				howToPlayButton.transform.Rotate(Vector3.forward * +howToPlayFast * Time.deltaTime , Space.Self);
				yield return null;
			}
			yield return null;
		}
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(rotateHowToPlayBtn());

		if (PlayerPrefs.HasKey ("completedAllLevels")) 
		{
			StartCoroutine (SoundCompletedAll());
			PlayerPrefs.DeleteKey ("completedAllLevels");
		}
	}

	public void openLevelsMenu()
	{
		StartCoroutine (SoundCasualButton ());

		if (PlayerPrefs.HasKey ("LevelPageNo")) 
		{
			int sayfa = PlayerPrefs.GetInt ("LevelPageNo");
			SceneManager.LoadScene ("LevelMenu" + sayfa);
		}
		else
			SceneManager.LoadScene ("LevelMenu" + ButtonSettings.staticLevelPage);
	}

	public void openHowToPlayMenu()
	{
		StartCoroutine (SoundCasualButton ());
		
		SceneManager.LoadScene ("HTP1");
	}
	public void CloseTheApp()
	{
		StartCoroutine (SoundCasualButton ());
		
		Application.Quit();
	}

	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}

	IEnumerator SoundCompletedAll()
	{

		FindObjectOfType<gameSounds>().Play("completedAll");

		yield return null;
	}
}
