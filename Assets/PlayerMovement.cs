using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public bool player1Playing = false;
    public bool player2Playing = false;
    public bool player3Playing = false;
    public bool player4Playing = false;
    public string playerPlaying;
    public float jumpDuration;
    public float jumpHight = 3.5f;
    public float jumpSpeed;
    public bool airborne;
    public float jumpTime;

    public string Horizontal;
	// Use this for initialization
	void Start ()
    {
        jumpTime = 0.25f;
        playerPlaying = "P" + tag[gameObject.tag.Length - 1];
        whoIsPlaying();
        Debug.Log(playerPlaying);
        Debug.Log(gameObject.tag);
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        Debug.Log(Horizontal);
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (airborne == true)
        {
            jumpDuration += Time.deltaTime;
        }
        Jump();
        movement(Horizontal);
	}

    void movement(string horizontal)
    {
        transform.Translate(new Vector2(Input.GetAxis(horizontal), 0) * 10 * Time.deltaTime);
    }
    void Jump()
    {
        if ((player1Playing == true) && (Input.GetKey(KeyCode.Joystick2Button4)) && jumpDuration <= jumpTime)
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P1 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player2Playing == true) && (Input.GetKey(KeyCode.Joystick3Button4)) && jumpDuration <= jumpTime)
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P2 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player3Playing == true) && (Input.GetKey(KeyCode.Joystick4Button4)) && jumpDuration <= jumpTime)
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P3 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player4Playing == true) && (Input.GetKey(KeyCode.Joystick5Button4)) && jumpDuration <= jumpTime)
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log(" P4 jumping");
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
            airborne = false;
            jumpDuration = 0;
        }
    }
}
