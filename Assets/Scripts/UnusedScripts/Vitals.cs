public class Vitals : ModifiedParameters {
	private int _curValue;

	public Vitals() {
		_curValue = 0;
	}

	public int CurValue {
		get{ 
			//Prevent overflow of a stat (going over x amount of health when you heal)
			if (_curValue > CalculatedBaseValue)
				_curValue = CalculatedBaseValue; 
			
			return _curValue;
		}
		set{ _curValue = value; }
	}
}

public enum VitalType {
	Health,
	Stamina,
	Special,
}
