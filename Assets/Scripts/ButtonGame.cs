using UnityEngine;
using System.Collections;

public class ButtonGame : MonoBehaviour {

	public Button[] buttons;

	public MiniGame game;
	
	Color[] colors;
	string [] names;
	
	void Init() {
		colors = new Color[4];
		names = new string[4];
		
		colors[0] = Color.white;
		names[0] = "WHITE";
		colors[1] = Color.blue;
		names[1] = "BLUE";
		colors[2] = Color.red;
		names[2] = "RED";
		colors[3] = Color.green;
		names[3] = "GREEN";
	}
	
	void Randomize() {
		int winnerColor = Random.Range(0, colors.Length);
		int winner = Random.Range(0, buttons.Length);
		buttons[winner].winner = true;
		buttons[winner].GetComponent<SpriteRenderer>().color = colors[winnerColor];
		
		int color;
		for (int i = 0; i < buttons.Length; ++i) {
			if (i != winner) {
				color = Random.Range(0, colors.Length);
				if (color == winnerColor) {
					color = (color + 1)%colors.Length;
				}
				
				buttons[i].GetComponent<SpriteRenderer>().color = colors[color];
			}
		}
		
		game.explain = "PRESS THE "+names[winnerColor]+" BUTTON";
	}
}
