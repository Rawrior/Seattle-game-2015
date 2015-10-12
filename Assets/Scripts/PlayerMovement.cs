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
    public string rollLeft;
    public string rollRight;
    public float rollTimer;
    public bool rolling;
    public bool rollingLeft;
    public bool rollingRight;
    public bool canWallJump;
    public int wallJumpHeight = 10;
    public int moveSpeed = 10;
    public bool haveIWallJumped = false;
    public float wallJumpTimer;
    public bool haveTouchedWall = false;
    public bool isStunned = false;
    public float stunTimer;
    public bool canStun;
    private int rollDistance = 20;

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
        rollLeft = "LT0" + tag[gameObject.tag.Length - 1];
        rollRight = "RT0" + tag[gameObject.tag.Length - 1];
        Debug.Log(rollLeft);
        Debug.Log(rollRight);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isStunned == true)
        {
            stunTimer += Time.deltaTime;
        }
        if (stunTimer >= 0.5f)
        {
            isStunned = false;
            stunTimer = 0;
        }
        if (haveTouchedWall == true)
        {
            wallJumpTimer += Time.deltaTime;
        }
        if (rolling == true)
        {
            rollTimer += Time.deltaTime;
        }
        if ( rolling == true && rollTimer >= 1)
        {
            rollTimer = 0;
            rolling = false;
        }
        if (airborne == true)
        {
            moveSpeed = 5;
            jumpDuration += Time.deltaTime;
        }
        if (airborne == false)
        {
            moveSpeed = 10;
        }
        Jump();
        movement(Horizontal);
        Roll(rollLeft, rollRight);
        wallJump();
        //Debug.Log(canStun);
	}

    void movement(string horizontal)
    {
        if (( rolling == false | rollTimer >= 0.3f) && isStunned == false)
        {
            transform.Translate(new Vector2(Input.GetAxis(horizontal), 0) * moveSpeed * Time.deltaTime);
        }
        
    }

    void Jump()
    {
        if ((player1Playing == true) && (Input.GetKey(KeyCode.Joystick1Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 10f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P1 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player2Playing == true) && (Input.GetKey(KeyCode.Joystick2Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P2 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player3Playing == true) && (Input.GetKey(KeyCode.Joystick3Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P3 jumping");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else if ((player4Playing == true) && (Input.GetKey(KeyCode.Joystick4Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 5f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
            Debug.Log("P4 jumping");
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
        if (other.CompareTag("Wall") && airborne == true)
        {
            haveTouchedWall = true;
            canWallJump = true;
        }
        if (other.CompareTag("Ground") && airborne == false)
        {
            haveTouchedWall = false;
            wallJumpTimer = 0;
            haveIWallJumped = false;
            canWallJump = false;
        }

    }

    void Roll(string rollDirectionLeft, string rollDirectionRight)
    {
        if (airborne == false && Input.GetAxis(rollDirectionLeft) >= 0.1f && rollTimer <= 0.1f && rolling == false && rollingRight == false)
        {
            rollingLeft = true;
            rolling = true;
        }
        if (airborne == false && Input.GetAxis(rollDirectionRight) >= 0.1f && rollTimer <= 0.1f && rolling == false && rollingLeft == false)
        {
            rollingRight = true;
            rolling = true;
        }
        if (rolling == true && rollingLeft == true)
        {
            transform.Translate(new Vector2(-rollDistance, 0) * Time.deltaTime);
            Debug.Log("rolling left");
        }
        if (rolling == true && rollingRight == true)
        {
            transform.Translate(new Vector2(rollDistance, 0)* Time.deltaTime);
            Debug.Log("rolling right");
        }
        if (rollTimer >= 0.2f)
        {
            rollingLeft = false;
            rollingRight = false;
        }
    }

    void wallJump()
    {
        if (airborne == true && canWallJump == true && player1Playing == true && Input.GetKeyDown(KeyCode.Joystick1Button4) && haveIWallJumped == false && wallJumpTimer <= 0.2f)
        {
            haveIWallJumped = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(-7, 15));
            Debug.Log("walljumping");
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);
        if (player1Playing == true && other.CompareTag("Player02") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        {
            Debug.Log(canStun);
            canStun = true;
        }
        if (player2Playing == true && other.CompareTag("Player01") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        {
            Debug.Log(canStun);
            canStun = true;
        }
        if (player3Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player04"))
        {
            Debug.Log(canStun);
            canStun = true;
        }
        if (player4Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player03"))
        {
            Debug.Log(canStun);
            canStun = true;
        }
        else
        {
            canStun = false;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick2Button0))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick3Button0))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick4Button0))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
    }
    
}
