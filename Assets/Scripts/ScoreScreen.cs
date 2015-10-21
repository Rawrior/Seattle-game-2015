using UnityEngine;
using System.Collections;

public class ScoreScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
	}
}
