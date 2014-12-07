using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Sprite Pressed;
	public Sprite NotPressed;
	
	public bool winner = false;
	
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = NotPressed;
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Tongue") {
			sprite.sprite = Pressed;
			
			if (winner) {
				SendMessageUpwards("Win");
			}
			else {
				SendMessageUpwards("Lose");				
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Tongue") {
			sprite.sprite = NotPressed;
		}
	}
}
