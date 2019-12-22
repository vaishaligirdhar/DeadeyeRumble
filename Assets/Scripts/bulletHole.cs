using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHole : MonoBehaviour {
	private int oppPart = 0;
	private int part = 0;
	private int player = 0;
	private int oppPlayer = 0;
	public int damage = 0;
	public int oppDamage = 10;
	int count = 1;

	public GameplayManager scriptInstance;
	public GameplayManager manager;

	// Use this for initialization
	void Start () {
		count = 1;
		manager = GameObject.Find ("Player1_ScreenCanvas").GetComponent<GameplayManager> ();

	}

	void Update () {
		count++;
		if (count % 10 == 0) {
			count = 1;
			manager.reduceEnemyHealth (damage);
		}

	}

	// Update is called once per frame

	public void OnTriggerEnter(Collider obj) {
		//if (count == 1) {

		Debug.Log (this.transform.parent.parent.parent.name);
		if(this.transform.parent.parent.parent.name.Contains("PlayerUI") && !this.transform.parent.parent.name.Contains ("AI")){	
			string name = obj.gameObject.name;
		
			if (name.Contains ("w")) {
				Debug.LogError (name);
				player = 6;
				if (name.Contains ("deah")) {
					part = 4;
					damage += part * player;
				} else if (name.Contains ("osrot")) {
					part = 3;
					damage += part * player;
				} else if (name.Contains ("mra")) {
					part = 2;
					damage += part * player;
				} else if (name.Contains ("sgel")) {
					part = 1;
					damage += part * player;
				}

			} else if (name.Contains ("x")) {
				Debug.LogError (name);

				player = 7;
				if (name.Contains ("deah")) {
					part = 4;
					damage += part * player;
				} else if (name.Contains ("osrot")) {
					part = 3;
					damage += part * player;
				} else if (name.Contains ("mra")) {
					part = 2;
					damage += part * player;
				} else if (name.Contains ("sgel")) {
					part = 1;
					damage += part * player;
				}
			} else if (name.Contains ("y")) {
				Debug.LogError (name);

				player = 10;
				if (name.Contains ("deah")) {
					part = 4;
					damage += part * player;
				} else if (name.Contains ("osrot")) {
					part = 3;
					damage += part * player;
				} else if (name.Contains ("mra")) {
					part = 2;
					damage += part * player;
				} else if (name.Contains ("sgel")) {
					part = 1;
					damage += part * player;
				}
			
			} else if (name.Contains ("z")) {
				Debug.LogError (name);

				player = 5;
				if (name.Contains ("deah")) {
					part = 4;
					damage += part * player;
				} else if (name.Contains ("osrot")) {
					part = 3;
					damage += part * player;
				} else if (name.Contains ("mra")) {
					part = 2;
					damage += part * player;
				} else if (name.Contains ("sgel")) {
					part = 1;
					damage += part * player;
				}		
			}
				
			oppDamage += 0;

			Debug.Log (name + damage);
		}
			
		//manager.reduceEnemyHealth (damage);

			//manager.reducePlayerHealth (damage);
			//manager.reduceEnemyHealth (oppDamage);
		//manager.reduceEnemyHealth (damage);

		}


	//GameplayManager temp = transform.parent.parent.parent.parent.GetComponent<GameplayManager>();
	//scriptInstance = transform

}