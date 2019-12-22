#pragma strict
public var bullet : GameObject;
//public var theController : Animator;
//public var theAnimation : Animation;
//public var testArray : AnimatorControllerParameter;

function Start () {
	//theAnimation = GetComponent.<Animation>();
	//theAnimation.speed = 10;
	//GetComponent.<Animation>().speed = 10;
}
function Update () {
 // GetAxis() returns a value between 1 and -1. Depending on which arrow key is
 //pressed. So we change the spaceship X speed by GetAxis("Horizontal") * 10
 GetComponent.<Rigidbody>().velocity.x = Input.GetAxis("Horizontal") * 10;
 GetComponent.<Rigidbody>().velocity.y = Input.GetAxis("Vertical") * 10;
 if (Input.GetKeyDown("space")) {
 // Create a new bullet at “transform.position” which is the current position of the ship
 Instantiate(bullet, transform.position, Quaternion.identity);
 Debug.Log("space");
 }
}

function OnCollisionEnter(){
 GetComponent.<Rigidbody>().velocity=Vector3.zero;
 GetComponent.<Rigidbody>().angularVelocity=Vector3.zero;
}