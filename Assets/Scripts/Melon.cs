using UnityEngine;
using System.Collections;

public class Melon : MonoBehaviour {

	public Sprite open;

	Tongue t;
	
	bool won = false;

	// Use this for initialization
	void Start () {
		t = GameObject.Find("tongue").GetComponent<Tongue>();
	}
	
	void Init() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.1f, 0.1f), 1.3f);
		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.1f, 0.1f);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Tongue") {
			if (t.delayedCount > 2 && !won) {
				Vector3 aux = t.MovementDirection();
				
				aux.z = 0;
				
				float angle = Vector3.Angle(Vector3.up, aux) - 90.0f;
				
				Vector3 rot = transform.rotation.eulerAngles;
				rot.z = angle;
				
				transform.rotation = Quaternion.Euler(rot);
				
				GetComponent<SpriteRenderer>().sprite = open;
				
				SendMessageUpwards("Win");
				
				Destroy(GetComponent<Rigidbody2D>());
				
				won = true;
			}
		}
		if (col.gameObject.tag == "Die") {
			Destroy(gameObject);
		}
	}
}
