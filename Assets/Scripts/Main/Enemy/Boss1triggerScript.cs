using UnityEngine;
using System.Collections;

public class Boss1triggerScript : MonoBehaviour {
	public GameObject boss;
	public GameObject polls;

	private bool trigger;

	void OnTriggerEnter2D(Collider2D col){


		if (col.gameObject.tag == "Player" && !trigger) {
			trigger = true;
			Sound.LoadBgm ("boss1", "boss1");
			Sound.PlayBgm ("boss1");


			CameraScript.adjustment = 4;

			StartCoroutine (pollShowUp ());
			StartCoroutine (backgroundChange ());



			boss.SetActive (true);

			//Destroy (gameObject);
		}

	}

	IEnumerator pollShowUp(){
		while(true){
			polls.transform.position += new Vector3 (0, 0.1f, 0);
			if (polls.transform.position.y >= 13)
				break;

			yield return null;
		}
	}

	IEnumerator backgroundChange(){
		while(true){
			Camera.main.backgroundColor -= new Color (0.05f, 0.1f, 0.05f);
			if (Camera.main.backgroundColor.g <= 0.1f)
				break;

			yield return null;
		}
	}
}
