using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class networkUpdateManagerScript : MonoBehaviour {

	private NewNetworkManagerScript networkManager;
	//[SerializeField] Text connectionText;

	private string[] characterNames = new string[] {"Butch", "Split", "Chim","Kronos"};
	private string selectedCharacterName = "";
	private int wins = 0;
	private int losses = 0;
	private int winStreak = 0;

	// Use this for initialization
	void Start () {
		networkManager = GameObject.Find ("NewNetworkManager").GetComponent<NewNetworkManagerScript> ();
		//GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().characterLimit = 10;
		//GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().contentType = UnityEngine.UI.InputField.ContentType.Alphanumeric;
	}

	// Update once per frame
	void Update () {
		if (selectedCharacterName != networkManager.getSelectedCharacterName ()) {
			selectedCharacterName = networkManager.getSelectedCharacterName ();
			GameObject.Find ("SelectedCharacter").GetComponent<UnityEngine.UI.Text> ().text = "Selected Character: " + selectedCharacterName;
			foreach (string characterName in characterNames) {
				if (characterName == selectedCharacterName) {
					GameObject.Find (characterName).SetActive (true);
				} else {
					GameObject.Find (characterName).SetActive (false);
				}
			}
		}

		if (wins != networkManager.wins) {
			wins = networkManager.wins;
			//Debug.Log ("Wins: " + wins.ToString());
			GameObject.Find ("Wins").GetComponent<UnityEngine.UI.Text> ().text = "Wins: " + wins.ToString();
		}
		if (losses != networkManager.losses) {
			losses = networkManager.losses;
			//Debug.Log ("Losses: " + losses.ToString());
			GameObject.Find ("Losses").GetComponent<UnityEngine.UI.Text> ().text = "Losses: " + losses.ToString();
		}
		if (winStreak != networkManager.winStreak) {
			winStreak = networkManager.winStreak;
			//Debug.Log ("Win Streak: " + winStreak.ToString());
			GameObject.Find ("WinStreak").GetComponent<UnityEngine.UI.Text> ().text = "Win Streak: " + winStreak.ToString();
		}
		//connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}

	// Update player name
	public void updatePlayFriendInput (string input) {
		//GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.Image> ().color = Color.white;
	}

	public void switchToCharacterSelectButton () {
		networkManager.switchToCharacterSelect ();
	}

	public void switchToGameButton () {
		networkManager.switchToGameScene ();
	}

	// Host a friend game
	public void hostFriendButton () {
		//networkManager.hostFriend ();
	}

	// Join a friend game
	public void joinFriendButton () {
		//networkManager.joinFriend ();
	}

	// Play with random player
	public void playRandomButton () {
		//networkManager.playRandom ();
	}
}
