using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject exitButton;
    public GameObject players2Button;
    public GameObject players3Button;
    public GameObject players4Button;
    public GameObject backButton;
    public string vertical;
    public bool playSelected = true;
    public bool creditsSelected;
    public bool exitSelected;
    public bool players2Selected = true;
    public bool players3Selected;
    public bool players4Selected;
    public bool backSelected;
    public int currentLevel;
    public float selectTimer;
	// Use this for initialization
	void Start ()
    {
        vertical = "Vertical01";
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayersPlaying.player1Points = 0;
        PlayersPlaying.player2Points = 0;
        PlayersPlaying.player3Points = 0;
        PlayersPlaying.player4Points = 0;
        selectTimer += Time.deltaTime;
        Debug.Log(Input.GetAxis(vertical));

        currentLevel = Application.loadedLevel;
        if (currentLevel == 0)
        {
            menuOne();  
        }
        else if (currentLevel == 2)
        {
            menuTwo();
        }
        else if (currentLevel == 1)
        {
            menuCredits();
        }
        selectedButtonController();

	}
    void menuOne()
    {
        players2Selected = false;
        if (playSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(2);
        }
        if (playSelected == true && Input.GetAxis(vertical) <= -1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            playSelected = false;
            creditsSelected = true;
        }
        else if (creditsSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(1);
        }
        else if (creditsSelected == true && Input.GetAxis(vertical) >= 1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            creditsSelected = false;
            playSelected = true;
        }
        else if (creditsSelected == true && Input.GetAxis(vertical) <= -1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            creditsSelected = false;
            exitSelected = true;
        }
        else if (exitSelected == true && Input.GetAxis(vertical) >= 1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            exitSelected = false;
            creditsSelected = true;
        }
        if (exitSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.Quit();
        }
    }

    void menuTwo()
    {
        playSelected = false;
        if (players3Selected == true || players4Selected == true || backSelected == true)
        {
            players2Selected = false;
        }
        else if (players3Selected == false && players4Selected == false && backSelected == false)
        {
            players2Selected = true;
        }
        if (players2Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PlayersPlaying.playersPlaying = 2;
            Application.LoadLevel(3);
        }
        if (players3Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PlayersPlaying.playersPlaying = 3;
            Application.LoadLevel(3);
        }
        if (players4Selected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PlayersPlaying.playersPlaying = 4;
            Application.LoadLevel(3);
        }
        if (players2Selected == true && Input.GetAxis(vertical) <= -1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            players2Selected = false;
            players3Selected = true;
        }
        if (players3Selected == true && Input.GetAxis(vertical) <= -1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            players3Selected = false;
            players4Selected = true;
        }
        if (players4Selected == true && Input.GetAxis(vertical) <= -1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            players4Selected = false;
            backSelected = true;
        }
        if (backSelected == true && Input.GetAxis(vertical) >= 1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            backSelected = false;
            players4Selected = true;
        }
        if (players4Selected == true && Input.GetAxis(vertical) >= 1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            players4Selected = false;
            players3Selected = true;
        }
        if (players3Selected == true && Input.GetAxis(vertical) >= 1 && selectTimer >= 0.15f)
        {
            selectTimer = 0;
            players3Selected = false;
            players2Selected = true;
        }
        if (backSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(0);
        }

    }

    void menuCredits()
    {
        playSelected = false;
        backSelected = true;
        if (backSelected == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel(0);
        }
    }

    void selectedButtonController()
    {
        if (playSelected == true && Application.loadedLevel == 0)
        {
            playButton.SetActive(true);
            
        }
        else if (playSelected == false && Application.loadedLevel == 0)
        {
            playButton.SetActive(false);
        }
        if (creditsSelected == true && Application.loadedLevel == 0)
        {
            creditsButton.SetActive(true);
        }
        else if (creditsSelected == false && Application.loadedLevel == 0)
        {
            creditsButton.SetActive(false);
        }
        if (exitSelected == true && Application.loadedLevel == 0)
        {
            exitButton.SetActive(true);
        }
        else if (exitSelected == false && Application.loadedLevel == 0)
        {
            exitButton.SetActive(false);
        }
        if ((backSelected == true) && (Application.loadedLevel == 1 || Application.loadedLevel == 2))
        {
            backButton.SetActive(true);
        }
        else if (backSelected == false && (Application.loadedLevel == 1 || Application.loadedLevel == 2))
        {
            backButton.SetActive(false);
        }
        if (players2Selected == true && Application.loadedLevel == 2)
        {
            players2Button.SetActive(true);
        }
        else if (players2Selected == false && Application.loadedLevel == 2)
        {
            players2Button.SetActive(false);
        }
        if (players3Selected == true && Application.loadedLevel == 2)
        {
            players3Button.SetActive(true);
        }
        else if (players3Selected == false && Application.loadedLevel == 2)
        {
            players3Button.SetActive(false);
        }
        if (players4Selected == true && Application.loadedLevel == 2)
        {
            players4Button.SetActive(true);
        }
        else if (players4Selected == false && Application.loadedLevel == 2)
        {
            players4Button.SetActive(false);
        }
    }
}
