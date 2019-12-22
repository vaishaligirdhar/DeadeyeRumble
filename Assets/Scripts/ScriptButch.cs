using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButch : MonoBehaviour {

	public string characterName;
	public int health;
	public int damage;
	public int reloadSpeed;
	public int hSpeed;
	public int vSpeed;
	GameObject xHair;
	GameObject bulletMark;

	public enum parameterEvaluation {
		veryLow = 1,
		low = 2,
		standard = 3,
		high = 4,
		veryHigh = (int) 5
	};
			
	public enum params_Butch {
		_health = parameterEvaluation.standard,
		_damage = parameterEvaluation.low,
		_reloadSpeed = parameterEvaluation.veryHigh,
		_hSpeed = parameterEvaluation.standard,
		_vSpeed = parameterEvaluation.standard,
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void initButch() {
		characterName = transform.name;
		health = params_Butch._health.GetHashCode ();
		damage = params_Butch._damage.GetHashCode ();
		reloadSpeed = params_Butch._reloadSpeed.GetHashCode ();
		hSpeed = params_Butch._hSpeed.GetHashCode ();
		vSpeed = params_Butch._vSpeed.GetHashCode ();

	}
}
