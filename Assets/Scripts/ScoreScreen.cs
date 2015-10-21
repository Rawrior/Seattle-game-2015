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
            Debug.Log(PointBehavior.player1Points);
            Debug.Log(PointBehavior.player2Points);
            Debug.Log(PointBehavior.player3Points);
            Debug.Log(PointBehavior.player4Points);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Application.LoadLevel(0);
        }
	}
}
