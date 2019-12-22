using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//remember to change the bullets to splitbullet to splitbullet1

public class CrosshairScript : MonoBehaviour {

	public GameObject[] bullets;
	public GameObject[] hitboxes;
	public float speed = 20;
	private GameObject myBullet;
	private GameObject hitbox;
	private GameObject runTimeBullet;
	private GameObject runTimeHitbox;
	private Rigidbody myRigidBody;
	private int count = 0;
	private int temp = Screen.height / 7;
	private Rect rect;

	public GameplayManager helpme;

	// Use this for initialization
	void Start () {		
		helpme = GameObject.Find ("Player1_ScreenCanvas").GetComponent<GameplayManager> ();
		rect = new Rect(0, temp, Screen.width, Screen.height);
	}

	// Update is called once per frame
	void Update () {

		for(int i = 0; i < Input.touchCount; i++){
			if (Input.GetTouch (i).phase == TouchPhase.Began && !(helpme.isReloading) && rect.Contains(Input.GetTouch(i).position)) {
				float b_x, b_y;
				if((int) Input.GetTouch(i).position.x <= (int) Screen.width/2)
					b_x = Input.GetTouch(i).position.x - Screen.width;
				else
					b_x = Input.GetTouch (i).position.x;
				if((int) Input.GetTouch(i).position.y <= (int) Screen.height/2)
					b_y = Input.GetTouch(i).position.y - Screen.height;
				else
					b_y = Input.GetTouch (i).position.y;			
				float b_z = 189f;
				GameplayManager godpleasehelpme = GameObject.Find ("Player1_ScreenCanvas").GetComponent <GameplayManager> ();

				initMyHitbox (godpleasehelpme.getActiveCharId());//error here, need to get ID of player
				initMyBulletType (godpleasehelpme.getActiveCharId());

				count++;
				if (count == 1)
					runTimeHitbox = Instantiate (hitbox, new Vector3 (0, 0, 0), Quaternion.identity);

				this.transform.position = new Vector2 (b_x, b_y);

				runTimeHitbox.transform.parent = transform;
				runTimeHitbox.transform.position = new Vector3(b_x, b_y, 500f);
				runTimeHitbox.transform.localScale += runTimeHitbox.transform.localScale.normalized;
				runTimeHitbox.SetActive (true);

				runTimeBullet = Instantiate (myBullet, new Vector3(0,0,0), Quaternion.identity);
				Vector3 help = new Vector3 (b_x, b_y, b_z);
				runTimeBullet.transform.position = help;
				runTimeBullet.SetActive (true);
				helpme.shootReload ();
			}
		}
	}

	public void initMyBulletType(int id){
		myBullet = bullets [id];
	}

	public void initMyHitbox(int id){
		hitbox = hitboxes [id];
	}

}
