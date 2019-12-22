public class Skills : ModifiedParameters {
	private bool _knows;

	public Skills() {
		_knows = false;

	}

	public bool Knows {
		get{ return _knows; }
		set{ _knows = value; }
	}
		
}

public enum SkillName {
	Standard_Shot,
	Double_Shot,
	//Fast_Reload,
	//Large_Shot,
	//Persistent_Shot
}
