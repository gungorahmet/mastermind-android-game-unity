// Developed by Ahmet Gungor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
//using LitJson; //--------------------------------------------------------- TOBE ADDED
using EasyUIAnimator;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
//Game logos -> http://flamingtext.com/logo/Design-Skate?_variations=true   -> http://coollogo.com/
//Game Logos for More -> https://cooltext.com/

public class GameHandler : MonoBehaviour {



	private int howManyLevelsThereAre = 144;

	public Transform colorsSheet;
	public Transform rowGameCellsSheet;
	public Transform rowHolder; //Special SITUATION !!
	public Transform hintsSheet;
	public Transform hintsRowHolder;
	public Transform buttonsSheet;
	public Transform resultsSheet;

	public GameObject restart2GO;
	public GameObject mainRowHolder;
	public GameObject plusOneGO;
	public GameObject myAnimatorGO;
	public GameObject explosionObject;
	public Transform mainPanel;
	public RectTransform resultSection;
	public GameObject restartObject;
	public GameObject colorCell;
	public GameObject gameCell; //At the same time we use it for "result"
	public GameObject buttonCell;
	public GameObject hintCellYes;
	public GameObject hintCellQuestion;
	public GameObject nextLevelArrow;
	public GameObject addNewRow;
	public GameObject colorSection;
	public GameObject levelText;
	public Text youAreText,justAmazingText;
	public Image starSection,star1,star2,star3;
	public Text tryAgainText;
	public Image goodResult,badResult;
	public static bool delHandler = false;
	GameObject tmpObj;

	private GameObject forNewRowEmptyLine;
	private GameObject tempGO;
	public Sprite emptyCellSprite;
	public Sprite[] colorSprites;

	private static int currentRow;
	private int colorCount,columnCount,gameRowCount;
	private int[] guessInt,solutionInt;
	private int noAdsFirstNLevelValue = 10;
	private int adsModInterValue = 5;
	private int[] MainColorScheme,guessColorScheme;

	private bool gameFinishedYouWon;
	private bool generalGameFinished;
	private bool isPlusOneAppears;
	public static bool rewardRequested = false;
	public static bool intersRequested = false;
	private int currentLevel;
	private string jsonString;
	//private JsonData itemData; //--------------------------------------------------------- TOBE ADDED




		private IEnumerator rotateRestartGO()
		{

			restart2GO.GetComponent<Image>().enabled = true;
			restart2GO.GetComponent<Button>().interactable = true;
			while (true)
			{
				for (float i = 0; i <= 2; i += Time.deltaTime)
				{
					restart2GO.transform.Rotate(Vector3.forward * -200 * Time.deltaTime , Space.Self);
					yield return null;
				}

				for (float i = 0; i <= 2; i += Time.deltaTime)
				{
					restart2GO.transform.Rotate(Vector3.forward * +200 * Time.deltaTime , Space.Self);
					yield return null;
				}
				yield return null;
			}
		}

	private IEnumerator WaitAndPrint()
	{
			tmpObj = new GameObject();
			tmpObj.AddComponent<RectTransform>();
			tmpObj.transform.position = new Vector2(resultSection.position.x,resultSection.position.y + 500);

			UIAnimator.MoveVertical(resultSection, UIAnimator.GetCenter(resultSection).y, UIAnimator.GetCenter(tmpObj.GetComponent<RectTransform>()).y, 4f).Play();


			yield return new WaitForSeconds(4);

			tmpObj.transform.position = new Vector2(resultSection.position.x,resultSection.position.y - 500);
			UIAnimator.MoveVertical(resultSection, UIAnimator.GetCenter(resultSection).y, UIAnimator.GetCenter(tmpObj.GetComponent<RectTransform>()).y, 1f).Play();
			Destroy(tmpObj);

	}
	IEnumerator FadeImage(Image img)
	{

		goodResult.enabled = true;

		for (float i = 0; i <= 3; i += Time.deltaTime)
		{
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}

		for (float i = 2; i >= 0; i -= Time.deltaTime)
		{
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}

		if( currentLevel == howManyLevelsThereAre )
		{
			StartCoroutine(WonAllOfTheGame(youAreText,justAmazingText));
		}
		else
		{
			goodResult.enabled = false;
		}
		yield return null;
	}

	IEnumerator FadeText(Text txt)
	{

		for (float i = 0; i <= 3; i += Time.deltaTime)
		{
			// set color with i as alpha
			txt.color = new Color(1, 1, 1, i);
			yield return null;
		}

		for (float i = 2; i >= 0; i -= Time.deltaTime)
		{
			// set color with i as alpha
			txt.color = new Color(1, 1, 1, i);
			yield return null;
		}
		yield return null;
	}

	IEnumerator WonAllOfTheGame(Text youAre,Text justAmazing)
	{

		for (float i = 0; i <= 2; i += Time.deltaTime)
		{
			// set color with i as alpha

			youAre.color = new Color(1, 1, 1, i);
			yield return null;
		}
		yield return new WaitForSeconds(1f);

		for (float i = 0; i <= 2; i += Time.deltaTime)
		{
			// set color with i as alpha
			justAmazing.color = new Color(1, 1, 1, i);
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		for (float i = 2; i >= 0; i -= Time.deltaTime)
		{
			// set color with i as alpha
			youAre.color = new Color(1, 1, 1, i);
			justAmazing.color = new Color(1, 1, 1, i);
			yield return null;
		}
		goodResult.enabled = false;
		yield return null;
	}



	IEnumerator redLostScreen()
	{
		badResult.enabled = true;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = false;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = true;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = false;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = true;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = false;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = true;
		yield return new WaitForSeconds(0.5f);
		badResult.enabled = false;
		yield return null;
	}

	IEnumerator bombScreen()
	{
		myAnimatorGO.SetActive (true);
		myAnimatorGO.GetComponent<Animator>().SetBool("moveIT",true);

		yield return new WaitForSeconds(2f);

		myAnimatorGO.GetComponent<Animator>().SetBool("moveIT",false);
		myAnimatorGO.SetActive (false);
		yield return null;
	}


	IEnumerator NextLevelButtonAnim()
	{
		bool forward = true;

		while(true)
		{
			float theNum;
			GameObject ok = new GameObject("ok");
			ok.AddComponent<RectTransform>();
			if(forward)
				theNum = nextLevelArrow.GetComponent<RectTransform>().position.x + 30.0f;
			else
				 theNum = nextLevelArrow.GetComponent<RectTransform>().position.x - 30.0f;
			ok.GetComponent<RectTransform>().position = new Vector2(theNum,nextLevelArrow.GetComponent<RectTransform>().position.y);


			UIAnimator.MoveTo(nextLevelArrow.GetComponent<RectTransform>(), UIAnimator.GetCenter(ok.GetComponent<RectTransform>()), 0.5f).Play();

			forward=!forward;
			yield return new WaitForSeconds(0.5f);
			Destroy(ok);
		}
	}

	IEnumerator plusOneAnim(bool myInternetConnection) //original values are -250 and +100
	{
		isPlusOneAppears = true;// bug onleyici.eger iki kere +1e basarsa checkButton'a basilmasini engelliyor.
		plusOneGO.GetComponent<Button> ().interactable = false;
		GameObject myGO = new GameObject("myGO");
		myGO.AddComponent<RectTransform>();



		myGO.GetComponent<RectTransform>().position = new Vector2(plusOneGO.transform.GetComponent<RectTransform>().position.x - 0,
																  plusOneGO.transform.GetComponent<RectTransform>().position.y - 500);



		plusOneGO.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
		Debug.Log ("here is qweqwe = " + myInternetConnection);
		if (myInternetConnection == true)
		{
			Debug.Log ("Passed Here 1");
			plusOneGO.transform.GetChild (0).GetChild (0).GetComponent<Text> ().enabled = true;
		}
		else
		{
			Debug.Log ("Passed Here 2");
			plusOneGO.transform.GetChild (0).GetChild (1).GetComponent<Text> ().enabled = true;
		}




		UIAnimator.MoveTo(plusOneGO.GetComponent<RectTransform>(), UIAnimator.GetCenter(myGO.GetComponent<RectTransform>()), 1f).Play();
		yield return new WaitForSeconds(3.0f);

		myGO.GetComponent<RectTransform>().position = new Vector2(plusOneGO.transform.GetComponent<RectTransform>().position.x + 0,
																  plusOneGO.transform.GetComponent<RectTransform>().position.y + 500);

		UIAnimator.MoveTo(plusOneGO.GetComponent<RectTransform>(), UIAnimator.GetCenter(myGO.GetComponent<RectTransform>()), 1f).Play();



		yield return new WaitForSeconds(1f);




		plusOneGO.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;

		if (myInternetConnection == true)
		{
			Debug.Log ("Passed Here 3");
			plusOneGO.transform.GetChild (0).GetChild (0).GetComponent<Text> ().enabled = false;
		}
		else
		{
			Debug.Log ("Passed Here 4");
			plusOneGO.transform.GetChild (0).GetChild (1).GetComponent<Text> ().enabled = false;
		}




		Destroy(myGO);

		isPlusOneAppears = false;
		plusOneGO.GetComponent<Button> ().interactable = true;
	}


	private IEnumerator SoundPlayer(string filename)
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play(filename);

		yield return null;
	}

	void Start () 
	{
		currentLevel = LevelManager.lastPlayedGameLevel;
		Debug.Log("currentLevel IS --> " + currentLevel);
		if( rewardRequested == false )
		{
			admobINSTANCE.Instance.LoadRewardBasedAd();
			rewardRequested = true;
		}


		//StartCoroutine(bombScreen());
		Debug.Log ("myCheckAttemptCount-Start = " + PlayerPrefs.GetInt ("attemptCount"));
		Debug.Log ("LastLevel Info = " + PlayerPrefs.GetInt ("LastLevel"));

		PlayerPrefs.SetInt ("attemptIsLocked", 0);


		if (PlayerPrefs.GetInt ("attemptCount") != 0 && PlayerPrefs.GetInt ("attemptCount") % adsModInterValue == 0 && PlayerPrefs.GetInt ("LastLevel") >  noAdsFirstNLevelValue )//TODO Needs to make it 10
		{
			Debug.Log ("Condition is provided");
			PlayerPrefs.SetInt ("attemptIsLocked", 1);
			admobINSTANCE.Instance.RequestInterstitial ();
			intersRequested = true;
			Debug.Log ("INTERS is requested");
		}






		if (PlayerPrefs.HasKey ("nextLevelSound")) 
		{
			StartCoroutine (SoundPlayer("nextLevel"));
			PlayerPrefs.DeleteKey ("nextLevelSound");
		}

		StopCoroutine(NextLevelButtonAnim());
		StopCoroutine(rotateRestartGO());
		plusOneGO.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
		plusOneGO.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
		plusOneGO.transform.GetChild(0).GetChild(1).GetComponent<Text>().enabled = false;


		goodResult.enabled = false;
		gameFinishedYouWon = false;
		generalGameFinished = false;
		isPlusOneAppears = false;
		levelText.transform.GetChild(1).GetComponent<Text>().text = LevelManager.lastPlayedGameLevel.ToString();

		//StartCoroutine(plusOneAnim()); //for Testing


		//jsonString = File.ReadAllText(Application.dataPath + "/Resources/Items2.json");    //--------------------------------------------------------- TOBE ADDED
		//itemData = JsonMapper.ToObject(jsonString);    //--------------------------------------------------------- TOBE ADDED



		//redBallSprite = Resources.Load<Sprite>("Sprites/ball_red_s");

		/*colorSprites = Resources.LoadAll<Sprite>("Sprites/check3");
		whitePlaySprite = colorSprites.Single(s => s.name == "check3_0");
		blackPlaySprite = colorSprites.Single(s => s.name == "check3_1");*/

		/*
		int currentLevel = 2;
		int currentEpisode = 5;

		colorCount = int.Parse(getEpisodeProp(currentLevel,currentEpisode)["colorCount"].ToString());    //--------------------------------------------------------- TOBE ADDED
		gameRowCount = int.Parse(getEpisodeProp(currentLevel,currentEpisode)["gameRowCount"].ToString());    //--------------------------------------------------------- TOBE ADDED
		columnCount = int.Parse(getEpisodeProp(currentLevel,currentEpisode)["columnCount"].ToString());    //--------------------------------------------------------- TOBE ADDED
		*/
		Debug.Log("this is lastPlayedGame = " + LevelManager.lastPlayedGameLevel);

		nextLevelArrow.GetComponent<Image>().enabled = false;
		nextLevelArrow.GetComponent<Button>().interactable = false;
		nextLevelArrow.transform.GetChild(0).GetComponent<Text>().enabled = false;

		restart2GO.GetComponent<Image>().enabled = false;  			//it's already on scene but for being sure added here.
		restart2GO.GetComponent<Button>().interactable = false;		//it's already on scene but for being sure added here.


		currentRow = 0;

		colorCount = LevelManager.lvlColorCount;
		columnCount = LevelManager.lvlColumnCount;
		gameRowCount = LevelManager.lvlGameRowCount;

		/*
		colorCount=9; //max is 6 + new Game
		gameRowCount=11; //max is 11
		columnCount=7; //max is 5*/

		solutionInt = new int[columnCount];



		int rastgele;
		for(int i=0 ; i<columnCount; i++)
		{
			rastgele = UnityEngine.Random.Range(0,colorCount);
			solutionInt[i] = rastgele;
			//Debug.Log(solutionInt[i]);
		}

		//simdilik el ile atadim.
		/*
		solutionInt[0] = 2;
		solutionInt[1] = 3;*/

		/*
		cozum[0] = "ball_pink_s"; //    pink
		cozum[1] = "ball_purple"; // 	purple
		cozum[2] = "ball_red_s"; // 	red
		cozum[3] = "ball_red_s"; //	red
		cozum[4] = "ball_pink_s"; //	pink
		cozum[5] = "ball_pink_s"; //	pink
		//cozum[6] = 5; //	red*/



		tempGO = new GameObject();
		for (int i = 0; i < columnCount; i++) 
		{
			tempGO = Instantiate(gameCell);
		    tempGO.name = i.ToString();
			tempGO.transform.SetParent(rowGameCellsSheet,false);
			tempGO.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,0);
			//gameRows[i].transform.GetChild(j).GetComponent<Image>().sprite = testSprite;
			//tempGO.transform.GetChild(0).GetComponent<Image>().color = Color.black;
			//gameRows[i].transform.GetChild(j).transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}

		forNewRowEmptyLine = new GameObject();
		forNewRowEmptyLine.name = "forNewRowEmptyLineISSUE";
		forNewRowEmptyLine = Instantiate(rowGameCellsSheet.gameObject);
		forNewRowEmptyLine.transform.SetParent(mainPanel,false);
		forNewRowEmptyLine.transform.localPosition = new Vector2(-600,-200);

		Sprite lockSprite = new Sprite();
		lockSprite = Resources.Load<Sprite>("Sprites/lock");


		for (int i = 0; i < gameRowCount - 1; i++) 
		{
			tempGO = Instantiate(rowGameCellsSheet.gameObject);
			tempGO.name = "rowGameCellsSheet" + (i+2).ToString();
			tempGO.transform.SetParent(rowHolder,false);
			for (int j = 0; j < columnCount; j++) 
			{
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Image>().sprite = lockSprite;
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Button>().interactable = false;

				Destroy(tempGO.transform.GetChild(j).GetComponent<DragAndDropCell>());
				Destroy(tempGO.transform.GetChild(j).GetChild(0).GetComponent<DragAndDropItem>());
			}
		}

		/*
		for (int i = 0; i < columnCount; i++)
		{
			Destroy(rowHolder.transform.GetChild(0).GetChild(i).GetComponent<DragAndDropCell>());
			Destroy(rowHolder.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<DragAndDropItem>());
		}*/



		for (int i = 0; i < colorCount; i++) 
		{
			tempGO = Instantiate(colorCell);
			tempGO.name = i.ToString();
			tempGO.transform.SetParent(colorsSheet,false);
			//burasi random olacak
			tempGO.transform.GetChild(0).GetComponent<Image>().sprite = colorSprites[i];
		}
			
		buttonCell.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => ButonKaydir());

		Debug.Log ("YENIOYUNDA " + generalGameFinished);

	}
	int getColorIndexByName(string gelen)
	{
		for (int i = 0; i < colorCount; i++) 
		{
			if(gelen == colorSprites[i].name)
			{
				return i;
			}
		}
		return 0;
	}
	public void ButonKaydir()	////////////////////////////////////////////////////////////////////////////////////////
	{
		bool isAnyEmptyCell = false;
		if(currentRow < gameRowCount)
		{
			for (int i = 0; i < columnCount; i++) 
			{
				if(rowHolder.transform.GetChild(currentRow).GetChild(i).GetChild(0).GetComponent<Image>().sprite == emptyCellSprite)
				{
					isAnyEmptyCell = true;
					Debug.Log("satirda bos cell(ler) var!");
					break;
				}
			}
		}



		if(isAnyEmptyCell == false && isPlusOneAppears == false && currentRow < gameRowCount && generalGameFinished == false)
		{

			int guessedCorrect = 0 , another = 0 ; //, olmayan = 0 ;
			guessInt = new int[columnCount];
			guessColorScheme = new int[colorCount];
			MainColorScheme = new int[colorCount];

			for(int i=0 ; i<columnCount; i++)
			{
				MainColorScheme[solutionInt[i]]++;
				//Debug.Log(solutionInt[i]);
			}
			Debug.Log("--------");

				
			Debug.Log("CRow = " + currentRow.ToString());
			//////////////////////////////////////////tutan degerler
			for (int i = 0; i < columnCount; i++) 
			{
				guessInt[i] = getColorIndexByName(rowHolder.transform.GetChild(currentRow).GetChild(i).GetChild(0).GetComponent<Image>().sprite.name);
				//Debug.Log("tahmin" + i.ToString() + " = " + guessInt[i].ToString());
				guessColorScheme[guessInt[i]]++;

				if(solutionInt[i] == guessInt[i])
				{
					MainColorScheme[solutionInt[i]]--;
					guessColorScheme[guessInt[i]]--;
					guessedCorrect++;
				}

			}

			/*for(int i=0 ; i<columnCount; i++)
			{
				Debug.Log(guessInt[i]);
			}*/


			//////////////////////////////////////////baska yerde olan degerler

			for (int i = 0; i < colorCount; i++) 
			{

				if(MainColorScheme[i] >= 1)
				{
					if(guessColorScheme[i] >= MainColorScheme[i])
						another+=MainColorScheme[i];
					else
						another+=guessColorScheme[i];
				}
			}


			Debug.Log("guessedCorrect = " + guessedCorrect.ToString() + " , Another = " + another.ToString());


			tempGO = Instantiate(hintsRowHolder.gameObject);
			tempGO.name = "hintsRowHolder" + currentRow.ToString();
			tempGO.transform.SetParent(hintsSheet,false);

			for (int i = 0; i < guessedCorrect; i++) 
			{
				tempGO = Instantiate(hintCellYes);
				tempGO.name = "guessedCorrect" + i.ToString();
				tempGO.transform.SetParent(hintsSheet.GetChild(currentRow).GetChild(0).transform,false);
			}

			for (int i = 0; i < another; i++) 
			{
				tempGO = Instantiate(hintCellQuestion);
				tempGO.name = "another"+ i.ToString();
				tempGO.transform.SetParent(hintsSheet.GetChild(currentRow).GetChild(0).transform,false);
			}

			if(guessedCorrect == columnCount)
			{
				Debug.Log("Tabiii kiii zrooog");
				gameFinishedYouWon = true;	//it means that the game has finished.
			}

			if(currentRow < gameRowCount - 1 && gameFinishedYouWon != true)
			{
				tempGO = new GameObject("hedef");
				tempGO.AddComponent<RectTransform>();
				float theNum = buttonCell.GetComponent<RectTransform>().position.y - (gameCell.GetComponent<RectTransform>().sizeDelta.y + 3);
				tempGO.GetComponent<RectTransform>().position = new Vector2(buttonCell.GetComponent<RectTransform>().position.x,theNum);

				UIAnimator.MoveTo(buttonCell.GetComponent<RectTransform>(), UIAnimator.GetCenter(tempGO.GetComponent<RectTransform>()), 0.5f).Play();

				Destroy(tempGO);
			}


			if (gameFinishedYouWon == true)    	//it means that the game has finished.
			{
				ShowResult (1);
			}
			else if (currentRow == gameRowCount - 1)
			{
				ShowResult (0);
			}
			else
			{
				StartCoroutine (SoundPlayer("guessButton"));
			}


			currentRow++;
			/*
			for (int i = 0; i < gameRowCount; i++) 
			{
				for (int j = 0; j < columnCount; j++) 
				{
					Destroy(rowHolder.transform.GetChild(i).GetChild(j).gameObject.GetComponent<DragAndDropCell>());
					Destroy(rowHolder.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.GetComponent<DragAndDropItem>());
				}
			}
			
*/			
			Debug.Log("currentRow = " + currentRow.ToString() + " and gameRowCount = " + gameRowCount.ToString());
			if(currentRow!=gameRowCount &&  gameFinishedYouWon != true)
			{
				for(int i = 0; i < columnCount; i++)
				{
					rowHolder.transform.GetChild(currentRow).GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptyCellSprite;
				}
			}

			if (currentRow < gameRowCount && generalGameFinished == false) {
				for (int i = 0; i < columnCount; i++) {
					
					rowHolder.transform.GetChild (currentRow - 1).GetChild (i).gameObject.GetComponent<DragAndDropCell> ().isItAColorSource = true;

					rowHolder.transform.GetChild (currentRow).GetChild (i).gameObject.AddComponent<DragAndDropCell> ();
					rowHolder.transform.GetChild (currentRow).GetChild (i).GetChild (0).gameObject.AddComponent<DragAndDropItem> ();
				}
			} 
			else
			{
				for (int i = 0; i < gameRowCount; i++) 
				{
					for (int j = 0; j < columnCount; j++) 
					{
						Destroy(rowHolder.transform.GetChild(i).GetChild(j).GetComponent<DragAndDropCell>());
						Destroy(rowHolder.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<DragAndDropItem>());
					}
				}

				for (int i = 0; i < colorCount; i++) 
				{
					Destroy(colorsSheet.transform.GetChild(i).GetComponent<DragAndDropCell>());
					Destroy(colorsSheet.transform.GetChild(i).GetChild(0).GetComponent<DragAndDropItem>());
				}
			}


			/*
			for (int i = 0; i < columnCount; i++)
			{
				Destroy(rowHolder.transform.GetChild(currentRow-1).GetChild(i).GetComponent<DragAndDropCell>());
				Destroy(rowHolder.transform.GetChild(currentRow-1).GetChild(i).GetChild(0).GetComponent<DragAndDropItem>());
			}*/

			if( (currentRow == gameRowCount - 2 || currentRow == gameRowCount - 1) && gameRowCount != 11 &&  generalGameFinished == false  && PlayerPrefs.GetInt ("LastLevel") > 5) // 5.Leveldan once +1 teklif etme
			{
				Debug.Log("Son sansLar " + currentRow + "  " + gameRowCount);
				StartCoroutine(plusOneAnim(true));
			}

		}
	}

	public void restartGame()
	{

		/*
		restartObject.GetComponent<Image>().color = new Color(0,0,0,0);
		restartObject.GetComponent<Image>().raycastTarget=false;
		restartObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,0);
		restartObject.transform.GetChild(0).GetComponent<Image>().raycastTarget=false;

		restartObject.transform.GetChild(0).GetComponent<Button>().interactable = false;*/

		if (PlayerPrefs.GetInt ("attemptIsLocked") == 0 && generalGameFinished == false)//genel oyunu koymazsak kazanadıktan sonra restaria basınca da istek atar.
		{
			int attemptCount = 0;
			attemptCount = PlayerPrefs.GetInt ("attemptCount", attemptCount);
			attemptCount += 1;
			PlayerPrefs.SetInt ("attemptCount", attemptCount);
			Debug.Log ("myCheckAttemptCount-RESTARTGAME1  = " + PlayerPrefs.GetInt ("attemptCount"));
		}
			
		Debug.Log ("myCheckAttemptCount-RESTARTGAME2  = " + PlayerPrefs.GetInt ("attemptCount"));




		StartCoroutine (SoundPlayer ("casualButton"));

		if(gameFinishedYouWon == true)
		{
			LevelManager.lastPlayedGameLevel--;
		}

		Debug.Log("GameScene Acildi.");
		SceneManager.LoadScene("GameScene");
	}
	public void ShowResult(int result)
	{
		StartCoroutine(WaitAndPrint());

		if (PlayerPrefs.GetInt ("attemptIsLocked") == 0)
		{
			int attemptCount = 0;
			attemptCount = PlayerPrefs.GetInt ("attemptCount", attemptCount);
			attemptCount += 1;
			PlayerPrefs.SetInt ("attemptCount", attemptCount);
		}

		Debug.Log ("myCheckAttemptCount-ShowResultOne = " + PlayerPrefs.GetInt ("attemptCount"));
		if (PlayerPrefs.GetInt ("attemptCount") != 0 && PlayerPrefs.GetInt ("attemptCount") % adsModInterValue == 0 && PlayerPrefs.GetInt ("LastLevel") >  noAdsFirstNLevelValue )//TODO 10 yapilmasi lazim
		{
			Debug.Log ("KosulSaglandi2");
			PlayerPrefs.SetInt ("attemptIsLocked", 1);
			admobINSTANCE.Instance.RequestInterstitial ();
			intersRequested = true;
			Debug.Log ("INTERS istegi yapildi2");
		}


		generalGameFinished = true;
		Debug.Log ("SHOWRESULTTAKI = " + generalGameFinished);
		if (result == 0)
		{
			StartCoroutine (SoundPlayer ("explosion"));
			StartCoroutine(redLostScreen());
			StartCoroutine(bombScreen());
			StartCoroutine(FadeText(tryAgainText));
			StartCoroutine(rotateRestartGO());
		    Debug.Log("Hakkin Bitti");
		}
		else if (result == 1)
		{

			StartCoroutine (SoundPlayer ("wonLevel"));

			if (36 <= ButtonSettings.staticLevelInfo && ButtonSettings.staticLevelInfo <= 71) 
			{
				PlayerPrefs.SetInt ("LevelPageNo", 2);
				ButtonSettings.staticLevelPage = 2;
			}
			else if (71 <= ButtonSettings.staticLevelInfo && ButtonSettings.staticLevelInfo <= 107) 
			{
				PlayerPrefs.SetInt ("LevelPageNo", 3);
				ButtonSettings.staticLevelPage = 3;
			}
			else if (108 <= ButtonSettings.staticLevelInfo && ButtonSettings.staticLevelInfo <= 143) 
			{
				PlayerPrefs.SetInt ("LevelPageNo", 4);
				ButtonSettings.staticLevelPage = 4;
			}

			int beforeStarCount = 0;

			if (PlayerPrefs.HasKey("starCountOfLevel" + LevelManager.lastPlayedGameLevel.ToString())) 
				beforeStarCount = PlayerPrefs.GetInt("starCountOfLevel" + LevelManager.lastPlayedGameLevel.ToString(),beforeStarCount);



			int star1Thresold = (LevelManager.lvlGameRowCount * 3) / 8;
			int star2Thresold = (LevelManager.lvlGameRowCount * 8) / 10;

			if( LevelManager.lastPlayedGameLevel == 1 || LevelManager.lastPlayedGameLevel == 2 ) // ilk 2 level her turlu 3 yildiz alsin.   BUXFIXED v1.8'de fixlendi.
			{
				star2Thresold = currentRow + 2;
				star1Thresold = currentRow + 2;
			}
				


			Debug.Log("currentRow + 1 ---------------  " + (currentRow + 1).ToString() + "============" + star1Thresold.ToString() + "---" + star2Thresold.ToString() + "---");

			StartCoroutine(FadeImage(starSection));

			if(currentRow+1 <= star1Thresold)
			{
				StartCoroutine(FadeImage(star1));
				StartCoroutine(FadeImage(star2));
				StartCoroutine(FadeImage(star3));
			}
			else if(currentRow+1 <= star2Thresold)
			{
				StartCoroutine(FadeImage(star1));
				StartCoroutine(FadeImage(star2));
			}
			else
			{
				StartCoroutine(FadeImage(star1));
			}

			if((currentRow + 1) > star2Thresold && beforeStarCount < 2)
			{
				PlayerPrefs.SetInt ("starCountOfLevel" + LevelManager.lastPlayedGameLevel, 1);
			}
			else if((currentRow + 1) > star1Thresold && beforeStarCount < 3)
			{
				PlayerPrefs.SetInt ("starCountOfLevel" + LevelManager.lastPlayedGameLevel, 2);
			}
			else
			{
				PlayerPrefs.SetInt ("starCountOfLevel" + LevelManager.lastPlayedGameLevel, 3);
			}


			LevelManager.lastPlayedGameLevel++;

			int hafizaLastLevel = 1;
			hafizaLastLevel = PlayerPrefs.GetInt ("LastLevel",hafizaLastLevel);

			if(hafizaLastLevel <= LevelManager.lastPlayedGameLevel)
			{
				hafizaLastLevel = LevelManager.lastPlayedGameLevel;
				PlayerPrefs.SetInt ("LastLevel", hafizaLastLevel);
			}

			if( currentLevel != howManyLevelsThereAre ) // son bolum degilse ciksin
			{
				Debug.Log("Buldun!");
				Debug.Log("Level " + LevelManager.lastPlayedGameLevel + " ------------------------> is completed!");
				nextLevelArrow.GetComponent<Image>().enabled = true;
				nextLevelArrow.GetComponent<Button>().interactable = true;
				nextLevelArrow.transform.GetChild(0).GetComponent<Text>().enabled = true;

				StartCoroutine(NextLevelButtonAnim());
			}

		}
			
		//Son satirin tasinilabilirligini de kaldir
		//Cozumu Yazdir.
		tempGO = new GameObject();
		for (int i = 0; i < columnCount; i++) 
		{
			tempGO = Instantiate(gameCell);
			tempGO.name = "cozum" + i.ToString();
			tempGO.transform.SetParent(resultsSheet,false);
			tempGO.transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);	
			tempGO.transform.GetChild(0).GetComponent<Image>().sprite = colorSprites[solutionInt[i]];

			Destroy(tempGO.transform.GetComponent<DragAndDropCell>());
			Destroy(tempGO.transform.GetChild(0).GetComponent<DragAndDropItem>());
		}

		//////////////Bolume restart atma
		/*
		restartObject.GetComponent<Image>().color = new Color(1,1,1,1);
		restartObject.GetComponent<Image>().raycastTarget =true;

		restartObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);
		restartObject.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;


		restartObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
*/
	}
	/*
							//--------------------------------------------------------------------------- TO BE ADDED.
	JsonData getEpisodeProp(int level,int episode)
	{
		for (int i = 0; i < itemData["Levels"].Count; i++) 
		{
			if (itemData["Levels"][i]["lid"].ToString() == level.ToString()) 
			{
				for (int j = 0; j < itemData["Levels"][i]["Episode"].Count; j++) 
				{
					if (itemData["Levels"][i]["Episode"][j]["eid"].ToString() == episode.ToString()) 
					{
						return itemData["Levels"][i]["Episode"][j];
					}
				}
			}
		}
		return null;
	}
	*/
	public void LoadRewardAd()
	{
		admobINSTANCE.Instance.LoadRewardBasedAd ();
	}

	public void showRewardAd()
	{
		if(generalGameFinished == false && gameRowCount < 11)
			admobINSTANCE.Instance.ShowRewardBasedAd ();
	}



	public void checkYourInternet()
	{
		StartCoroutine (plusOneAnim (false));
	}

	public void addNewRowToGame()
	{
		
		Debug.Log ("REKLAM BITTI VE +1 EKLENECEK");
		Debug.Log ("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq " + gameRowCount + " ve de " + generalGameFinished);
		if(gameRowCount < 11 && generalGameFinished == false)
		{
		Debug.Log ("1REKLAM BITTI VE +1 EKLENECEK");
			gameRowCount++;
		Debug.Log ("2REKLAM BITTI VE +1 EKLENECEK");
			Sprite lockSprite = new Sprite();
			lockSprite = Resources.Load<Sprite>("Sprites/lock");
		Debug.Log ("3REKLAM BITTI VE +1 EKLENECEK");
			tempGO = new GameObject();
		Debug.Log ("4REKLAM BITTI VE +1 EKLENECEK");
			tempGO = Instantiate(forNewRowEmptyLine);
		Debug.Log ("5REKLAM BITTI VE +1 EKLENECEK");
			tempGO.name = "rowGameCellsSheet" + (gameRowCount - 1+2).ToString();
			tempGO.transform.SetParent(rowHolder,false);
		Debug.Log ("6REKLAM BITTI VE +1 EKLENECEK");
			for (int j = 0; j < columnCount; j++) 
			{
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Image>().sprite = lockSprite;
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);
				tempGO.transform.GetChild(j).GetChild(0).GetComponent<Button>().interactable = false;

				Destroy(tempGO.transform.GetChild(j).GetComponent<DragAndDropCell>());
				Destroy(tempGO.transform.GetChild(j).GetChild(0).GetComponent<DragAndDropItem>());
			}
		}
	}

	/*
	 IEnumerator plusOneAnim()
	{
		float numX,numy;
		GameObject myGO = new GameObject("myGO");
		myGO.AddComponent<RectTransform>();

		numX = plusOneGO.GetComponent<RectTransform>().position.x;
		numy = plusOneGO.GetComponent<RectTransform>().position.y;

		myGO.GetComponent<RectTransform>().position = new Vector2(numX,numy);

		plusOneGO.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
		plusOneGO.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;

		UIAnimator.MoveTo(plusOneGO.GetComponent<RectTransform>(), UIAnimator.GetCenter(target), 1f).Play();
		yield return new WaitForSeconds(2.5f);


		UIAnimator.MoveTo(plusOneGO.GetComponent<RectTransform>(), UIAnimator.GetCenter(myGO.GetComponent<RectTransform>()), 1f).Play();

		plusOneGO.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
		plusOneGO.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;

		yield return new WaitForSeconds(1f);
		Destroy(myGO);

	}*/

	public void destroyAdAffectedDragDragObjects()
	{

		for (int i = 0; i < currentRow + 1; i++) 
		{
			for (int j = 0; j < columnCount; j++) 
			{
				Destroy(mainRowHolder.transform.GetChild(i).GetChild(j).GetComponent<DragAndDropCell>());
				Destroy(mainRowHolder.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<DragAndDropItem>());
			}
		}

		for (int i = 0; i < colorCount; i++)
		{
			Destroy(colorSection.transform.GetChild(0).GetChild(i).GetComponent<DragAndDropCell> ());
			Destroy(colorSection.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<DragAndDropItem>());
		}

	}

	public void addAdAffectedDragDragObjects()
	{
		for (int i = 0; i < currentRow + 1; i++) 
		{
			for (int j = 0; j < columnCount; j++) 
			{
				mainRowHolder.transform.GetChild(i).GetChild(j).gameObject.AddComponent<DragAndDropCell> ();
				if( i != currentRow)
					mainRowHolder.transform.GetChild(i).GetChild(j).gameObject.GetComponent<DragAndDropCell>().isItAColorSource = true;
				mainRowHolder.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.AddComponent<DragAndDropItem> ();
			}
		}

		for (int i = 0; i < colorCount; i++)
		{
			colorSection.transform.GetChild(0).GetChild(i).gameObject.AddComponent<DragAndDropCell> ();
			colorSection.transform.GetChild(0).GetChild(i).gameObject.GetComponent<DragAndDropCell>().isItAColorSource = true;
			colorSection.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.AddComponent<DragAndDropItem>();
		}

	}

	public void destroyBugIcon()
	{
		Debug.Log("DESTROYBUG-START");
		/*

		if (DragAndDropItem.icon != null)
		{
			Destroy(DragAndDropItem.icon);                                                     
		}
*/
		GameObject[] respawns;
		respawns = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject respawn in respawns)
		{
			Destroy(respawn);
		}

		respawns = GameObject.FindGameObjectsWithTag("ITEM");
		foreach (GameObject respawn in respawns)
		{
			respawn.GetComponent<Image>().raycastTarget = true;
		}
			                                  
			//        FindObjectOfType<DragAndDropItem>().MakeRaycast(true);


		DragAndDropItem.draggedItem = null;
		DragAndDropItem.icon = null;
		DragAndDropItem.sourceCell = null;




		/*
		GameObject[] respawns;
		respawns = GameObject.FindGameObjectsWithTag("ortaligiKaristiran");

		foreach (GameObject respawn in respawns)
		{
			Destroy(respawn);
		}
		*/
		Debug.Log("DESTROYBUG-END");
	}
}
