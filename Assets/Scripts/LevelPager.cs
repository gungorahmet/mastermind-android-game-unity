// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUIAnimator;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPager : MonoBehaviour {

	public int levelPageNo;
	public GameObject leftArrow,rightArrow;
	void Start()
	{
		if(levelPageNo == 1)
		{
			showHideArrows(leftArrow,false);
			showHideArrows(rightArrow,true);
		}
		else if(levelPageNo == 2 || levelPageNo == 3)
		{
			showHideArrows(leftArrow,true);
			showHideArrows(rightArrow,true);
		}
		else if(levelPageNo == 4)
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


	public void LevelRightClick()
	{
		StartCoroutine (SoundCasualButton ());
		
		if(levelPageNo == 1)
		{
			SceneManager.LoadScene ("LevelMenu2");
		}
		if(levelPageNo == 2)
		{
			SceneManager.LoadScene ("LevelMenu3");
		}
		if(levelPageNo == 3)
		{
			SceneManager.LoadScene ("LevelMenu4");
		}

	}

	public void LevelLeftClick()
	{
		StartCoroutine (SoundCasualButton ());
		
		if(levelPageNo == 2)
		{
			SceneManager.LoadScene ("LevelMenu1");
		}
		if(levelPageNo == 3)
		{
			SceneManager.LoadScene ("LevelMenu2");
		}
		if(levelPageNo == 4)
		{
			SceneManager.LoadScene ("LevelMenu3");
		}
	}

	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}
}
