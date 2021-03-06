﻿using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdScript : MonoBehaviour {
	
	public void ShowRewardedAd()
	{
		print (Advertisement.IsReady ("rewardedVideoZone"));
		if (Advertisement.IsReady("rewardedVideoZone"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideoZone", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
