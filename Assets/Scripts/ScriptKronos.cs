using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptKronos : MonoBehaviour {

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

	public enum params_Kronos{
		_health = parameterEvaluation.veryHigh,
		_damage = parameterEvaluation.veryHigh,
		_reloadSpeed = parameterEvaluation.veryLow,
		_hSpeed = parameterEvaluation.veryLow,
		_vSpeed = parameterEvaluation.veryLow,
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initKronos() {
		characterName = transform.name;
		health = params_Kronos._health.GetHashCode ();
		damage = params_Kronos._damage.GetHashCode ();
		reloadSpeed = params_Kronos._reloadSpeed.GetHashCode ();
		hSpeed = params_Kronos._hSpeed.GetHashCode ();
		vSpeed = params_Kronos._vSpeed.GetHashCode ();
	}
}
