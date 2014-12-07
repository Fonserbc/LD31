using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logic : MonoBehaviour {

	CanvasHolder canvas;
	
	int lives = 3;
	int points = 0;
	
	bool gameOver = false;
	bool lost = false;
	bool win = false;
	bool playing = false;
	float restartTime = 3.0f;
	float stoppedTime = 0.0f;
	
	float gameTime = 5.0f;
	
	float currTime = 0.0f;
	
	public SpriteRenderer phoneBackground;
	public Sprite blackBackground;
	
	public GameObject[] Action1;
	public GameObject[] Action2;
	public GameObject[] Action3;
	
	public float timeFactor = 1.0f;
	
	MiniGame currentGame;
	GameObject currentGameObject;

	// Use this for initialization
	void Awake () {
		if (GameObject.FindGameObjectsWithTag("Logic").Length > 1) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad (gameObject);
			if (Application.loadedLevel == 0) Application.LoadLevel(Application.loadedLevel+1);
			
			OnLevelWasLoaded();
		}
	}
	
	void OnLevelWasLoaded() {
		canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasHolder>();
		
		lives = 3;
		points = 0;
		gameOver = false;
		stoppedTime = 0.0f;
		timeFactor = 1.0f;
		
		Prepare();
	}
	
	Vector3 outPosition = new Vector3();
	
	// Use this for initialization
	void Start () {
		outPosition.z = -100;
	}
	
	
	// Update is called once per frame
	void Update () {
		if (currentGameObject) {
			if (canvas.on) {
				currentGameObject.transform.position = canvas.transform.position;
			}
			else {
				currentGameObject.transform.position = outPosition;
			}
		}
	
		currTime -= Time.deltaTime;
		
		if (currTime < 0.0f) {
			currTime = 0.0f;
			if (playing && !win && !lost) Lose();
		}
		
		if (canvas) canvas.SetTimeFactor(currTime / gameTime);
		
		stoppedTime += Time.deltaTime;
		
		if (stoppedTime > restartTime) {
			stoppedTime = 0.0f;
			if (gameOver) {
				Application.LoadLevel(0);
			}
			else if (lost) {
				Prepare();
				stoppedTime += restartTime/3.0f;
			}
			else if (win) {
				Prepare();
			}
			else if (!playing) {
				StartGame();
			}
		}
	}
	
	void StartGame() {
		int max;
		if (points < 5) { // Easy
			max = Action1.Length;
		}
		else if (points < 10) { // Medium
			max = Action1.Length + Action2.Length;
		}
		else {
			max = Action1.Length + Action2.Length + Action3.Length;
		}
		
		int r = Random.Range(0, max);
		currentGameObject = (GameObject) GameObject.Instantiate(
			(r >= Action1.Length)?
				((r - Action1.Length >= Action2.Length)?
					Action3[r - Action1.Length - Action2.Length] :
					Action2[r - Action1.Length]):
				Action1[r]
		);
		currentGame = currentGameObject.GetComponent<MiniGame>();
		currentGame.l = this;
		currentGame.BroadcastMessage("Init");
		currentGame.BroadcastMessage("Randomize");
		currentGameObject.transform.position = canvas.transform.position;
		
		if (currentGame.background != null) {
			phoneBackground.sprite = currentGame.background;
		}
	
		currTime = currentGame.gameTime * timeFactor;
		gameTime = currentGame.gameTime * timeFactor;
		canvas.topText.text = currentGame.explain;		
		
		canvas.middleText.enabled = false;
		canvas.points.enabled = true;
		canvas.topText.enabled = true;
		
		lost = false;
				
		canvas.bomb.enabled = true;
		canvas.fire.GetComponent<Image>().enabled = true;
		canvas.rope.GetComponent<Image>().enabled = true;
		
		playing = true;
	}
	
	public void Lose() {
		lives--;
		
		canvas.lives.fillAmount = (float) lives / 3.0f;
		
		canvas.points.enabled = true;
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
		
		Destroy(currentGameObject);
		currentGame = null;
		currentGameObject = null;
		
		phoneBackground.sprite = blackBackground;
		
		stoppedTime = 0.0f;
	}
	
	public void Win () {
		points ++;
		canvas.points.text = points.ToString();
		
		canvas.points.enabled = true;
		canvas.topText.enabled = false;		
		
		canvas.middleText.text = "SUCCESS!";
		canvas.middleText.enabled = true;
		
		canvas.bomb.enabled = false;
		canvas.fire.GetComponent<Image>().enabled = false;
		canvas.rope.GetComponent<Image>().enabled = false;
		
		timeFactor = 0.3f + 0.7f * (1.0f - Mathf.Min(1.0f, ((float) points)/25.0f));
		
		win = true;
	}
	
	public void Prepare() {
		canvas.lives.fillAmount = (float) lives / 3.0f;
		canvas.points.text = points.ToString();		
		canvas.middleText.enabled = true;
		canvas.middleText.text = "PREPARE!";
		canvas.topText.enabled = false;
		
		canvas.bomb.enabled = false;
		canvas.fire.GetComponent<Image>().enabled = false;
		canvas.rope.GetComponent<Image>().enabled = false;
		
		lost = false;
		win = false;		
		playing = false;	
	
		Destroy(currentGameObject);
		currentGame = null;
		currentGameObject = null;
		
		phoneBackground.sprite = blackBackground;
	}
	
	public void GameOver () {
		gameOver = true;
		canvas = null;
	}
	
	void FixedUpdate() {
		if (Camera.main) transform.position = Camera.main.transform.position;
	}
}
