using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

	public struct parameterParentAndChildren
	{
		public GameObject objParent; // e.g. Health parent obj
		public GameObject theChild; // health bar indicator (green,yellow,red) bar
		public int numChildren;
	}

	public struct parameterPanelFamily
	{
		public GameObject objGrandParent; // character0's parameterspanel...character1's...character2's
		public GameObject playerModel; // Butch... Split... Etc in hierarchy (under Character0...1...2)
		public parameterParentAndChildren[] paraName; // Name (and name string), Health (indicator)...
		public int numChildren; //Should be 8 atm
	}

	private struct characterDisplay
	{
		public GameObject displayedModel; // character0...1...2
		public parameterPanelFamily myParams;
	}

	private GameObject[] characterList;
	private int selectionIndex;
	private int numDisplays;
	Vector3 currentRotationVector;

	private characterDisplay[] characterDisplays;

	private Dictionary<int, GameObject> namesDictionary;
	private Dictionary<int, GameObject> healthsDictionary;
	private Dictionary<int, GameObject> damagesDictionary;
	private Dictionary<int, GameObject> rSpeedsDictionary;
	private Dictionary<int, GameObject> hSpeedsDictionary;
	private Dictionary<int, GameObject> vSpeedsDictionary;
	private Dictionary<int, GameObject> xHairsDictionary;
	private Dictionary<int, GameObject> bMarksDictionary;

	private ScriptButch butchValueScript;
	private ScriptSplit splitValueScript;
	private ScriptChim chimValueScript;
	private ScriptKronos kronosValueScript;

	// Use this for initialization
	private void Start () {

		// Allocate memory for our data structures
		numDisplays = transform.childCount;
		characterList = new GameObject[numDisplays];
		characterDisplays = new characterDisplay[numDisplays];

		namesDictionary = new Dictionary<int, GameObject> ();
		healthsDictionary = new Dictionary<int, GameObject> ();
		damagesDictionary = new Dictionary<int, GameObject> ();
		rSpeedsDictionary = new Dictionary<int, GameObject> ();
		hSpeedsDictionary = new Dictionary<int, GameObject> ();
		vSpeedsDictionary = new Dictionary<int, GameObject> ();
		xHairsDictionary = new Dictionary<int, GameObject> ();
		bMarksDictionary = new Dictionary<int, GameObject> ();

		// Initialize our data structures
		for (int i = 0; i < transform.childCount; i++) {
			characterList [i] = transform.GetChild (i).gameObject;
			characterDisplays [i].displayedModel = characterList [i].gameObject;
			characterDisplays [i].myParams.objGrandParent = characterDisplays [i].displayedModel.transform.FindChild ("ParametersPanel").gameObject;
			characterDisplays [i].myParams.playerModel = characterDisplays [i].displayedModel.transform.GetChild (1).gameObject;
			characterDisplays [i].myParams.numChildren = characterDisplays [i].myParams.objGrandParent.transform.childCount;
			characterDisplays [i].myParams.paraName = new parameterParentAndChildren[characterDisplays [i].myParams.numChildren];

			for (int j = 0; j < characterDisplays [i].myParams.numChildren; j++) {
				characterDisplays [i].myParams.paraName [j].objParent = characterDisplays [i].myParams.objGrandParent.transform.GetChild (j).gameObject;
				//characterDisplays[i].myParams.paraName[j].theChildren = new GameObject[characterDisplays [i].myParams.paraName[j].objParent.transform.childCount];
				characterDisplays [i].myParams.paraName [j].numChildren = characterDisplays [i].myParams.paraName [j].objParent.transform.childCount;
				characterDisplays [i].myParams.paraName [j].theChild = characterDisplays [i].myParams.paraName [j].objParent.transform.GetChild (0).gameObject;

				//Debug.Log("This should say name or dummy11: " + characterDisplays[i].myParams.paraName[j].theChild.GetComponent<Text>().text);
				/*Debug.Log("This should say name or dummy1: " + characterDisplays[i].myParams.paraName[j].theChild.name);

				for (int k = 0; k < characterDisplays [i].myParams.paraName[j].numChildren; k++) {
					characterDisplays [i].myParams.paraName[j].theChildren [k] = characterDisplays [i].myParams.paraName[j].objParent.transform.GetChild(k).gameObject;
					//Debug.Log (characterDisplays [i].myParams.paraName[j].theChildren [k].name);
				}*/
				switch (j) {
				case 0:
					namesDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 1:
					healthsDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 2:
					damagesDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 3:
					rSpeedsDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 4:
					hSpeedsDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 5:
					vSpeedsDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 6:
					xHairsDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				case 7:
					bMarksDictionary.Add (i, characterDisplays [i].myParams.paraName [j].theChild.gameObject);
					continue;
				}
			}

			//initialize script references for the characters
			switch (i) {
			case 0:
				butchValueScript = characterDisplays [i].myParams.playerModel.GetComponent<ScriptButch> ();
				butchValueScript.initButch ();
				namesDictionary [i].GetComponent<Text> ().text = butchValueScript.characterName;
				determineBarFeatures ((int) butchValueScript.health, healthsDictionary [i].gameObject);
				determineBarFeatures ((int) butchValueScript.damage, damagesDictionary [i].gameObject);
				determineBarFeatures ((int) butchValueScript.reloadSpeed, rSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) butchValueScript.hSpeed, hSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) butchValueScript.vSpeed, vSpeedsDictionary [i].gameObject);

				continue;
			case 1:
				splitValueScript = characterDisplays [i].myParams.playerModel.GetComponent<ScriptSplit> ();
				splitValueScript.initSplit ();
				namesDictionary [i].GetComponent<Text> ().text = splitValueScript.characterName;
				determineBarFeatures ((int) splitValueScript.health, healthsDictionary [i].gameObject);
				determineBarFeatures ((int) splitValueScript.damage, damagesDictionary [i].gameObject);
				determineBarFeatures ((int) splitValueScript.reloadSpeed, rSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) splitValueScript.hSpeed, hSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) splitValueScript.vSpeed, vSpeedsDictionary [i].gameObject);

				continue;
			case 2:
				chimValueScript = characterDisplays [i].myParams.playerModel.GetComponent<ScriptChim> ();
				chimValueScript.initChim ();
				namesDictionary [i].GetComponent<Text> ().text = chimValueScript.characterName;
				determineBarFeatures ((int) chimValueScript.health, healthsDictionary [i].gameObject);
				determineBarFeatures ((int) chimValueScript.damage, damagesDictionary [i].gameObject);
				determineBarFeatures ((int) chimValueScript.reloadSpeed, rSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) chimValueScript.hSpeed, hSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) chimValueScript.vSpeed, vSpeedsDictionary [i].gameObject);

				continue;
			case 3:
				kronosValueScript = characterDisplays [i].myParams.playerModel.GetComponent<ScriptKronos> ();
				kronosValueScript.initKronos ();
				namesDictionary [i].GetComponent<Text> ().text = kronosValueScript.characterName;
				determineBarFeatures ((int) kronosValueScript.health, healthsDictionary [i].gameObject);
				determineBarFeatures ((int) kronosValueScript.damage, damagesDictionary [i].gameObject);
				determineBarFeatures ((int) kronosValueScript.reloadSpeed, rSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) kronosValueScript.hSpeed, hSpeedsDictionary [i].gameObject);
				determineBarFeatures ((int) kronosValueScript.vSpeed, vSpeedsDictionary [i].gameObject);

				continue;
			}

		}


		// Toggle off rendering of displayModels
		for (int i = 0; i < numDisplays; i++) {
			characterDisplays [i].displayedModel.gameObject.SetActive (false);
		}


		/*
		foreach (KeyValuePair<int,GameObject> entry in namesDictionary){
			Debug.Log("these are all the names in the list: " + entry.Value.GetComponent<Text>().text);
		}*/

		NewNetworkManagerScript networkManager = GameObject.Find ("NewNetworkManager").GetComponent<NewNetworkManagerScript> ();
		selectionIndex = networkManager.getSelectedCharacterId ();
		// Toggle on first index
		if (characterDisplays [networkManager.getSelectedCharacterId()].displayedModel) {
			characterDisplays [networkManager.getSelectedCharacterId()].displayedModel.SetActive (true);
		} else if (characterDisplays [0].displayedModel) {
			characterDisplays [0].displayedModel.SetActive (true);
		} else {
			Debug.Log ("Error: index error while toggling on characterDisplays during start()");
		}
	}

	public void ToggleLeft() {
		// Toggle off current model
		characterDisplays[selectionIndex].displayedModel.SetActive(false);
		//characterList[selectionIndex].SetActive(false);

		// Store current rotation vector
		//currentRotationVector = characterList[selectionIndex].transform.eulerAngles;
		currentRotationVector.x = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		currentRotationVector.y = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.y;
		currentRotationVector.z = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;

		float xVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		float zVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;

		selectionIndex--;
		if (selectionIndex < 0) {
			selectionIndex = numDisplays - 1;
		}

		// Align axis of rotation between models
		//characterList[selectionIndex].transform.eulerAngles = currentRotationVector;
		//characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.y = currentRotationVector.y;
		//float xVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		//float zVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;

		characterDisplays[selectionIndex].myParams.playerModel.transform.eulerAngles.Set(xVal, currentRotationVector.y, zVal);
		// Toggle on new model
		//characterList[selectionIndex].SetActive(true);
		characterDisplays[selectionIndex].displayedModel.SetActive(true);

	}

	public void ToggleRight() {
		// Toggle off current model
		characterDisplays[selectionIndex].displayedModel.SetActive(false);
		//characterList[selectionIndex].SetActive(false);

		// Store current rotation vector
		//currentRotationVector = characterList[selectionIndex].transform.eulerAngles;
		currentRotationVector.x = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		currentRotationVector.y = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.y;
		currentRotationVector.z = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;
		float xVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		float zVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;

		selectionIndex++;
		if (selectionIndex == numDisplays ) {
			selectionIndex = 0;
		}

		// Align axis of rotation between models
		//characterList[selectionIndex].transform.eulerAngles = currentRotationVector;
		//characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.y = currentRotationVector.y;
		//float xVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.x;
		//float zVal = characterDisplays [selectionIndex].myParams.playerModel.transform.eulerAngles.z;

		characterDisplays[selectionIndex].myParams.playerModel.transform.eulerAngles.Set(xVal, currentRotationVector.y, zVal);

		// Toggle on new model
		//characterList[selectionIndex].SetActive(true);
		characterDisplays[selectionIndex].displayedModel.SetActive(true);


	}

	public void ConfirmButton() {
		NewNetworkManagerScript networkManager = GameObject.Find ("NewNetworkManager").GetComponent<NewNetworkManagerScript> ();
		networkManager.updateSelectedCharacter (selectionIndex);
		SceneManager.LoadScene ("Network");
	}

	public void determineBarFeatures(int indicator, GameObject obj){
		float scalingFactor = 0.2f;
		switch (indicator) {
		case 1:
			obj.transform.localScale += new Vector3 (-(1-scalingFactor), 0, 0);
			//obj.GetComponent<RectTransform> ().localScale = new Vector3 (scalingFactor, 0, 0);
			obj.GetComponent<Image> ().color = Color.red;
			break;
		case 2:
			obj.GetComponent<RectTransform> ().localScale += new Vector3 (-(1-2*scalingFactor), 0, 0);
			obj.GetComponent<Image> ().color = new Color (1f,.7f,0);
			break;
		case 3:
			obj.GetComponent<RectTransform> ().localScale += new Vector3 (-(1-3*scalingFactor), 0, 0);
			obj.GetComponent<Image> ().color = Color.yellow;
			break;
		case 4:
			obj.GetComponent<RectTransform> ().localScale += new Vector3 (-(1-4*scalingFactor), 0, 0);
			obj.GetComponent<Image> ().color = new Color (.7f, 1, .2f);
			break;
		case 5:
			//obj.GetComponent<RectTransform> ().localScale += new Vector3 (5*scalingFactor, 0, 0);
			//obj.GetComponent<Image> ().color = new Color (Color.green);
			break;
		}
	}

	public int characterNameToSelectionIndex(string characterName) {
		if (characterName == "Butch") {
			return 0;
		} else if (characterName == "Split") {
			return 1;
		} else if (characterName == "Chim") {
			return 2;
		} else if (characterName == "Kronos") {
			return 3;
		} else {
			return -1;
		}
	}


	// Update is called once per frame
	void Update () {
		Vector3 runTimeRotationVector = Vector3.up * (Time.fixedDeltaTime*30);
		if (selectionIndex == 1 || selectionIndex == 3) {
			runTimeRotationVector = Vector3.forward * (Time.fixedDeltaTime*30);
			characterDisplays [selectionIndex].myParams.playerModel.transform.Rotate (runTimeRotationVector);
		} else {
			characterDisplays [selectionIndex].myParams.playerModel.transform.Rotate (runTimeRotationVector);
		}
	}
}
