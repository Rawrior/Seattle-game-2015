using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Reloads the level if R is pressed.
	    if (Input.GetKeyDown(KeyCode.R))
	    {
	        Application.LoadLevel("Scene01");
	    }

        //Quits the game if escape is pressed.
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Application.Quit();
	    }
	}
}
