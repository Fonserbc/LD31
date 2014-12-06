using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {

	public GameObject tongueEnd;
	
	public bool tongueOut;

	Vector3 originalPos;
	float shakeTime = 0.15f;
	float lastShake = -10.0f;
	Vector2 shakeAmount = new Vector2(0.05f, 0.05f);
	Vector3 shakePosition = new Vector3();
	
	LineRenderer line;
	
	Vector3 wantedTonguePosition = new Vector3();
	float tongueSpeed = 20.0f;
	float outTime = 1.5f;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		wantedTonguePosition = transform.position;
		tongueEnd.transform.position = transform.position;
		
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.position);
		line.SetPosition(1, tongueEnd.transform.position);
		
		tongueOut = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Test
		
		if (Input.GetMouseButtonDown(0)) {
			
			Throw();
		}
		//
		
		tongueEnd.transform.position = Vector3.Lerp(tongueEnd.transform.position, wantedTonguePosition, Time.deltaTime * tongueSpeed);
	
		shakePosition.x = 0; shakePosition.y = 0;
		
		if (Time.time - lastShake < shakeTime) {
			shakePosition.x += Random.Range(-shakeAmount.x, shakeAmount.x);
			shakePosition.y += Random.Range(-shakeAmount.y, shakeAmount.y);
		}
		else if (tongueOut && Time.time - lastShake > outTime) {
			Retrieve();
		}
	
		transform.position = originalPos + shakePosition;
	
		line.SetPosition(0, transform.position);
		line.SetPosition(1, tongueEnd.transform.position);
	}
	
	void Throw () {
		lastShake = Time.time;
		tongueOut = true;
		
		wantedTonguePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		wantedTonguePosition.z = 0;
	}
	
	void Retrieve() {
		tongueOut = false;
		
		
		wantedTonguePosition = originalPos;
	}
}
