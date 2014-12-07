using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {

	public GameObject tongueEnd;
	
	public bool tongueOut;
	
	public AudioSource enlarge;
	public AudioSource thugh;

	Vector3 originalPos;
	float shakeTime = 0.15f;
	float lastThrow = -10.0f;
	float lastSeen = -10.0f;
	Vector2 shakeAmount = new Vector2(0.05f, 0.05f);
	Vector3 shakePosition = new Vector3();
	
	LineRenderer line;
	
	Vector3 wantedTonguePosition = new Vector3();
	float tongueSpeed = 20.0f;
	float outTime = 1.5f;
	
	float delay = 0.3f;
	struct Delayed {
		public float time;
		public Vector3 position;
	};
	Delayed[] delayeds;
	
	int delayedIt = -1;
	int delayedCount = 0;
	
	
	GameObject player;
	bool wasCovered = true;	
	Movement mov;
	
	bool eaten = false;
	
	Logic l;

	// Use this for initialization
	void Start () {
		l = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
	
		originalPos = transform.position;
		wantedTonguePosition = transform.position;
		tongueEnd.transform.position = transform.position;
		
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.position);
		line.SetPosition(1, tongueEnd.transform.position);
		
		tongueOut = false;
		
		player = GameObject.FindGameObjectWithTag("Player");
		mov = player.GetComponent<Movement>();
		
		delayeds = new Delayed[64];
		for (int i = 0; i < delayeds.Length; ++i) {
			delayeds[i].position = new Vector3();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!eaten) {
			// Delay
			if (tongueOut) {		
				if (!mov.covered) {
					delayeds[delayedCount%delayeds.Length].time = Time.time + delay;
					delayeds[delayedCount%delayeds.Length].position = player.transform.position;
					++delayedCount;
				}
				
				while (delayedIt + 1 < delayedCount && delayeds[(delayedIt + 1)%delayeds.Length].time < Time.time) {
					delayedIt++;
				}
			}	
			//
			
			if (!mov.covered) {
				lastSeen = Time.time;
				
				if (wasCovered) {
					lastSeen = Time.time;
					
					delayedIt = -1;
					delayedCount = 0;
					
					delayeds[delayedCount%delayeds.Length].time = Time.time + delay;
					delayeds[delayedCount%delayeds.Length].position = player.transform.position;
					++delayedCount;
				}
			}
			
			
			
			if (!tongueOut && !mov.covered) {
				Throw();
			}
				
			if (tongueOut && delayedIt >= 0) {
				wantedTonguePosition = Vector3.Lerp(wantedTonguePosition, delayeds[delayedIt%delayeds.Length].position, Time.deltaTime * tongueSpeed);
			}			
		}
		
		tongueEnd.transform.position = Vector3.Lerp(tongueEnd.transform.position, wantedTonguePosition, Time.deltaTime * tongueSpeed);
		
		bool wasEnabled = tongueEnd.collider2D.enabled;
		tongueEnd.collider2D.enabled = Vector3.Distance(tongueEnd.transform.position, wantedTonguePosition) < 0.1f;
		if (!wasEnabled && tongueEnd.collider2D.enabled) {
			thugh.Play();
		}
		
		shakePosition.x = 0; shakePosition.y = 0;
		
		float dTime = Time.time - lastThrow;
		float seenTime = Time.time - lastSeen;
		if (dTime > delay && dTime < shakeTime + delay) {
			shakePosition.x += Random.Range(-shakeAmount.x, shakeAmount.x);
			shakePosition.y += Random.Range(-shakeAmount.y, shakeAmount.y);
		}
		else if (tongueOut && seenTime > outTime*l.timeFactor && mov.covered) {
			Retrieve();
		}
		
		transform.position = originalPos + shakePosition;
		
		line.SetPosition(0, transform.position);
		line.SetPosition(1, tongueEnd.transform.position);
		
		wasCovered = mov.covered;
	}
	
	void Throw () {
		lastThrow = Time.time;
		tongueOut = true;
		enlarge.Play();
	}
	
	public void Eat() {
		eaten = true;
	
		Retrieve();
	}
	
	void Retrieve() {
		tongueOut = false;
		
		wantedTonguePosition = originalPos;
		
		l.AugmentSound();
	}
}
