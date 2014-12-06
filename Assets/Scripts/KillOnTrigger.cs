using UnityEngine;
using System.Collections;

public class KillOnTrigger : MonoBehaviour {

	public Tongue tongue;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("Die");
			col.gameObject.GetComponent<Movement>().enabled = false;
			col.gameObject.collider2D.enabled = false;
			
			col.gameObject.transform.position = transform.position;
			col.gameObject.transform.parent = transform;
			
			tongue.Eat();
		}
		
	}
	
	void OnTriggerExit2D (Collider2D col) {
	
	}
}
