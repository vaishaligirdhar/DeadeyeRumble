public class Attributes : BaseParameters {
	private string _name;
	public Attributes() {
		_name = "";
		//Level up mechanics can be added here
	}

	public string Name {
		get { return _name; }
		set { _name = value; }
	}
}

public enum AttributeName {
	Health,
	Damage,
	Firing_Rate,
	Vertical_Movement,
	Horizontal_Movement
}
