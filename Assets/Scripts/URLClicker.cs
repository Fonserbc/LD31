using UnityEngine;
using System.Collections;

public class URLClicker : MonoBehaviour {

	public string URL;
	
	void OnMouseDown() {
		if (URL.Length > 0) {
			Application.OpenURL(URL);
		}
	}
}
