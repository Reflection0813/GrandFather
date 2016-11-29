using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	public Sprite[] sprites;
	public GameObject obj;
	private float changeFrameSecond = 0.05f;
	private float dTime;
	private int frameNum;

	// Use this for initialization
	void Start () {
		dTime = 0.0f;
		frameNum = 0;
	}

	// Update is called once per frame
	void Update () {
		dTime += Time.deltaTime;
		if (changeFrameSecond < dTime) {
			dTime = 0.0f;
			frameNum++;
			//if(frameNum >= sprites.Length + 2) frameNum = 0;
			if (frameNum >= 3)
				Destroy (gameObject);
		}
		//print ("frameNum = " + frameNum);
		if( frameNum < sprites.Length ) gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [frameNum];
	}
}
