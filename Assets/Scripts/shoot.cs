using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
	public GameObject manager;
	public GameObject shot;
	static int count = 0;
	public Vector2 position = new Vector2(0,0);

	void Start () {
		//this.GetComponent<Collider>().enabled = true;
		this.gameObject.SetActive (true);
		//manager.GetComponent<GameplayManager>().
		//^ is false then can shoot, else no.

		//setactivetrue
		//set active false

	}
		
	// Update is called once per frame
	void Update () {
		this.GetComponent<Collider>().enabled = false;
		count++;
		//if (isReaload == (true)) {
		if (count % 40 == 0) {
			count = 0;
			this.GetComponent<Collider>().enabled = true;
			this.GetComponent<AudioSource> ().Play ();
			position = new Vector3 (Random.Range(-Screen.width, Screen.width), Random.Range(0, Screen.height), 111f);
			transform.position = position;
			Instantiate (shot, position, Quaternion.identity);
			shot.transform.localScale += transform.localScale.normalized;

		}
			//}
	}

}
