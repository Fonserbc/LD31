using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 2.0f;

	Animator anim;
	
	Vector2 vel = new Vector3();

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
	
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		
		anim.SetFloat ("h-v", Mathf.Abs(hAxis) < Mathf.Abs(vAxis)? -1.0f : 1.0f);
		
		anim.SetFloat ("horizontal", hAxis);
		anim.SetFloat ("vertical", vAxis);
		
		vel.x = rigidbody2D.velocity.x * 0.97f + hAxis * speed * Time.fixedDeltaTime;
		vel.y = rigidbody2D.velocity.y * 0.97f + vAxis * speed * Time.fixedDeltaTime;
		
		
		rigidbody2D.velocity = vel;	
	}
}
