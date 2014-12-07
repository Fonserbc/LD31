using UnityEngine;
using System.Collections;

public class KillOnTrigger : MonoBehaviour {

	public Tongue tongue;
	
	GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.FindGameObjectWithTag("Canvas");	
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("Die");
			col.gameObject.GetComponent<Movement>().enabled = false;
			col.gameObject.collider2D.enabled = false;
			
			Vector3 relPos = col.gameObject.transform.position - transform.position;
			col.gameObject.transform.parent = transform;
			col.gameObject.transform.localPosition = relPos;
			
			tongue.Eat();
		}
		else if (col.gameObject.name == "Menu") {
			canvas.SendMessage("PressedButton");
		}
		else if (col.gameObject.name == "Audio") {
			
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
	
	}
}
