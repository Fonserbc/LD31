using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 1.5f;

	Animator anim;
	
	Vector2 vel = new Vector3();
	
	Logic l;
	
	public bool covered = true;

	// Use this for initialization
	void Start () {
		covered = true;
	
		anim = GetComponent<Animator>();
		anim.SetBool("cover", covered);
		
		l = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
	}
	
	void Update() {
        if (covered && Input.GetButtonDown("Jump"))
        {
            covered = false;
        }
        else if (!covered && Input.GetButtonUp("Jump")) {
            covered = true;
        }
		
		anim.SetBool("cover", covered);
	}
	
	void FixedUpdate () {
	
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		
		anim.SetFloat ("h-v", Mathf.Abs(hAxis) < Mathf.Abs(vAxis)? -1.0f : 1.0f);
		
		anim.SetFloat ("horizontal", hAxis);
		anim.SetFloat ("vertical", vAxis);
		
		vel.x = hAxis * speed ;
		vel.y = vAxis * speed;
		
		
		GetComponent<Rigidbody2D>().velocity = vel;	
	}
	
	public void Die() {
		GetComponent<Rigidbody2D>().Sleep();
		anim.SetFloat ("horizontal", 0.0f);
		anim.SetFloat ("vertical", 0.0f);
		
		anim.SetBool("cover", false);
		
		l.GameOver();
	}
}
