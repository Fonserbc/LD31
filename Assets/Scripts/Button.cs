using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Sprite Pressed;
	public Sprite NotPressed;
	
	public bool winner = false;
	public bool loser = false;
	public bool switchAct = false;
	
	public bool autoFix = true;
	
	SpriteRenderer sprite;
	
	public bool state;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		
		SetState(state);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Tongue") {
			SetState(switchAct? !state : true);
			
			if (winner) {
				SendMessageUpwards("Win");
			}
			else if (loser) {
				SendMessageUpwards("Lose");				
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (autoFix && col.gameObject.tag == "Tongue") {
			SetState(false);
		}
	}
	
	public void SetState(bool pressed) {
		if (sprite) {
			if (pressed) {
				sprite.sprite = Pressed;		
			}
			else {
				sprite.sprite = NotPressed;
			}
		}
		state = pressed;
	}
}
