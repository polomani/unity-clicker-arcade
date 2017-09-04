using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;

public class GPGDirector : MonoBehaviour {

	void Start () {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated) Authenticate(null);
	}

    void AuthenticateAndDo(Action action)
    {
        if (Social.localUser.authenticated)
        {
            action();
        }
        else
        {
            Authenticate((bool success) =>
            {
                if (success) action();
            });
        }
    }

    public void ReportScore(int score)
    {
        AuthenticateAndDo(() =>
        {
            Social.ReportScore(score, GPGSIds.leaderboard_best_players, (bool success1) =>
            {
                // handle success or failure
            });
        });
    }

    public void ReportProgress()
    {
        AuthenticateAndDo(() =>
        {
            Social.ReportProgress(GPGSIds.achievement_test_achievement, 100.0f, (bool success1) =>
            {
                // handle success or failure
            });
        });
    }

    public void Authenticate(Action<bool> callback)
    {
        Social.localUser.Authenticate(callback);
    }

    public void ShowAchievements()
    {
        AuthenticateAndDo(() =>
        {
            Social.ShowAchievementsUI();
        });  
    }

    public void ShowLeaderboard()
    {
        AuthenticateAndDo(() =>
        {
            Social.ShowLeaderboardUI();
        });    
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

}
