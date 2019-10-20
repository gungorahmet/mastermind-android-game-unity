using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPersistence : MonoBehaviour {

	// Use this for initialization
	public static bool soundIsOpen;
	public Sprite soundOpenSprite,soundCloseSprite;

	void Start()
	{
		if( PlayerPrefs.HasKey("soundIsOpen") )
		{
			if( PlayerPrefs.GetInt("soundIsOpen") == 0 )
				soundIsOpen = false;
			else
				soundIsOpen = true;

		}
		else
		{
			PlayerPrefs.SetInt("soundIsOpen" , 1);
			soundIsOpen = true;
		}

		setSoundStatus(soundIsOpen);
	}



	void clickSound()
	{
		soundIsOpen = !soundIsOpen;

		if( PlayerPrefs.GetInt("soundIsOpen") == 0 )
			PlayerPrefs.SetInt("soundIsOpen" , 1);
		else
			PlayerPrefs.SetInt("soundIsOpen" , 0);

		StartCoroutine (SoundCasualButton ());

		setSoundStatus(soundIsOpen);
	}

	void setSoundStatus(bool soundIsOpen)
	{
		if(soundIsOpen)
		{
			this.GetComponent<Image>().sprite = soundOpenSprite;
			//Sound related things will be developed in here.

		}
		else
		{
			this.GetComponent<Image>().sprite = soundCloseSprite;
			//Sound related things will be developed in here.
		}
	}
	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}

}
