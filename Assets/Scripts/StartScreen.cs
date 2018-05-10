using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}
}
