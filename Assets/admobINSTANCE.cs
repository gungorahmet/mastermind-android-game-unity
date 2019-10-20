// Developed / Modified by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;


public class admobINSTANCE : MonoBehaviour {


	private RewardBasedVideoAd rewardBasedVideo;
	public InterstitialAd interstitial;

	public static admobINSTANCE  Instance{ set; get;}
	private static bool delegatesAreDone = false;
	private bool rewardVideoWatched;
	uint exitCount = 0;
	void Start () 
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);

		rewardBasedVideo = RewardBasedVideoAd.Instance;
		if (delegatesAreDone == false)
		{
			rewardBasedVideo.OnAdLoaded += HandleOnAdLoaded;
			rewardBasedVideo.OnAdFailedToLoad += HandleOnAdFailedToLoad;
			rewardBasedVideo.OnAdOpening += HandleOnAdOpening;
			rewardBasedVideo.OnAdStarted += OnAdStarted;
			rewardBasedVideo.OnAdClosed += OnAdClosed;
			rewardBasedVideo.OnAdRewarded += HandleOnAdRewarded;
			rewardBasedVideo.OnAdLeavingApplication += HandleOnAdLeavingApplication;
			delegatesAreDone = true;

			rewardVideoWatched = false;
		}
	}

	public void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "Please Get Your Own ID :)";
		#elif UNITY_IPHONE
		string adUnitId = "Please Get Your Own ID :)";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.


		AdRequest request = new AdRequest.Builder().Build(); //Real Ads
		Debug.Log ("INTERS fromInstance");

		/*
		AdRequest request = new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
		.AddTestDevice("Please get your own ID, hehe :)")  // My test device.
		.Build();*/

		// Load the interstitial with the request.
		interstitial.LoadAd(request);

	}

		public void ShowInterstitial()
		{
			if (interstitial.IsLoaded()) 
			{
				interstitial.Show();

				FindObjectOfType<GameHandler>().destroyBugIcon (); // Life-saver, bug-fixer great function :)
				PlayerPrefs.SetInt ("attemptIsLocked", 0);
				PlayerPrefs.SetInt ("attemptCount", 0);
				Debug.Log ("attempCount and  attemptIsLocked values are made 0");
				GameHandler.intersRequested = false;
				Debug.Log ("Let's show us INTERS");
			}
			Debug.Log ("Out of INTERS");
		}


		void Update () 
		{
			if (GameHandler.intersRequested == true)
			{
				ShowInterstitial();
			}

			if(Input.GetKeyUp(KeyCode.Escape))
			{
				exitCount++;
				
				if(!IsInvoking("disableDoubleClick"))
					Invoke("disableDoubleClick", 0.3f);
			}
			
			if(exitCount == 2)
			{
				CancelInvoke("disableDoubleClick");
				Application.Quit();
			}

		}




	public void ShowRewardBasedAd()
	{
		if (rewardBasedVideo.IsLoaded ())
		{
			rewardBasedVideo.Show ();
		} 
		else
		{
			FindObjectOfType<GameHandler>().checkYourInternet ();
			FindObjectOfType<GameHandler>().LoadRewardAd ();
			Debug.Log ("Doesnt loaded");
			//LoadRewardBasedAd ();
            // Here I should write a function to check Internet Connection :)
		}
	}

	public void LoadRewardBasedAd()
	{
		#if UNITY_EDITOR
		string adUnitId = "Please Get Your Own ID :)";
		#elif UNITY_ANDROID
		string adUnitId = "Please Get Your Own ID :)";
		#elif UNITY_IPHONE
		string adUnitId = "Please Get Your Own ID :)";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		Debug.Log ("came here");



		AdRequest request = new AdRequest.Builder().Build();   //Real Ads

		/*
		AdRequest request = new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
		.AddTestDevice("Please Get Your Own ID :)")  // My test device.
		.Build();*/

		rewardBasedVideo.LoadAd(request, adUnitId);

		}
		// Ad event fired when the rewarded video ad 

		//has been received.
		public void HandleOnAdLoaded(object sender, EventArgs args)
		{
			Debug.Log("REKLAM RECIEVED");
			GameHandler.rewardRequested = true;
		}

		// has failed to load.
		public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			//StartCoroutine(plusOneAnim(false));
			Debug.Log("REKLAM COULDN't BE LOADEDDD");
			GameHandler.rewardRequested = false;
		}

		// is opened.
		public void HandleOnAdOpening(object sender, EventArgs args)
		{
			Debug.Log("REKLAM WHAT IS THAT :)");
		}

		// has started playing.
		public void OnAdStarted(object sender, EventArgs args)
		{
			Debug.Log("REKLAM STARTED");
		}

		// is closed.
		public void OnAdClosed(object sender, EventArgs args)
		{
		Debug.Log("REKLAM CLOSED");
			FindObjectOfType<GameHandler>().destroyBugIcon ();
			LoadRewardBasedAd ();

			if( rewardVideoWatched == true )
				StartCoroutine(addRowAnim());

				rewardVideoWatched = false;
		}

		// has rewarded the user.
		public void HandleOnAdRewarded(object sender, Reward args)
		{
			Debug.Log("REWARD ADS WATCHED AND REWARD IS GIVEN");
			rewardVideoWatched = true;
		}

		// is leaving the application.
		public void HandleOnAdLeavingApplication(object sender, EventArgs args)
		{
			Debug.Log("REKLAM WHILE LEAVING");
		}

		private IEnumerator addRowAnim()
		{
			yield return new WaitForSeconds(1.5f);
			StartCoroutine(SoundPlayer("plusOneFX"));
			FindObjectOfType<GameHandler>().addNewRowToGame();

			yield return null;
		}

		private IEnumerator SoundPlayer(string filename)
		{
			if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play(filename);

			yield return null;
		}

		void disableDoubleClick()
		{
			exitCount = 0;
		}
}
