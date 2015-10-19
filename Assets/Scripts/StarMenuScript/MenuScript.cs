using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public string vertical;
    public bool playSelected = true;
    public bool creditsSelected;
    public bool exitSelected;
    public bool players2Selected;
    public bool players3Selected;
    public bool players4Selected;
    public bool backSelected;
    public int currentLevel;
	// Use this for initialization
	void Start ()
    {
        vertical = "Vertical01";
	}
	
	// Update is called once per frame
	void Update ()
    {

        currentLevel = Application.loadedLevel;
        if (currentLevel == 0)
        {
            menuOne();  
        }
        else if (currentLevel == 2)
        {
            menuTwo();
        }

	}
    void menuOne()
    {
        if (playSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(2);
        }
        if (playSelected == true && Input.GetAxis(vertical) <= 0)
        {
            playSelected = false;
            creditsSelected = true;
        }
        else if (creditsSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(1);
        }
        else if (creditsSelected == true && Input.GetAxis(vertical) >= 0)
        {
            creditsSelected = false;
            playSelected = true;
        }
        else if (creditsSelected && Input.GetAxis(vertical) <= 0)
        {
            creditsSelected = false;
            exitSelected = true;
        }
        if (exitSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.Quit();
        }
    }

    void menuTwo()
    {
        if (currentLevel == 2 && players3Selected == false && players4Selected == false && backSelected == false)
        {
            players2Selected = true;
        }
        else
        {
            players2Selected = false;
        }
        if (players2Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(3);
        }
        if (players3Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(3);
        }
        if (players4Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(3);
        }
        if ((players2Selected == true && Input.GetAxis(vertical) <= 0) || (players4Selected == true && Input.GetAxis(vertical) >= 0))
        {
            players3Selected = true;
        }
        else if ((players3Selected == true && Input.GetAxis(vertical) <= 0) || (backSelected == true && Input.GetAxis(vertical) >= 0))
        {
            players4Selected = true;
        }
        if (players4Selected == true && Input.GetAxis(vertical) <= 0)
        {
            backSelected = true;
        }
        if (players3Selected == true && Input.GetAxis(vertical) >= 0)
        {
            players2Selected = true;
        }
        if (backSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(0);
        }

    }
}
