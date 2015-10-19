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
    private int rollDistance = 15;
    private RaycastHit2D hit;
    //private RaycastHit2D[] rays;

    public string Horizontal;
	// Use this for initialization
	void Start ()
    {
        jumpTime = 0.2f;
        playerPlaying = "P" + tag[gameObject.tag.Length - 1];
        whoIsPlaying();
        Debug.Log(playerPlaying);
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        rollLeft = "LT0" + tag[gameObject.tag.Length - 1];
        rollRight = "RT0" + tag[gameObject.tag.Length - 1];

        //rays[0] = Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.5f, 0), Vector2.down, 0.1f);
        //rays[1] = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, 0.1f);
        //rays[2] = Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.5f, 0), Vector2.down, 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
         //hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        Debug.DrawRay(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down * 0.3f);
        Debug.DrawRay(transform.position + new Vector3(0,-0.4f,0), Vector2.down * 0.3f);
        Debug.DrawRay(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down * 0.3f);

        //in update we have all our methods placed, and also a lot of different timers that start and stop individually and is controlled by Time.deltaTime
        if (isStunned == true)
        {
            stunTimer += Time.deltaTime;
        }
        if (stunTimer >= 2f)
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
            moveSpeed = 3;
            jumpDuration += Time.deltaTime;
        }
        if (airborne == false)
        {
            moveSpeed = 5;
        }
        Jump();
        movement(Horizontal);
        Roll(rollLeft, rollRight);
        wallJump();
        RaycastMethod();
	}

    void movement(string horizontal)
    {
        //checks if you're tolling or stunned, and if not, you can move horizontaly
        if (( rolling == false | rollTimer >= 0.3f) && isStunned == false)
        {
            transform.Translate(new Vector2(Input.GetAxis(horizontal), 0) * moveSpeed * Time.deltaTime);
        }   
    }

    void Jump()
    {
        //adds velocity to the current playing player to give it a jump.
        if ((airborne == false && player1Playing == true) && (Input.GetKeyDown(KeyCode.Joystick1Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 10f;
            //airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((airborne == false && player2Playing == true) && (Input.GetKeyDown(KeyCode.Joystick2Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 10f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((airborne == false && player3Playing == true) && (Input.GetKeyDown(KeyCode.Joystick3Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 10f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((airborne == false && player4Playing == true) && (Input.GetKeyDown(KeyCode.Joystick4Button4)) && jumpDuration <= jumpTime && (rolling == false | rollTimer >= 0.3f))
        {
            jumpSpeed = 10f;
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        else
        {
            jumpSpeed = 0;
        }
    }

    void whoIsPlaying()
    {
        //checking what player is currently playing.
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

    void RaycastMethod()
    {
        if (RaycastHit() && hit.collider.gameObject.CompareTag("Ground"))
        {
            Debug.Log(hit.collider.gameObject.tag);
            airborne = false;
            jumpDuration = 0;
        }
        else if (!RaycastHit())
        {
            airborne = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //checking if you're grounded and touching a wall for elighability to do a walljump
        if (other.CompareTag("Wall") && airborne == true)
        {
            haveTouchedWall = true;
            canWallJump = true;
        }
        //returns false peramiters so you will be unable to walljump when standing on the ground.
        if (other.CompareTag("Ground") && airborne == false)
        {
            haveTouchedWall = false;
            wallJumpTimer = 0;
            haveIWallJumped = false;
            canWallJump = false;
        }

        //Pick up arrows
        if (other.CompareTag("Arrow") && GetComponentInChildren<ShootScript>().ArrowCount < 3)
        {
            GetComponentInChildren<ShootScript>().ArrowCount++;
            Destroy(other.gameObject);
        }
    }

    void Roll(string rollDirectionLeft, string rollDirectionRight)
    {
        //in here we are making a rollfunction when you click the right and left trigger on an xbox 360 controller.
        //first we check if you are allowed to perform a roll, turning false if you're already rolling,  are in the air, or have rolled within the last second.
        if (airborne == false && Input.GetAxis(rollDirectionLeft) >= 0.1f && rollTimer <= 0.05f && rolling == false && rollingRight == false)
        {
            rollingLeft = true;
            rolling = true;
        }
        if (airborne == false && Input.GetAxis(rollDirectionLeft) * -1 >= 0.1f && rollTimer <= 0.05f && rolling == false && rollingLeft == false)
        {
            rollingRight = true;
            rolling = true;
        }
        //iif you then were able to roll, this code will roll you in the direction you pressed the triger.
        if (rolling == true && rollingLeft == true)
        {
            transform.Translate(new Vector2(-rollDistance, 0) * Time.deltaTime);
        }
        if (rolling == true && rollingRight == true)
        {
            transform.Translate(new Vector2(rollDistance, 0)* Time.deltaTime);
        }
        //this prevents you from holding the button down to keep rolling. and also determins the length of your roll.
        if (rollTimer >= 0.1f)
        {
            rollingLeft = false;
            rollingRight = false;
        }
    }

    void wallJump()
    {
        //If you've touched the wall you have 0.2 seconds to perfom a walljump, increasing you velocity to shoot you upwards and towards the other sida than the wall.
        if (airborne == true && canWallJump == true && player1Playing == true && Input.GetKeyDown(KeyCode.Joystick1Button4) && haveIWallJumped == false && wallJumpTimer <= 0.2f)
        {
            haveIWallJumped = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 10));
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other);
        //checking if you're touching the correct player and returning canStun as true so you can stun the other player.
        //Debug.Log(other);
        if (player1Playing == true && other.CompareTag("Player02") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        {
            //Debug.Log(canStun);
            canStun = true;
        }
        if (player2Playing == true && other.CompareTag("Player01") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        {
            //Debug.Log(canStun);
            canStun = true;
        }
        if (player3Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player04"))
        {
            //Debug.Log(canStun);
            canStun = true;
        }
        if (player4Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player03"))
        {
            //Debug.Log(canStun);
            canStun = true;
        }
        else
        {
            canStun = false;
        }
        //stunning the player by pressing the "A" button
        if (canStun == true && Input.GetKey(KeyCode.Joystick1Button0) && !other.CompareTag("Player01"))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick2Button0) && !other.CompareTag("Player02"))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick3Button0) && !other.CompareTag("Player03"))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
        if (canStun == true && Input.GetKey(KeyCode.Joystick4Button0) && !other.CompareTag("Player04"))
        {
            Debug.Log("stunning");
            other.GetComponent<PlayerMovement>().isStunned = true;
        }
    }

    public bool RaycastHit()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down, 0.3f) /*rays[0]*/)
        {
            //Debug.Log("Hit");
            //hit = rays[0];
            hit = Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down, 0.3f);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f))
        {
            //Debug.Log("Hit");
            hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down, 0.3f))
        {
            //Debug.Log("Hit");
            hit = Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down, 0.3f);
            return true;
        }
        else
        {
            return false;
        }
    }
}
