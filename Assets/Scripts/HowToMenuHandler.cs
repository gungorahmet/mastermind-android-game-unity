// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUIAnimator;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToMenuHandler : MonoBehaviour {

	public int pageNo;
	public GameObject leftArrow,rightArrow;
	void Start()
	{
		if(pageNo == 1)
		{
			showHideArrows(leftArrow,false);
			showHideArrows(rightArrow,true);
		}
		else if(pageNo == 2)
		{
			showHideArrows(leftArrow,true);
			showHideArrows(rightArrow,true);
		}
		else if(pageNo == 3)
		{
			showHideArrows(leftArrow,true);
			showHideArrows(rightArrow,false);
		}
	}

	void showHideArrows(GameObject myGO,bool active)
	{
		myGO.GetComponent<Image>().enabled = active;
		myGO.GetComponent<Button>().interactable = active;
		myGO.transform.GetChild(0).GetComponent<Text>().enabled = active;
	}


	public void rightPageClick()
	{
		StartCoroutine (SoundCasualButton ());

		if(pageNo == 1)
		{
			SceneManager.LoadScene ("HTP2");
		}
		if(pageNo == 2)
		{
			SceneManager.LoadScene ("HTP3");
		}
	}

	public void leftPageClick()
	{
		StartCoroutine (SoundCasualButton ());
		
		if(pageNo == 2)
		{
			SceneManager.LoadScene ("HTP1");
		}
		if(pageNo == 3)
		{
			SceneManager.LoadScene ("HTP2");
		}
	}
		
	public void openLevelsMenu()
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
