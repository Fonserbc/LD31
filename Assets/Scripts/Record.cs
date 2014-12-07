using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Record : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text t = GetComponent<Text>();
		
		int r = 0;
		if (PlayerPrefs.HasKey("record")) {
			r = PlayerPrefs.GetInt("record");
		}
		
		t.text = "RECORD "+r+" POINTS";
    }
}
