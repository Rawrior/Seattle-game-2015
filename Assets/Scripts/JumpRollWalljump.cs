using UnityEngine;
using System.Collections;

public class JumpRollWalljump : MonoBehaviour
{
    public bool player1Playing = false;
    public bool player2Playing = false;
    public bool player3Playing = false;
    public bool player4Playing = false;
    public string playerPlaying;
    public float jumpCD;
    public bool canJump;

	// Use this for initialization
	void Start ()
    {
        playerPlaying = "P" + tag[gameObject.tag.Length - 1];
        whoIsPlaying();
        Debug.Log(playerPlaying);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Jump();
	}
    void Jump()
    {
        if ((player1Playing == true) && (Input.GetKey(KeyCode.Joystick1Button4) && canJump == true))
        {
            jumpCD += Time.deltaTime;
            Debug.Log("jumping");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50), ForceMode2D.Force);
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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
}
