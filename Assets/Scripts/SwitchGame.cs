using UnityEngine;
using System.Collections;

public class SwitchGame : MonoBehaviour {

	public Button left;
	public Button right;
	
	public MiniGame game;
	
	bool toDo;
	
	bool won = false;

	// Use this for initialization
	void Init () {
		
	}
	
	void Randomize() {
		float r = Random.Range(0.0f, 1.0f);
		
		toDo = (r < 0.5f);
		
		left.SetState(!toDo);
		right.SetState(!toDo);
		
		if (r < 0.25f) {
			left.SetState(toDo);
		}
		else if (r > 0.75f) {
			right.SetState(toDo);
		}
		
		game.explain = "TURN EVERYTHING "+(toDo? "ON" : "OFF");
	}
	
	void Update() {
		if (!won && left.state == toDo && right.state == toDo) {
			gameObject.SendMessage("Win");
			won = true;
		}
	}
}
