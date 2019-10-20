// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

//[RequireComponent(typeof(AudioSource))] // If we add like this, it means Must.
public class gameSounds : MonoBehaviour {

	public mySoundClass[] mySounds;


	void Awake () 
	{
		foreach (mySoundClass s in mySounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
		}
	}

	public void Play(string name)
	{
		mySoundClass s = Array.Find (mySounds, sound => sound.name == name); // There is no sound variable, but it works. => built-in.
		s.source.Play ();
	}
		
}
