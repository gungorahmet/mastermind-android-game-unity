// Developed by Ahmet Gungor

using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

	void Start()
	{
		
	}
    #region Public Methods

    public void SetEnglish()
    {
        Localize.SetCurrentLanguage(SystemLanguage.English);
        LocalizeImage.SetCurrentLanguage();

		StartCoroutine (SoundCasualButton ());
    }

    public void SetTurkish()
    {
		Localize.SetCurrentLanguage(SystemLanguage.Turkish);
        LocalizeImage.SetCurrentLanguage();

		StartCoroutine(SoundCasualButton());
    }

    #endregion Public Methods

	IEnumerator SoundCasualButton()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("casualButton");

		yield return null;
	}
}
