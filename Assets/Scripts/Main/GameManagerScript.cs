using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Sound.LoadBgm ("bgm1", "theme1");

		Sound.PlayBgm ("bgm1");
	}
	

}
