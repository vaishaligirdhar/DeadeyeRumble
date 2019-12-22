using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Get enum class


public class BaseCharacter : MonoBehaviour {
	private string _name;

	private Attributes[] _theAttributes;
	private Vitals[] _theVitals;
	private Skills[] _theSkills;

	public void Awake() {
		_name = string.Empty;

		_theAttributes = new Attributes[Enum.GetValues(typeof(AttributeName)).Length];
		_theVitals = new Vitals[Enum.GetValues(typeof(VitalType)).Length];
		_theSkills = new Skills[Enum.GetValues(typeof(SkillName)).Length];

		InitAttributes ();
		InitVitals ();
		InitSkills ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string Name {
		get { return _name; }
		set { _name = value; }
	}

	//Add exp function would be here were we to have it

	private void InitAttributes() {
		for (int i = 0; i < _theAttributes.Length; i++) {
			_theAttributes [i] = new Attributes ();
		}
	}
	private void InitVitals() {
		for (int i = 0; i < _theVitals.Length; i++) {
			_theVitals [i] = new Vitals ();
		}
	}
	private void InitSkills() {
		for (int i = 0; i < _theSkills.Length; i++) {
			_theSkills [i] = new Skills ();
		}
	}

	public Attributes GetAttribute(int index) {
		return _theAttributes [index];
	}
	public Vitals GetVital(int index) {
		return _theVitals [index];
	}
	public Skills GetSkill(int index) {
		return _theSkills [index];
	}

	private void ReviseVitalModifiers() {
		// health
		ModifiableAttribute healthModifier = new ModifiableAttribute();
		healthModifier.attribute = GetAttribute ((int)AttributeName.Health);
		healthModifier.ratio = 0.5f;

		GetVital ((int)VitalType.Health).AddModifier (healthModifier);

		//stamina
		ModifiableAttribute staminaModifier = new ModifiableAttribute();
		staminaModifier.attribute = GetAttribute ((int)AttributeName.Health);
		staminaModifier.ratio = 1.0f;

		GetVital ((int)VitalType.Stamina).AddModifier (staminaModifier);

		//special
		ModifiableAttribute specialModifier = new ModifiableAttribute();
		specialModifier.attribute = GetAttribute ((int)AttributeName.Damage);
		staminaModifier.ratio = 0.2f;

		GetVital ((int)VitalType.Special).AddModifier (specialModifier);

	}

	private void ReviseSkillModifiers() {
		//Standard shot
		ModifiableAttribute StandardShotModifier1 = new ModifiableAttribute ();
		ModifiableAttribute StandardShotModifier2 = new ModifiableAttribute ();
		ModifiableAttribute StandardShotModifier3 = new ModifiableAttribute ();
		ModifiableAttribute StandardShotModifier4 = new ModifiableAttribute ();
		ModifiableAttribute StandardShotModifier5 = new ModifiableAttribute ();

		StandardShotModifier1.attribute = GetAttribute ((int)AttributeName.Damage);
		StandardShotModifier1.ratio = .33f;

		StandardShotModifier2.attribute = GetAttribute ((int)AttributeName.Firing_Rate);
		StandardShotModifier2.ratio = .33f;

		StandardShotModifier3.attribute = GetAttribute ((int)AttributeName.Vertical_Movement);
		StandardShotModifier3.ratio = .33f;

		StandardShotModifier4.attribute = GetAttribute ((int)AttributeName.Horizontal_Movement);
		StandardShotModifier4.ratio = .33f;

		StandardShotModifier5.attribute = GetAttribute ((int)AttributeName.Health);
		StandardShotModifier5.ratio = .33f;

		GetSkill ((int)SkillName.Standard_Shot).AddModifier (StandardShotModifier1);
		GetSkill ((int)SkillName.Standard_Shot).AddModifier (StandardShotModifier2);
		GetSkill ((int)SkillName.Standard_Shot).AddModifier (StandardShotModifier3);
		GetSkill ((int)SkillName.Standard_Shot).AddModifier (StandardShotModifier4);
		GetSkill ((int)SkillName.Standard_Shot).AddModifier (StandardShotModifier5);

		//Double shot
		ModifiableAttribute DoubleShotModifier1 = new ModifiableAttribute ();
		ModifiableAttribute DoubleShotModifier2 = new ModifiableAttribute ();
		ModifiableAttribute DoubleShotModifier3 = new ModifiableAttribute ();
		ModifiableAttribute DoubleShotModifier4 = new ModifiableAttribute ();
		ModifiableAttribute DoubleShotModifier5 = new ModifiableAttribute ();

		DoubleShotModifier1.attribute = GetAttribute ((int)AttributeName.Damage);
		DoubleShotModifier1.ratio = .33f;

		DoubleShotModifier2.attribute = GetAttribute ((int)AttributeName.Firing_Rate);
		DoubleShotModifier2.ratio = .33f;

		DoubleShotModifier3.attribute = GetAttribute ((int)AttributeName.Vertical_Movement);
		DoubleShotModifier3.ratio = .33f;

		DoubleShotModifier4.attribute = GetAttribute ((int)AttributeName.Horizontal_Movement);
		DoubleShotModifier4.ratio = .33f;

		DoubleShotModifier5.attribute = GetAttribute ((int)AttributeName.Health);
		DoubleShotModifier5.ratio = .33f;

		GetSkill ((int)SkillName.Double_Shot).AddModifier (DoubleShotModifier1);
		GetSkill ((int)SkillName.Double_Shot).AddModifier (DoubleShotModifier2);
		GetSkill ((int)SkillName.Double_Shot).AddModifier (DoubleShotModifier3);
		GetSkill ((int)SkillName.Double_Shot).AddModifier (DoubleShotModifier4);
		GetSkill ((int)SkillName.Double_Shot).AddModifier (DoubleShotModifier5);

	}

	public void ReviseStats() {
		for (int i = 0; i < _theVitals.Length; i++) {
			_theVitals [i].Revise ();
		}

		for (int i = 0; i < _theSkills.Length; i++) {
			_theSkills [i].Revise ();
		}
	}

}
