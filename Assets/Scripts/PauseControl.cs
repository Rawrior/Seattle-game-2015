using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //The text to indicate the game is paused.
    public Text text;
    public Text text2;

    //Bool to keep in check if the game is paused or not.
    private bool pause;
	
	// Update is called once per frame
	void Update ()
    {
        //Calls the method for pausing.
        PauseMethod();
	}

    //Method for handling the pausing itself.
    void PauseMethod()
    {
        //Check if player 1 presses the start button and the game is unpaused.
        if (!pause && Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            //Sets the game-state to paused.
            pause = true;
        }
        //check if the game is paused and player 1 presses the start button.
        else if (pause && Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            //Sets the game-state to be unpaused.
            pause = false;
        }

        //Checks if the game is paused.
        if (pause)
        {
            //If it is, display some text on screen.
            text2.text = "GAME HAS BEEN PAUSED";
            text.text = "Press START to unpause or BACK to go to menu";

            //And set the timescale to 0, effectively stopping everything.
            Time.timeScale = 0;
        }
        //Check if the game is unpaused.
        else if (!pause)
        {
            //Set the pause-text to nothing.
            text2.text = "";
            text.text = "";

            //Force the time-scale to run normal speed.
            Time.timeScale = 1;
        }

        //If the game is paused and player 1 presses back...
        if (pause && Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            //... Then load the main menu.
            Application.LoadLevel(0);
        }
    }
}
