using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

	public int count = 0;
	public Vector3 position = new Vector3(0,0,0);
	float p_z;

	// Use this for initialization
	void Start () {
		p_z = 1100;
		
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		if (count % 25 == 0) {
			position = new Vector3 (Random.Range(-Screen.width, Screen.width), Random.Range(-Screen.height, Screen.height), p_z);
			transform.position = position;
		}
		
	}
}
