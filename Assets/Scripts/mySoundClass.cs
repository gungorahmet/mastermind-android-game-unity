using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class mySoundClass
{
	public string name;
	public AudioClip clip;

	[HideInInspector]
	public AudioSource source;

}
