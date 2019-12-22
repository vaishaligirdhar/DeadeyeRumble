using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oppbulletHole : MonoBehaviour {
	private int oppPart = 0;
	private int part = 0;
	private int player = 0;
	public int oppPlayer = 0;
	public int damage = 0;
	public int oppDamage = 0;
	int count = 0;

	public GameplayManager scriptInstance;
	public GameplayManager manager;

	// Use this for initialization
	void Start () {
		count = 1;
		manager = GameObject.Find ("Player1_ScreenCanvas").GetComponent<GameplayManager> ();

	}

	void Update () {
		count++;
		if (count %10 == 0) {
			count = 1;
			manager.reducePlayerHealth (oppDamage);
			Debug.Log ("player");
		}
			
	}

	// Update is called once per frame

	public void OnTriggerEnter(Collider obj) {
		//if (count == 1) {

		Debug.Log (this.transform.parent.parent.name);

			string name = obj.gameObject.name;

		if (this.transform.parent.parent.name.Contains ("AI") && !this.transform.parent.parent.parent.name.Contains("PlayerUI")){
			

			if (name.Contains ("g")) {
				Debug.LogError (name);

				oppPlayer = 6;
				if (name.Contains ("head")) {
					oppPart = 4;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("torso")) {
					oppPart = 3;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("arm")) {
					oppPart = 2;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("legs")) {
					oppPart = 1;
					oppDamage += oppPart * oppPlayer;
				}
			} else if (name.Contains ("r")) {
				Debug.LogError (name);

				oppPlayer = 7;
				if (name.Contains ("head")) {
					oppPart = 4;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("torso")) {
					oppPart = 3;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("arm")) {
					oppPart = 2;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("legs")) {
					oppPart = 1;
					oppDamage += oppPart * oppPlayer;
				}
			} else if (name.Contains ("k")) {
				Debug.LogError (name);

				oppPlayer = 10;
				if (name.Contains ("head")) {
					oppPart = 4;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("torso")) {
					oppPart = 3;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("arm")) {
					oppPart = 2;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("legs")) {
					oppPart = 1;
					oppDamage += oppPart * oppPlayer;
				}
			} else {
				Debug.LogError (name);

				oppPlayer = 5;
				if (name.Contains ("head")) {
					oppPart = 4;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("torso")) {
					oppPart = 3;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("arm")) {
					oppPart = 2;
					oppDamage += oppPart * oppPlayer;
				} else if (name.Contains ("legs")) {
					oppPart = 1;
					oppDamage += oppPart * oppPlayer;
				}
			}
			damage += 0;
			//oppDamage += oppPart * oppPlayer;


		//GameplayManager manager = GameObject.Find ("Player1_ScreenCanvas").GetComponent<GameplayManager> ();
		//manager.reducePlayerHealth (damage);
		//manager.reduceEnemyHealth (oppDamage);
		//manager.reducePlayerHealth (oppDamage);
		Debug.Log ("opp"+ name + oppDamage);

			//manager.reducePlayerHealth (oppDamage);


		}

	}

	//GameplayManager temp = transform.parent.parent.parent.parent.GetComponent<GameplayManager>();
	//scriptInstance = transform

}