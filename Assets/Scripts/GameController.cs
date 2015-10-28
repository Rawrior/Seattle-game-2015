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
        //Quits the game if escape is pressed.
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Application.Quit();
	    }
	}
}
