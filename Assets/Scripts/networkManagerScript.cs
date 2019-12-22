using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class networkManagerScript : MonoBehaviour {

	// Private variable(s)
	private int characterId = 0;
	private string[] characterNames = new string[] { "Butch", "Split", "Chim", "Kronos" };
	private string gameMode = "";
	private string playerStatus = "";
	private string playerType = "";
	private bool allowCancel = false;

	private static networkManagerScript scriptInstance;
	private GameObject gameUser;

	public GameObject player2Transform;
	private Vector3 player2Position;
	private Quaternion player2Rotation;

	// Initial setup
	void Start () {
		// Avoid duplication of this object
		if (scriptInstance == null) {
			scriptInstance = this;
		} else {
			Destroy (this.gameObject);
			return;
		}

		// To stop the script object from destroying when a new scene is loaded
		DontDestroyOnLoad (this.gameObject);

		// Randomly select a character
		updateSelectedCharacter (UnityEngine.Random.Range (0, 4));

		// Set loglevel to full
		//PhotonNetwork.logLevel = PhotonLogLevel.Full;

		// Get player 2 setting
		player2Position = player2Transform.transform.position;
		player2Rotation = player2Transform.transform.rotation;
	}

	// Get selected character id
	public int getSelectedCharacterId () {
		return characterId;
	}

	// Get selected character name
	public string getSelectedCharacterName () {
		return characterNames [characterId];
	}

	// Update selected character
	public void updateSelectedCharacter (int selectedCharacterId) {
		characterId = selectedCharacterId;
	}

	// Switch to CharacterSelect scene
	public void switchToCharacterSelect () {
		//	CharacterSelect scene will call updateSelectCharacterName to update player's selected character
		SceneManager.LoadScene ("CharacterSelect");
	}

	private void disableUserInputs () {
		GameObject.Find ("SelectCharacterButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
		GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().interactable = false;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
		GameObject.Find ("PlayFriendJoinButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
	}

	private void enableUserInputs () {
		GameObject.Find ("SelectCharacterButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().interactable = true;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("PlayFriendJoinButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}

	private void enableCancelFriendHostButton () {
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().image.color = Color.red;
		GameObject.Find ("PlayFriendHostButton").transform.Find ("Text").GetComponent<UnityEngine.UI.Text> ().text = "CANCEL";
		allowCancel = true;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}

	private void enableCancelRandomButton () {
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().image.color = Color.red;
		GameObject.Find ("PlayRandomButton").transform.Find ("Text").GetComponent<UnityEngine.UI.Text> ().text = "CANCEL";
		allowCancel = true;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}

	private void resetFriendHostButton () {
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().image.color = Color.white;
		GameObject.Find ("PlayFriendHostButton").transform.Find ("Text").GetComponent<UnityEngine.UI.Text> ().text = "Play with Friend (HOST)";
		allowCancel = false;
		GameObject.Find ("PlayFriendHostButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}

	private void resetRandomButton () {
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = false;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().image.color = Color.white;
		GameObject.Find ("PlayRandomButton").transform.Find ("Text").GetComponent<UnityEngine.UI.Text> ().text = "Play with Random Player";
		allowCancel = false;
		GameObject.Find ("PlayRandomButton").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}

	// Host a friend game
	public void hostFriend () {
		if (allowCancel) {
			if (playerType == "host") {
				PhotonNetwork.room.IsOpen = false;
				PhotonNetwork.room.IsVisible = false;
			}
			PhotonNetwork.Disconnect ();
			gameMode = "";
			playerStatus = "";
			playerType = "";
			resetFriendHostButton ();
			enableUserInputs ();
		} else {
			gameMode = "hostfriend";
			disableUserInputs ();
			PhotonNetwork.ConnectUsingSettings ("1.0");
		}
	}

	// Join a friend game
	public void joinFriend () {
		gameMode = "joinfriend";
		disableUserInputs ();
		PhotonNetwork.ConnectUsingSettings ("1.0");
	}

	// Play with random player
	public void playRandom () {
		if (allowCancel) {
			if (playerType == "host") {
				PhotonNetwork.room.IsOpen = false;
				PhotonNetwork.room.IsVisible = false;
			}
			PhotonNetwork.Disconnect ();
			gameMode = "";
			playerStatus = "";
			playerType = "";
			resetRandomButton ();
			enableUserInputs ();
		} else {
			gameMode = "random";
			disableUserInputs ();
			PhotonNetwork.ConnectUsingSettings ("2.0");
		}
	}

	// On connected as a master client, join lobby
	void OnConnectedToMaster () {
		PhotonNetwork.JoinLobby ();
	}

	// On joined lobby
	void OnJoinedLobby () {
		playerStatus = "inLobby";
		if (gameMode == "random") {
			PhotonNetwork.JoinRandomRoom ();
		} else if (gameMode == "joinfriend") {
			string roomName = GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().textComponent.text;
			if (roomName == "") {
				GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().placeholder.GetComponent<Text> ().text = "ENTER GAME NAME";
				GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.Image> ().color = Color.red;
				PhotonNetwork.Disconnect ();
				enableUserInputs ();
			} else {
				PhotonNetwork.JoinRoom (roomName);
			}
		} else if (gameMode == "hostfriend") {
			string roomName = GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().textComponent.text;
			if (roomName == "") {
				GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().placeholder.GetComponent<Text> ().text = "ENTER GAME NAME";
				GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.Image> ().color = Color.red;
				PhotonNetwork.Disconnect ();
				enableUserInputs ();
			} else {
				RoomOptions ro = new RoomOptions () { MaxPlayers = 2, IsOpen = true, IsVisible = true };
				PhotonNetwork.CreateRoom (roomName, ro, TypedLobby.Default);
			}
		} else {
			PhotonNetwork.Disconnect ();
			enableUserInputs ();
		}
	}

	void OnJoinedRoom () {
		playerStatus = "inRoom";
		if (gameMode == "random") {
			Debug.Log ("[Random] Name: "+PhotonNetwork.room.Name);
			Debug.Log ("[Random] PlayerCount: "+PhotonNetwork.room.PlayerCount);
			if (PhotonNetwork.room.PlayerCount == 1) {
				playerType = "host";
				enableCancelRandomButton ();
				setupGame ();
			} else {
				playerType = "client";
				setupGame ();
			}
		} else if (gameMode == "joinfriend") {
			PhotonNetwork.room.IsOpen = false;
			PhotonNetwork.room.IsVisible = false;
			Debug.Log ("[JoinFriend] Name: "+PhotonNetwork.room.Name);
			Debug.Log ("[JoinFriend] PlayerCount: "+PhotonNetwork.room.PlayerCount);
			playerType = "client";
			setupGame ();
		} else if (gameMode == "hostfriend") {
			Debug.Log ("[HostFriend] Name: "+PhotonNetwork.room.Name);
			Debug.Log ("[HostFriend] PlayerCount: "+PhotonNetwork.room.PlayerCount);
			playerType = "host";
			enableCancelFriendHostButton ();
			setupGame ();
		} else {
			PhotonNetwork.Disconnect ();
			enableUserInputs ();
		}
	}

	void OnPhotonRandomJoinFailed () {
		Debug.Log ("join random failed");
		playerStatus = "inLobby";
		if (gameMode == "random") {
			DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			string roomName = (DateTime.UtcNow - epochStart).TotalMilliseconds.ToString ();
			RoomOptions ro = new RoomOptions () { MaxPlayers = 2, IsOpen = true, IsVisible = true };
			PhotonNetwork.CreateRoom (roomName, ro, TypedLobby.Default);
		}
	}

	void OnPhotonJoinRoomFailed () {
		playerStatus = "";
		if (gameMode == "joinfriend") {
			Debug.Log ("DR: Join Room Failed!");
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().text = "";
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().placeholder.GetComponent<UnityEngine.UI.Text> ().text = "GAME NOT FOUND";
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.Image> ().color = Color.red;
			enableUserInputs ();
		}
		PhotonNetwork.Disconnect ();
	}

	void OnPhotonCreateRoomFailed () {
		playerStatus = "";
		if (gameMode == "random") {

		} else if (gameMode == "hostfriend") {
			Debug.Log ("DR: Create Room Failed!");
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().text = "";
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.InputField> ().placeholder.GetComponent<UnityEngine.UI.Text> ().text = "GAME NAME IN USE";
			GameObject.Find ("PlayFriendInput").GetComponent<UnityEngine.UI.Image> ().color = Color.red;
			enableUserInputs ();
		}
		PhotonNetwork.Disconnect ();
	}

	void OnPhotonInstantiate(PhotonMessageInfo info) 
	{
		Debug.Log (info);
	}

	void setupGame () {
		// Load game scene
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.LoadLevel("Game");

		// Instantiate network player
		if (playerType == "host") {
			gameUser = PhotonNetwork.Instantiate ("Player1_ScreenCanvas", new Vector3 (0, 0, 100), Quaternion.identity, 0);
		} else if (playerType == "client") {
			gameUser = PhotonNetwork.Instantiate ("Player1_ScreenCanvas", player2Position, player2Rotation, 0);
		} else {
			return;
		}

		// Activate character
		if (characterId == 0) { // Butch
			gameUser.GetComponent<GameplayManager> ().butch.SetActive (true);
		} else if (characterId == 1) { // Split
			gameUser.GetComponent<GameplayManager> ().split.SetActive (true);
		} else if (characterId == 2) { // Chim
			gameUser.GetComponent<GameplayManager> ().chim.SetActive (true);
		} else if (characterId == 3) { // Kronos
			gameUser.GetComponent<GameplayManager> ().kronos.SetActive (true);
		}

		// Setup network player
		gameUser.GetComponent<GameplayManager> ().initPlayer (0, "player");
	}

}
