using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	bool won = false;
	
	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "Tongue") {
			Vector3 aux = col.gameObject.transform.position;
			
			aux.z = transform.position.z;
			
			transform.position = aux;
		}
		else if (!won && col.gameObject.name == "cat") {
			SendMessageUpwards("Win");
			won = true;
		}
	}
}
