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
	Sprite backgroundSprite;
	bool screenOn = true;
	
	public GameObject[] Action1;
	public GameObject[] Action2;
	public GameObject[] Action3;
	
	public float timeFactor = 1.0f;
	
	public AudioClip winSound, loseSound, prepareSound;
	public AudioSource baseMusic;
	
	MiniGame currentGame;
	GameObject currentGameObject;
	
	void Awake() {
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
		if (screenOn) {
			phoneBackground.sprite = backgroundSprite;		
		}
		else {
			phoneBackground.sprite = blackBackground;		
		}
	
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

    int lastGame = -1;
	void StartGame() {
		int max;
		if (points < 3) { // Easy
			max = Action1.Length;
		}
		else if (points < 10) { // Medium
			max = Action1.Length + Action2.Length;
		}
		else {
			max = Action1.Length + Action2.Length + Action3.Length;
		}
		
		int r = Random.Range(0, max);
        if (r == lastGame) r = (r + 1) % max;
        lastGame = r;

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
			backgroundSprite = currentGame.background;
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
		GetComponent<AudioSource>().PlayOneShot(loseSound);
		
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
		
		backgroundSprite = blackBackground;
		
		stoppedTime = 0.0f;
	}
	
	public void Win () {
        if (!win)
        {
            GetComponent<AudioSource>().PlayOneShot(winSound);

            points++;
            canvas.points.text = points.ToString();

            canvas.points.enabled = true;
            canvas.topText.enabled = false;

            canvas.middleText.text = "SUCCESS!";
            canvas.middleText.enabled = true;

            canvas.bomb.enabled = false;
            canvas.fire.GetComponent<Image>().enabled = false;
            canvas.rope.GetComponent<Image>().enabled = false;

            timeFactor = 0.4f + 0.6f * (1.0f - Mathf.Min(1.0f, ((float)points) / 25.0f));

            win = true;

            stoppedTime = 0.0f;

            int record = 0;
            if (PlayerPrefs.HasKey("record"))
            {
                record = PlayerPrefs.GetInt("record");
            }

            if (points > record)
            {
                PlayerPrefs.SetInt("record", points);
                PlayerPrefs.Save();
            }
        }
	}
	
	public void Prepare() {		
		GetComponent<AudioSource>().PlayOneShot(prepareSound);
	
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
		
		backgroundSprite = blackBackground;
	}
	
	public void GameOver () {
		gameOver = true;
	}
	
	void FixedUpdate() {
		if (Camera.main) transform.position = Camera.main.transform.position;
	}
	
	public void TurnScreen(bool on) {
		screenOn = on;
	}
	
	public void ReduceSound() {
		GetComponent<AudioSource>().volume = 0.05f;
		baseMusic.volume = 0.1f;
	}
	
	public void AugmentSound() {
		GetComponent<AudioSource>().volume = 0.5f;
		baseMusic.volume = 0.7f;
		
	}
}
