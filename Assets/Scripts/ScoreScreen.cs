using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public Text Player1Score;
    public Text Player2Score;
    public Text Player3Score;
    public Text Player4Score;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Application.loadedLevel == 4)
        {
            Debug.Log(PlayersPlaying.player1Points);
            Debug.Log(PlayersPlaying.player2Points);
            Debug.Log(PlayersPlaying.player3Points);
            Debug.Log(PlayersPlaying.player4Points);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Application.LoadLevel(0);
        }
        PlayerScores();
	}
    void PlayerScores()
    {
        Player1Score.text = "Player 1 end score: " + PlayersPlaying.player1Points;
        Player2Score.text = "Player 2 end score: " + PlayersPlaying.player2Points;
        Player3Score.text = "Player 3 end score: " + PlayersPlaying.player3Points;
        Player4Score.text = "Player 4 end score: " + PlayersPlaying.player4Points;
    }
}
