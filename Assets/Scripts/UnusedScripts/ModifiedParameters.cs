using System.Collections.Generic;

public class ModifiedParameters : BaseParameters {

	private List<ModifiableAttribute> _mods;
	private int _modValue; //Might have to be changed to float for mouse movement

	public ModifiedParameters() {
		_mods = new List<ModifiableAttribute> ();
		_modValue = 0;
	}

	public void AddModifier( ModifiableAttribute mod ) {
		_mods.Add (mod);
	}

	private void CalculateModValue() {
		_modValue = 0;

		if (_mods.Count > 0) {
			foreach (ModifiableAttribute att in _mods)
				_modValue += (int) (att.attribute.CalculatedBaseValue * att.ratio);
		}
	}

	public new int CalculatedBaseValue {
		get { return BaseValue + BuffValue + _modValue; }
	}

	public void Revise() {
		CalculateModValue ();
		//Add more functions that you want to trigger when our stats change
	}

}

public struct ModifiableAttribute {
	public Attributes attribute;
	public float ratio;

	public ModifiableAttribute(Attributes att, float r) {
		attribute = att;
		ratio = r;
	}

}
