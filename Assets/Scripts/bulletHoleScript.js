#pragma strict

function Start () {
	
}
function OnTriggerEnter(obj : Collider) {
var name = obj.gameObject.name;
Debug.Log(name);
 }
