using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasHolder : MonoBehaviour {

	public Text topText;
	public RectTransform fire;
	public Image bomb;
	public RectTransform rope;
	
	public Text middleText;
	public Text points;
	public Image lives;
	
	Vector2 anchorMin = new Vector2();
	Vector2 anchorMax = new Vector2();
	
	Vector3 originalScale = new Vector3();
	Vector3 wantedScale = new Vector3();
	RectTransform trans;
	public bool on = true;
	
	public Logic l;
		
	void Start() {
		trans = GetComponent<RectTransform>();
		anchorMin.y = anchorMax.y = 0;
		originalScale = trans.localScale;
		wantedScale = originalScale;
		l = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
	}

	public void SetTimeFactor (float timeFactor) {
		anchorMin.x = anchorMax.x = Mathf.Max(0.06f, timeFactor);
		
		fire.anchorMin = anchorMin;
		fire.anchorMax = anchorMax;
		rope.anchorMax = anchorMax;
	}
	
	void FixedUpdate() {
		trans.localScale = Vector3.Lerp(trans.localScale, wantedScale, Time.deltaTime*10.0f);
	}
	
	public void PressedButton() {
		on = !on;			
		wantedScale = on? originalScale : Vector3.zero;
		
		l.TurnScreen(on);
	}
}
