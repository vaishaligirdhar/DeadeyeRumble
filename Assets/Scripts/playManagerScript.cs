using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class playManagerScript : MonoBehaviour {

	// Private variable(s)
	private string playerName = "";

	// Initial setup
	void Start () {
		Screen.fullScreen = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToPortrait = false;
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.SetResolution (Screen.width, Screen.height, false);

		hideHowToPlay ();

		//GameObject.Find ("Error").GetComponent<UnityEngine.UI.Text>().text = "";
		//GameObject.Find ("Canvas").transform.Find ("Error BG").gameObject.SetActive (false);
		//GameObject.Find ("NameInput").GetComponent<UnityEngine.UI.InputField> ().characterLimit = 8;

		// To stop the script object from destroying when a new scene is loaded
		DontDestroyOnLoad (this.gameObject);
	}

	// Switch to Network scene
	public void switchToNetworkScene () {
		//if (playerName == "") {
		//	GameObject.Find ("Error").GetComponent<UnityEngine.UI.Text>().text = "ENTER NAME";
		//	GameObject.Find ("Canvas").transform.Find ("Error BG").gameObject.SetActive (true);
		//} else {
			SceneManager.LoadScene ("Network");
		//}
	}

	// Get player name
	public string getPlayerName () {
		return playerName;
	}

	// Update player name
	public void updateInputName (string input) {
		//GameObject.Find ("Error").GetComponent<UnityEngine.UI.Text>().text = "";
		//GameObject.Find ("Canvas").transform.Find ("Error BG").gameObject.SetActive (false);
		//playerName = input;
	}

	// Show how to play 1 image
	public void loadHowToPlay1() {
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1").gameObject.SetActive (true);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1Button").gameObject.SetActive (true);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2Button").gameObject.SetActive (false);
	}

	// Show how to play 2 image
	public void loadHowToPlay2() {
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1Button").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2").gameObject.SetActive (true);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2Button").gameObject.SetActive (true);
	}

	// Hide how to play 1 and 2 images
	public void hideHowToPlay() {
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay1Button").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2").gameObject.SetActive (false);
		GameObject.Find ("Canvas").transform.Find ("HowToPlay2Button").gameObject.SetActive (false);
	}
}