using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player2 : MonoBehaviour {
	public float moveForce = 5;
	private float speed = 5.0f;
	public float _health;
	public float _damage;
	public float _rSpeed;
	public float _hSpeed;
	public float _vSpeed;

	private float currX;
	private float currY;
	//Rigidbody myBody;
	Vector2 myPositionVector;
	//Vector2 destination;
	void Start () {
		//myBody = this.GetComponent<Rigidbody> ();
		//myPositionVector = transform.localPosition;
		_hSpeed = 50f;
		_vSpeed = 50f;

	}

	void Update () {
		//transform.position = Vector.Lerp(m_StartPos, endMarker.position, moveForce);
		//Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("x"), CrossPlatformInputManager.GetAxis("y")) * moveForce;
		currX = transform.position.x;
		currY = transform.position.y;
		Vector2 destination = new Vector2 (_hSpeed*-CrossPlatformInputManager.GetAxis ("p2x") +currX, _vSpeed*CrossPlatformInputManager.GetAxis ("p2y")+currY);
		Vector2 moveVec = Vector2.Lerp (transform.position, destination, speed * Time.deltaTime);
		Vector3 appliedVec = new Vector3 (moveVec.x, moveVec.y, transform.position.z);
		transform.position = appliedVec;

		//myBody.AddForce (moveVec);
	}

}
