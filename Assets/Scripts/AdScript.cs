// Modified by Ahmet Gungor => ID

using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdScript : MonoBehaviour
{

	bool hasShownAdOneTime;

	// Use this for initialization
	void Start()
	{
		//Request Ad
		//RequestInterstitialAds();
	}

	public void showMyAd()
	{
		//if (GameScript.isGameOver)
		{
			if (!hasShownAdOneTime)
			{
				hasShownAdOneTime = true;
				showInterstitialAd ();
			}
		}
	}

	public void showInterstitialAd()
	{
		//Show Ad
		if (interstitial.IsLoaded())
		{
			interstitial.Show();

			//Stop Sound
			//

			Debug.Log("SHOW AD XXX");
		}

	}

	InterstitialAd interstitial;
	private void RequestInterstitialAds()
	{
		string adID = "Please Get Your Own ID :)";

		#if UNITY_ANDROID
		string adUnitId = adID;
		#elif UNITY_IOS
		string adUnitId = adID;
		#else
		string adUnitId = adID;
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);

		//***Test***
		AdRequest request = new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
		.AddTestDevice("// Developed / Modified by Ahmet Gungor")  // My test device.
		.Build();

		//***Production***
		//AdRequest request = new AdRequest.Builder().Build();

		//Register Ad Close Event
		interstitial.OnAdClosed += Interstitial_OnAdClosed;

		// Load the interstitial with the request.
		interstitial.LoadAd(request);

		Debug.Log("AD LOADED XXX");

		}

		//Ad Close Event
		private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
		{
		//Resume Play Sound

		}
}