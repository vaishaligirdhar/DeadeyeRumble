
#pragma strict

 private var movement : Vector3;

function Start () {
}
function Update () {
	 movement = Vector3.zero;
	 if (Input.GetKey (KeyCode.W))
		 movement.y+=3f;
	 if (Input.GetKey (KeyCode.S))
		 movement.y-=3f;
	 if (Input.GetKey (KeyCode.A))
		 movement.x-=3f;
	 if (Input.GetKey (KeyCode.D))
		 movement.x+=3f;
	 transform.Translate( movement * Time.deltaTime );
}