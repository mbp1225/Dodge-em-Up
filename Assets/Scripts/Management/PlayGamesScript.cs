using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using TMPro;

public class PlayGamesScript : MonoBehaviour
{
	public static PlayGamesScript instance;

	[SerializeField] TextMeshProUGUI playerName;

	void Awake()
    {
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			
		}
		else if(instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void Start ()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

		SignIn();

		print("Started");
	}

	void SignIn()
	{
		Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                    }
                });
		print("Signed In");
		playerName.text = Social.Active.localUser.userName;

	}

	#region Leaderboards

	public static void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		print(leaderboardId);
		Social.ReportScore(score, GPGSIds.leaderboard_highscores, success => {});
		print("Score Added");
	}

	public static void ShowLeaderboardsUI()
	{
		Social.ShowLeaderboardUI();
		print("Leaderboard Shown");
	}

	#endregion /Leaderboards
}
