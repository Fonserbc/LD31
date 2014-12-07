using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logic : MonoBehaviour {

	CanvasHolder canvas;
	
	int lives = 3;
	int points = 0;
	
	bool gameOver = false;
	bool lost = false;
	float restartTime = 3.0f;
	float deadTime = 0.0f;
	
	float gameTime = 5.0f;
	
	float currTime = 0.0f;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		if (Application.loadedLevel == 0) Application.LoadLevel(Application.loadedLevel+1);
	}
	
	void OnLevelWasLoaded() {
		canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasHolder>();
		
		lives = 3;
		points = 0;
		gameOver = false;
		deadTime = 0.0f;
				
		canvas.lives.fillAmount = (float) lives / 3.0f;
		canvas.points.text = points.ToString();		
		canvas.middleText.enabled = true;
		canvas.middleText.text = "PREPARE!";
		canvas.topText.enabled = false;
		
		canvas.bomb.enabled = false;
		canvas.fire.GetComponent<Image>().enabled = false;
		canvas.rope.GetComponent<Image>().enabled = false;
		
		lost = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currTime -= Time.deltaTime;
		
		if (currTime < 0.0f) {
			currTime = 0.0f;
			if (!lost) Lose();
		}
		
		canvas.SetTimeFactor(currTime / gameTime);
		
		if (gameOver || lost) {
			deadTime += Time.deltaTime;
			
			if (deadTime > restartTime) {
				if (gameOver) {
					Application.LoadLevel(Application.loadedLevel);
				}
				else if (lost) {
					StartGame();
				}
			}
		}
		else {			
			deadTime = 0.0f;
		}
	}
	
	void StartGame(/*MiniGame game*/) {
		currTime = gameTime;
		
		canvas.middleText.enabled = false;
		canvas.points.enabled = true;
		canvas.topText.enabled = true;
		
		canvas.topText.text = "WHAT TO DO";
		lost = false;
				
		canvas.bomb.enabled = true;
		canvas.fire.GetComponent<Image>().enabled = true;
		canvas.rope.GetComponent<Image>().enabled = true;
	}
	
	public void Lose() {
		lives--;
		
		canvas.lives.fillAmount = (float) lives / 3.0f;
		
		canvas.points.enabled = false;
		canvas.topText.enabled = false;
		
		canvas.middleText.text = "FAILED!";
		canvas.middleText.enabled = true;
		
		canvas.bomb.enabled = false;
		canvas.fire.GetComponent<Image>().enabled = false;
		canvas.rope.GetComponent<Image>().enabled = false;
		
		if (lives <= 0) {			
			canvas.middleText.text = "GAME OVER\n POINTS: "+points.ToString();	
			GameOver();
		}
		
		lost = true;
	}
	
	public void Win () {
		points ++;
		canvas.points.text = points.ToString();
		
		canvas.points.enabled = true;
		canvas.topText.enabled = false;		
		
		canvas.middleText.text = "SUCCESS!";
		canvas.middleText.enabled = true;
		
	}
	
	public void GameOver () {
		gameOver = true;
	}
	
	void FixedUpdate() {
		if (Camera.main) transform.position = Camera.main.transform.position;
	}
}
