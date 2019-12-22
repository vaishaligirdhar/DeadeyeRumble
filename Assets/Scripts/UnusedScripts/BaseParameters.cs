public class BaseParameters {
	private int _baseValue; //Note that level up mechanics can be added here
	private int _buffValue;

	public BaseParameters() {
		_baseValue = 0;
		_buffValue = 0;
	}

	//Basic Setters and Getters
	public int BaseValue {
		get { return _baseValue; }
		set { _baseValue = value; }
	}

	public int BuffValue {
		get { return _buffValue; }
		set { _buffValue = value; }
	}

	public int CalculatedBaseValue {
		get { return _baseValue + _buffValue; }
	}

}
