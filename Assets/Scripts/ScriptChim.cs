using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptChim : MonoBehaviour {

	public string characterName;
	public int health;
	public int damage;
	public int reloadSpeed;
	public int hSpeed;
	public int vSpeed;
	//GameObject xHair;
	//GameObject bulletMark;

	public enum parameterEvaluation {
		veryLow = 1,
		low = 2,
		standard = 3,
		high = 4,
		veryHigh = (int) 5
	};

	public enum params_Chim{
		_health = parameterEvaluation.high,
		_damage = parameterEvaluation.high,
		_reloadSpeed = parameterEvaluation.low,
		_hSpeed = parameterEvaluation.veryHigh,
		_vSpeed = parameterEvaluation.low,
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initChim() {
		characterName = transform.name;
		health = params_Chim._health.GetHashCode ();
		damage = params_Chim._damage.GetHashCode ();
		reloadSpeed = params_Chim._reloadSpeed.GetHashCode ();
		hSpeed = params_Chim._hSpeed.GetHashCode ();
		vSpeed = params_Chim._vSpeed.GetHashCode ();
	}
}
