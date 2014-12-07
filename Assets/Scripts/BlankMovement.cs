using UnityEngine;
using System.Collections;

public class BlankMovement : MonoBehaviour {

	public Vector2 min;
	public Vector2 max;
	
	Vector2 dir;
	Vector3 originalPos;
	Vector3 pos = new Vector3();
	float speed = 0.2f;

	// Use this for initialization
	void Init () {
		float r = Random.Range(0.0f, 1.0f);
		
		if (r < 0.25f) {
			dir.x = 1f; dir.y = 1f;
		}
		else if (r < 0.5f) {
			dir.x = 1f; dir.y = -1f;
		}
		else if (r < 0.75f) {
			dir.x = -1f; dir.y = 1f;
		}
		else {
			dir.x = -1f; dir.y = -1f;
		}
		
		originalPos = transform.position;
		pos = originalPos;
	}
	
	// Update is called once per frame
	void Update () {
		pos.x += dir.x*Time.deltaTime*speed;
		pos.y += dir.y*Time.deltaTime*speed;
		
		if (dir.x > 0 && pos.x > max.x) {
			dir.x = -1.0f;
		}
		if (dir.x < 0 && pos.x < min.x) {
			dir.x = 1.0f;
		}
		if (dir.y > 0 && pos.y > max.y) {
			dir.y = -1.0f;
		}
		if (dir.y < 0 && pos.y < min.y) {
			dir.y = 1.0f;
		}
		
		transform.position = pos;
	}
}
