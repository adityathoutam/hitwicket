using UnityEngine;
using System.Collections;

public class ActivateChest : MonoBehaviour {

	public Transform lid, lidOpen, lidClose;	
	public float openSpeed = 5F;				
	public bool canClose;						
	
	[HideInInspector]
	public bool _open;							

	void Update () {
        
		if(_open){
			ChestClicked(lidOpen.rotation);
		}
		else{
			ChestClicked(lidClose.rotation);
		}
	}
	
	
	void ChestClicked(Quaternion toRot){
		if(lid.rotation != toRot){
			lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
		}
	}
	
	void OnMouseDown(){
		if(canClose) _open = !_open; else _open = true;
	}
}
