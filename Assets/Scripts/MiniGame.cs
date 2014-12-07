using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour {

	public float gameTime = 5.0f;
	public string explain = "WHAT TO DO";
	public Sprite background;
	
	public Logic l;

	void Init() {
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Randomize() {
	
	}
	
	void Win() {
		l.Win();
	}
	
	void Lose() {
		l.Lose();
	}
}
