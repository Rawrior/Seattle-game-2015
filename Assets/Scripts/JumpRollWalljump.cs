using UnityEngine;
using System.Collections;

public class JumpRollWalljump : MonoBehaviour
{
    public bool player1Playing = false;
    public bool player2Playing = false;
    public bool player3Playing = false;
    public bool player4Playing = false;
    public string playerPlaying;
    public float jumpDuration;
    public float jumpHight = 3.5f;
    public float jumpSpeed;

	// Use this for initialization
	void Start ()
    {
        playerPlaying = "P" + tag[gameObject.tag.Length - 1];
        whoIsPlaying();
        //Debug.Log(playerPlaying);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Jump();
	}
    void Jump()
    {
        if ((player1Playing == true) && (Input.GetKey(KeyCode.Joystick1Button4)) && jumpDuration <= 0.25f)
        {
            jumpSpeed = 5f;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            jumpDuration += Time.deltaTime;
            //Debug.Log("P1 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player2Playing == true) && (Input.GetKey(KeyCode.Joystick2Button4)) && jumpDuration <= 0.25f)
        {
            jumpSpeed = 5f;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            jumpDuration += Time.deltaTime;
            //Debug.Log("P2 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player3Playing == true) && (Input.GetKey(KeyCode.Joystick3Button4)) && jumpDuration <= 0.25f)
        {
            jumpSpeed = 5f;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            jumpDuration += Time.deltaTime;
            //Debug.Log("P3 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player4Playing == true) && (Input.GetKey(KeyCode.Joystick4Button4)) && jumpDuration <= 0.25f)
        {
            jumpSpeed = 5f;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            jumpDuration += Time.deltaTime;
            //Debug.Log(" P4jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else
        {
            jumpSpeed = 0;
        }
    }
    void whoIsPlaying()
    {
        if (playerPlaying == "P1")
        {
            player1Playing = true;
        }
        if (playerPlaying == "P2")
        {
            player2Playing = true;
        }
        if (playerPlaying == "P3")
	    {
            player3Playing = true;
	    }
        if (playerPlaying == "P4")
	    {
            player4Playing = true;
	    }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            jumpDuration = 0;
        }
    }
}
