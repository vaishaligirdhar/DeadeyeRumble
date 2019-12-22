using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerMovement : MonoBehaviour {

	#region Local variables
	private GameplayManager playerManager;
	private CharacterParameters scriptWithMyValues;

	private float speed = 5.0f;
	//private int isPlayer1;
	private string myXAxisName;
	private string myYAxisName;

	private float movableXRegion;
	private float movableYRegion;

	private float xRegionReductionFactor;
	private float yRegionReductionFactor;

	public float _hSpeed;
	public float _vSpeed;

	private float currX;
	private float currY;

	Vector2 myPositionVector;
	#endregion

	// Use this for initialization
	void Start(){
		//movableXRegion = transform.parent.

		if (gameObject.name == "Player1") {
			//isPlayer1 = 0;
			myXAxisName = "p1x";
			myYAxisName = "p1y";
		} else if (gameObject.name == "Player2") {
			//isPlayer1 = 1;
			myXAxisName = "p2x";
			myYAxisName = "p2y";
		} else {
			Debug.LogError("playerMovement script is not on Player1 or Player2 game object");
		}

		//Get game manager object and script of active instance
		playerManager = this.gameObject.transform.parent.gameObject.GetComponent<GameplayManager> ();
		scriptWithMyValues = playerManager.getActiveCharacterScript();
		//_hSpeed = scriptWithMyValues.hSpeed;
		//_vSpeed = scriptWithMyValues.vSpeed;
		_hSpeed = 100;
		_vSpeed = 100;
	}

	// Update is called once per frame
	void Update () {
		currX = transform.position.x;
		currY = transform.position.y;
		//Debug.Log("CrossPlatformInputManager.GetAxis: " + CrossPlatformInputManager.GetAxis (myXAxisName).ToString() + " " + CrossPlatformInputManager.GetAxis (myYAxisName).ToString());
		//Debug.Log("Input.GetAxis: " + Input.GetAxis (myXAxisName).ToString() + " " + Input.GetAxis (myYAxisName).ToString());

		Vector2 destination = new Vector2 (_hSpeed*CrossPlatformInputManager.GetAxis (myXAxisName) +currX, _vSpeed*CrossPlatformInputManager.GetAxis (myYAxisName)+currY);
		Vector2 moveVec = Vector2.Lerp (transform.position, destination, speed * Time.deltaTime);
		Vector3 appliedVec = new Vector3 (moveVec.x, moveVec.y, transform.position.z);

		transform.position = appliedVec;
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -420, 420), Mathf.Clamp(transform.position.y, -500, 270), transform.position.z);
		//transform.localPosition = appliedVec; Using local position here doesn't work. Understand why in spare time.
	}

	#region Debug Log strings
	//Debug.Log("transform.position: " + transform.position.x.ToString() + " " + transform.position.y.ToString() + " " + transform.position.z.ToString());
	//Debug.Log("transform.position: " + currX.ToString() + " " + currY.ToString());
	//Debug.Log("CrossPlatformInputManager.GetAxis (): " + CrossPlatformInputManager.GetAxis(myXAxisName).ToString() + " " + CrossPlatformInputManager.GetAxis (myYAxisName).ToString());
	//Debug.Log ("Destination: " + destination.x.ToString () + " " + destination.y.ToString ()); 
	//Debug.Log("appliedVec: " + appliedVec.x.ToString() + " " + appliedVec.y.ToString() + " " + appliedVec.z.ToString());
	#endregion

}
