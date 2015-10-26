using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public Text text;
    public Text text2;
    public bool Pause;
    //public float resumeTimer;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(Time.timeScale);
        PauseMethod();
	}

    void PauseMethod()
    {
        if (Pause == false && Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("pausing");
            Pause = true;
            //resumeTimer = +Time.deltaTime;
        }
        else if (Pause == true && Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("unpausing");
            Pause = false;
        }
        if (Pause == true)
        {
            text2.text = "GAME HAS BEEN PAUSED";
            text.text = "Press START to unpause or BACK to go to menu";
            Time.timeScale = 0;
        }
        else if (Pause == false)
        {
            text2.text = "";
            text.text = "";
            Time.timeScale = 1;
        }
        if (Pause == true && Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Application.LoadLevel(0);
        }
    }
}
