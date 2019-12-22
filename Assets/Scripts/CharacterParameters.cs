using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParameters : MonoBehaviour {

	#region Local variables
	public string characterName;
	public float health;
	public float damage;
	public float reloadSpeed;
	public float hSpeed;
	public float vSpeed;
	int xHair;
	int bulletMark;
	int bulletType;
	//System.Array enumValues;
	#endregion

	#region Base parameter scaling factor struct and evalution enum
	public struct parameterScalingFactors
	{
		public const int _health = 100;
		public const int _damage = 20;
		public const float _rSpeed = 10f;
		public const float _hSpeed = 10f;
		public const float _vSpeed = 10f;
	};

	public enum parameterEvaluation {
		veryLow = 1,
			low = 2,
		standard = 3,
		high = 4,
		veryHigh = (int) 5
	};
	#endregion

	#region Character parameter enums
	public enum params_Butch {
		_health = parameterEvaluation.standard,
		_damage = parameterEvaluation.low,
		_reloadSpeed = parameterEvaluation.veryHigh,
		_hSpeed = parameterEvaluation.standard,
		_vSpeed = parameterEvaluation.standard,
		_xHair = (int) 0,
		_bulletMark = (int) 0,
		_bulletType = (int) 0
	}

	public enum params_Split{
		_health = parameterEvaluation.low,
		_damage = parameterEvaluation.standard,
		_reloadSpeed = parameterEvaluation.high,
		_hSpeed = parameterEvaluation.high,
		_vSpeed = parameterEvaluation.high,
		_xHair = (int) 1,
		_bulletMark = (int) 1,
		_bulletType = (int) 1
			
	}

	public enum params_Chim{
		_health = parameterEvaluation.high,
		_damage = parameterEvaluation.high,
		_reloadSpeed = parameterEvaluation.low,
		_hSpeed = parameterEvaluation.veryHigh,
		_vSpeed = parameterEvaluation.low,
		_xHair = (int) 2,
		_bulletMark = (int) 2,
		_bulletType = (int) 2
			
	}

	public enum params_Kronos{
		_health = parameterEvaluation.veryHigh,
		_damage = parameterEvaluation.veryHigh,
		_reloadSpeed = parameterEvaluation.veryLow,
		_hSpeed = parameterEvaluation.veryLow,
		_vSpeed = parameterEvaluation.veryLow,
		_xHair = (int) 3,
		_bulletMark = (int) 3,
		_bulletType = (int) 3
			
	}
	#endregion

	#region Debugging stuff
	/*void Start(){
		initCharacter ("Split");
	}*/
	#endregion

	#region initCharacter() used to communicate with initPlayerScreen.cs
	public void initCharacter(string characterName){
		switch (characterName)
		{
			case "Butch":
				{
					initButch ();
					break;
				}
			case "Split":
				{
					initSplit ();
					break;
				}
			case "Chim":
				{
					initChim ();
					break;
				}
			case "Kronos":
				{
					initKronos ();
					break;
				}
			default:
				Debug.Log ("Invalid characterName string passed to initCharacter");
				break;
		}
	}
	#endregion

	#region initCharacterX methods. All the local gameObject value initialisation logic in this region

	void initButch(){


		health = params_Butch._health.GetHashCode () * parameterScalingFactors._health;
		damage = params_Butch._damage.GetHashCode () * parameterScalingFactors._damage;
		reloadSpeed = params_Butch._reloadSpeed.GetHashCode () * parameterScalingFactors._rSpeed;
		hSpeed = params_Butch._hSpeed.GetHashCode () * parameterScalingFactors._hSpeed;
		vSpeed = params_Butch._vSpeed.GetHashCode () * parameterScalingFactors._vSpeed;
		xHair = (int)params_Butch._xHair;
		bulletMark = (int)params_Butch._bulletMark;
		bulletType = (int)params_Butch._bulletType;

	}
		
	void initSplit(){
		//Debug.Log ("Initialising: " + characterName);
		health = params_Split._health.GetHashCode () * parameterScalingFactors._health;
		damage = params_Split._damage.GetHashCode () * parameterScalingFactors._damage;
		reloadSpeed = params_Split._reloadSpeed.GetHashCode () * parameterScalingFactors._rSpeed;
		hSpeed = params_Split._hSpeed.GetHashCode () * parameterScalingFactors._hSpeed;
		vSpeed = params_Split._vSpeed.GetHashCode () * parameterScalingFactors._vSpeed;
		xHair = (int) params_Split._xHair;
		bulletMark = (int)params_Split._bulletMark;
		bulletType = (int)params_Split._bulletType;
		//int dummyTest = (int)params_Split._health;

		/*Debug.Log (characterName + "'s health: " + health.ToString());
		Debug.Log (characterName + "'s damage: " + damage);
		Debug.Log (characterName + "'s reloadSpeed: " + reloadSpeed);
		Debug.Log (characterName + "'s hSpeed: " + hSpeed);
		Debug.Log (characterName + "'s vSpeed: " + vSpeed);
		Debug.Log (characterName + "'s xhair: " + xHair);
		Debug.Log (characterName + "'s bulletmark: " + bulletMark);
		Debug.Log (characterName + "'s dummyTest: " + dummyTest);
		*/
	}

	void initChim(){


		health = params_Chim._health.GetHashCode () * parameterScalingFactors._health;
		damage = params_Chim._damage.GetHashCode () * parameterScalingFactors._damage;
		reloadSpeed = params_Chim._reloadSpeed.GetHashCode () * parameterScalingFactors._rSpeed;
		hSpeed = params_Chim._hSpeed.GetHashCode () * parameterScalingFactors._hSpeed;
		vSpeed = params_Chim._vSpeed.GetHashCode () * parameterScalingFactors._vSpeed;
		xHair = (int)params_Chim._xHair;
		bulletMark = (int)params_Chim._bulletMark;
		bulletType = (int)params_Chim._bulletType;

	}

	void initKronos(){


		health = params_Kronos._health.GetHashCode () * parameterScalingFactors._health;
		damage = params_Kronos._damage.GetHashCode () * parameterScalingFactors._damage;
		reloadSpeed = params_Kronos._reloadSpeed.GetHashCode () * parameterScalingFactors._rSpeed;
		hSpeed = params_Kronos._hSpeed.GetHashCode () * parameterScalingFactors._hSpeed;
		vSpeed = params_Kronos._vSpeed.GetHashCode () * parameterScalingFactors._vSpeed;
		xHair = (int)params_Kronos._xHair;
		bulletMark = (int)params_Kronos._bulletMark;
		bulletType = (int)params_Kronos._bulletType;

	}
	#endregion

}
