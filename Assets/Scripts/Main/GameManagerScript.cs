using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public Text leftSenbei;
	public Text leftPlayer;

	public static bool[] stageClear = new bool[21];

	public static int playerNum;
	public static int senbeiNum;
	public static bool passedCheckPoint;

	public static int playingStage = 0;

	private GameObject player;
	private GameObject checkPoint;

	public enum GameMode{
		NORMAL,
		STOP,
		DEAD,
		MESSAGE
	}

	// Use this for initialization
	void Start () {
		LoadSound ();
		Sound.PlayBgm ("default");


		passedCheckPoint = false;

		player = GameObject.Find ("player");
		checkPoint = GameObject.Find ("CheckPoint");
		putPlayerOnAccuratePosition();

		Load ();
		DisplayReLoad ();
	}


	public void changeGameMode(string newGameMode){
		//GameMode gameMode = newGameMode;

	}

	private void putPlayerOnAccuratePosition(){
		if (PlayerPrefs.GetInt("checkPoint") == 1) {
			player.transform.position = checkPoint.transform.position;
		}
	}

	public void DisplayReLoad(){
		leftPlayer.text = "×" + playerNum.ToString ();
		leftSenbei.text = "×" + senbeiNum.ToString();
	}

	public static void Save(){
		int i = 1;

		while (stageClear [i] == true) {
			print (i);
			i += 1;
		}
		//clearedStageはクリアしたステージまでを１始まりで示す
		PlayerPrefs.SetInt ("clearedStage", i);
		PlayerPrefs.SetInt ("senbeiNum", senbeiNum);
		PlayerPrefs.SetInt ("playerNum", playerNum);

		PlayerPrefs.Save ();
	}

	public static void Load(){
		/*
		int senbeiLeft = PlayerPrefs.GetInt ("senbeiNum");
		GameManagerScript.senbeiNum = senbeiLeft;
		*/
		int playerLeft = PlayerPrefs.GetInt ("playerNum");
		GameManagerScript.playerNum = playerLeft;
	}
		

	private void LoadSound(){
		Sound.LoadBgm ("default", "bgm_1");
		Sound.LoadSe ("jump", "jump");
	}

}
