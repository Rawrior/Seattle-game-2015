using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RespawnControl : MonoBehaviour
{
    public Text player1Score;
    public Text player2Score;
    public Text player3Score;
    public Text player4Score;
    public bool player01Dead = false;
    public bool player02Dead = false;
    public bool player03Dead = false;
    public bool player04Dead = false;
    public float respawnTimer01;
    public float respawnTimer02;
    public float respawnTimer03;
    public float respawnTimer04;
    public GameObject deadPlayer01;
    public GameObject deadPlayer02;
    public GameObject deadPlayer03;
    public GameObject deadPlayer04;
    public Vector2 respawnLocation01;
    public Vector2 respawnLocation02;
    public Vector2 respawnLocation03;
    public Vector2 respawnLocation04;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (PointBehavior.player1Points >= 15 || PointBehavior.player2Points >= 15 || PointBehavior.player3Points >= 15 || PointBehavior.player4Points >= 15)
        {
            Application.LoadLevel(4);
        }
        playersToSpawn();
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Application.LoadLevel(0);
        }
        if (player01Dead == true)
        {
            respawnTimer01 += Time.deltaTime;
        }
        if (player02Dead == true)
        {
            respawnTimer02 += Time.deltaTime;
        }
        if (player03Dead == true)
        {
            respawnTimer03 += Time.deltaTime;
        }
        if (player04Dead == true)
        {
            respawnTimer04 += Time.deltaTime;
        }
        if (respawnTimer01 >= 5f)
        {
            Instantiate(deadPlayer01, respawnLocation01, Quaternion.identity);
            respawnTimer01 = 0;
            player01Dead = false;
        }
        if (respawnTimer02 >= 5f)
        {
            Instantiate(deadPlayer02, respawnLocation02, Quaternion.identity);
            respawnTimer02 = 0;
            player02Dead = false;
        }
        if (respawnTimer03 >= 5f)
        {
            Instantiate(deadPlayer03, respawnLocation03, Quaternion.identity);
            respawnTimer03 = 0;
            player03Dead = false;
        }
        if (respawnTimer04 >= 5f)
        {
            Instantiate(deadPlayer04, respawnLocation04, Quaternion.identity);
            respawnTimer04 = 0;
            player04Dead = false;
        }
        playerPointScore();

	}
    void playersToSpawn()
    {
        if (PlayersPlaying.playersPlaying == 2)
        {
            Instantiate(deadPlayer01, respawnLocation01, Quaternion.identity);
            Instantiate(deadPlayer02, respawnLocation02, Quaternion.identity);
            PlayersPlaying.playersPlaying = 0;
        }
        if (PlayersPlaying.playersPlaying == 3)
        {
            Instantiate(deadPlayer01, respawnLocation01, Quaternion.identity);
            Instantiate(deadPlayer02, respawnLocation02, Quaternion.identity);
            Instantiate(deadPlayer03, respawnLocation03, Quaternion.identity);
            PlayersPlaying.playersPlaying = 0;
        }
        if (PlayersPlaying.playersPlaying == 4)
        {
            Instantiate(deadPlayer01, respawnLocation01, Quaternion.identity);
            Instantiate(deadPlayer02, respawnLocation02, Quaternion.identity);
            Instantiate(deadPlayer03, respawnLocation03, Quaternion.identity);
            Instantiate(deadPlayer04, respawnLocation04, Quaternion.identity);
            PlayersPlaying.playersPlaying = 0;
        }
    }

    void playerPointScore()
    {
        player1Score.text = "Player 1 points: " + PointBehavior.player1Points;
        player2Score.text = "Player 2 points: " + PointBehavior.player2Points;
        player3Score.text = "Player 3 points: " + PointBehavior.player3Points;
        player4Score.text = "Player 4 points: " + PointBehavior.player4Points;
    }
}
