using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSplit : MonoBehaviour {

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

	public enum params_Split{
		_health = parameterEvaluation.low,
		_damage = parameterEvaluation.standard,
		_reloadSpeed = parameterEvaluation.high,
		_hSpeed = parameterEvaluation.high,
		_vSpeed = parameterEvaluation.high,
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initSplit() {
		characterName = transform.name;
		health = params_Split._health.GetHashCode ();
		damage = params_Split._damage.GetHashCode ();
		reloadSpeed = params_Split._reloadSpeed.GetHashCode ();
		hSpeed = params_Split._hSpeed.GetHashCode ();
		vSpeed = params_Split._vSpeed.GetHashCode ();
	}
}
