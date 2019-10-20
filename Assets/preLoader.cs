using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preLoader : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
