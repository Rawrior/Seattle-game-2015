using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private bool Player1Playing;
    private bool Player2Playing;
    private bool Player3Playing;
    private bool Player4Playing;
    private string PlayerPlaying;
    public float jumpDuration;
    public float jumpHight = 3.5f;
    public float jumpSpeed;
    public bool Airborne;
    public float jumpTime;
    public string RollLeft;
    //public string RollRight;
    public float RollTimer;
    public bool RollCooldown;
    public bool RollingLeft;
    public bool RollingRight;
    public bool canWallJump;
    public int wallJumpHeight = 10;
    public int moveSpeed = 10;
    public bool haveIWallJumped = false;
    public float wallJumpTimer;
    public bool haveTouchedWall = false;
    //public bool isStunned = false;
    //public float stunTimer;
    //public bool canStun;
    private int rollDistance = 15;
    private RaycastHit2D JumpHit;
    private RaycastHit2D MoveHit;
    private RaycastHit2D RollHit;
    public LayerMask LayerMask;
    //private RaycastHit2D[] rays;
    public float spawnCampProtection;
    public bool enableIframes;

    public string Horizontal;
	// Use this for initialization
	void Start ()
    {
        if (gameObject.tag == "Player0" + tag[gameObject.tag.Length - 1])
        {
            PlayerPlaying = "Player0" + tag[gameObject.tag.Length - 1];
        }

        if (PlayerPlaying == "Player01")
        {
            Player1Playing = true;
        }
        if (PlayerPlaying == "Player02")
        {
            Player2Playing = true;
        }
        if (PlayerPlaying == "Player03")
        {
            Player3Playing = true;
        }
        if (PlayerPlaying == "Player04")
        {
            Player4Playing = true;
        }
        Debug.Log(PlayerPlaying);

        jumpTime = 0.2f;
        PlayerPlaying = "P" + tag[gameObject.tag.Length - 1];
        whoIsPlaying();
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        RollLeft = "LT0" + tag[gameObject.tag.Length - 1];
        //RollRight = "RT0" + tag[gameObject.tag.Length - 1];

        LayerMask = ~LayerMask;
    }
	
	// Update is called once per frame
	void Update ()
    {
        spawnCampProtection += Time.deltaTime;
        if (spawnCampProtection <= 2f)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            enableIframes = true;
        }
        else if (spawnCampProtection >= 2f)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            enableIframes = false;
        }
         //JumpHit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        //Debug.DrawRay(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down * 0.3f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(0,-0.4f,0), Vector2.down * 0.3f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down * 0.3f, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.right * 0.3f, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.left * 0.3f, Color.blue);
        //Debug.Log(JumpRaycastHit());

        //in update we have all our methods placed, and also a lot of different timers that start and stop individually and is controlled by Time.deltaTime

        //if (isStunned == true)
        //{
        //    stunTimer += Time.deltaTime;
        //}
        //if (stunTimer >= 2f)
        //{
        //    isStunned = false;
        //    stunTimer = 0;
        //}
        //if (haveTouchedWall)

        //if (isStunned == true)
        //{
        //    stunTimer += Time.deltaTime;
        //}
        //if (stunTimer >= 2f)
        //{
        //    isStunned = false;
        //    stunTimer = 0;
        //}
        if (haveTouchedWall == true)
        {
            wallJumpTimer += Time.deltaTime;
        }
        if (RollCooldown)
        {
            RollTimer += Time.deltaTime;
        }
        if (RollTimer >= 1)
        {
            RollTimer = 0;
            RollCooldown = false;
        }
        if (Airborne == true)
        {
            moveSpeed = 3;
            jumpDuration += Time.deltaTime;
        }
        if (Airborne == false)
        {
            moveSpeed = 5;
        }
        Jump();
        movement(Horizontal);
        Roll(RollLeft);
        //RollMethod(RollLeft, RollRight);
        wallJump();
        RaycastMethod();
        IFrames();
	}

    void movement(string horizontal)
    {
        //checks if you're tolling or stunned, and if not, you can move horizontaly
        if ((RollCooldown == false || RollTimer >= 0.3f) && MoveRaycastHit())
        {
            Debug.Log("Can move");
            transform.Translate(new Vector2(Input.GetAxis(horizontal), 0) * moveSpeed * Time.deltaTime);
        }   
    }

    void Jump()
    {
        //adds velocity to the current playing player to give it a jump.
        if ((Airborne == false && Player1Playing == true) && (Input.GetKeyDown(KeyCode.Joystick1Button4)) && jumpDuration <= jumpTime && (RollCooldown == false | RollTimer >= 0.3f))
        {
            jumpSpeed = 12f;
            Airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((Airborne == false && Player2Playing == true) && (Input.GetKeyDown(KeyCode.Joystick2Button4)) && jumpDuration <= jumpTime && (RollCooldown == false | RollTimer >= 0.3f))
        {
            jumpSpeed = 12f;
            Airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((Airborne == false && Player3Playing == true) && (Input.GetKeyDown(KeyCode.Joystick3Button4)) && jumpDuration <= jumpTime && (RollCooldown == false | RollTimer >= 0.3f))
        {
            jumpSpeed = 12f;
            Airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
        if ((Airborne == false && Player4Playing == true) && (Input.GetKeyDown(KeyCode.Joystick4Button4)) && jumpDuration <= jumpTime && (RollCooldown == false | RollTimer >= 0.3f))
        {
            jumpSpeed = 12f;
            Airborne = true;
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
        if (PlayerPlaying == "P1")
        {
            Player1Playing = true;
        }
        if (PlayerPlaying == "P2")
        {
            Player2Playing = true;
        }
        if (PlayerPlaying == "P3")
        {
            Player3Playing = true;
        }
        if (PlayerPlaying == "P4")
        {
            Player4Playing = true;
        }
    }

    void RaycastMethod()
    {
        if (JumpRaycastHit()/* && JumpHit.collider.gameObject.tag != "Arrow"*/)
        {
            Airborne = false;
            jumpDuration = 0;
        }
        else if (!JumpRaycastHit())
        {
            Airborne = true;
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        //checking if you're grounded and touching a wall for elighability to do a walljump
        if (other.CompareTag("Wall") && Airborne == true)
        {
            haveTouchedWall = true;
            canWallJump = true;
        }
        //returns false peramiters so you will be unable to walljump when standing on the ground.
        if (other.CompareTag("Ground") && Airborne == false)
        {
            haveTouchedWall = false;
            wallJumpTimer = 0;
            haveIWallJumped = false;
            canWallJump = false;
        }

        //Pick up arrows
        if (other.CompareTag("Arrow") && GetComponentInChildren<ShootScript>().ArrowCount < 3 && other.GetComponent<ArrowBehavior>().UsedArrow == true)
        {
            Debug.Log("Picked up an arrow");
            GetComponentInChildren<ShootScript>().ArrowCount++;
            Destroy(other.gameObject);
        }
    }

    void RollMethod(string rollLeft, string rollRight)
    {
        //if (!Airborne && Input.GetAxis(rollLeft) >= 0.1f && RollTimer <= 0.05f && )
    }

    void Roll(string rollDirectionLeft)
    {
        //in here we are making a rollfunction when you click the right and left trigger on an xbox 360 controller.
        //first we check if you are allowed to perform a roll, turning false if you're already RollCooldown,  are in the air, or have rolled within the last second.
        if (!Airborne && Input.GetAxis(rollDirectionLeft) >= 0.1f && !RollCooldown)
        {
            RollingLeft = true;
            RollCooldown = true;
        }
        
        if (!Airborne && -Input.GetAxis(rollDirectionLeft) >= 0.1f && !RollCooldown)
        {
            RollingRight = true;
            RollCooldown = true;
        }

        //if you then were able to roll, this code will roll you in the direction you pressed the triger.
        if (RollingLeft && RollRaycastHit())
        {
            transform.Translate(new Vector2(-rollDistance, 0) * Time.deltaTime);
        }
        
        if (RollingRight && RollRaycastHit())
        {
            transform.Translate(new Vector2(rollDistance, 0) * Time.deltaTime);
        }
        
        //this prevents you from holding the button down to keep RollCooldown. and also determins the length of your roll.
        if (RollTimer >= 0.1f)
        {
            RollingLeft = false;
            RollingRight = false;
        }
    }

    void wallJump()
    {
        //If you've touched the wall you have 0.2 seconds to perfom a walljump, increasing you velocity to shoot you upwards and towards the other sida than the wall.
        if (Airborne == true && canWallJump == true && Player1Playing == true && Input.GetKeyDown(KeyCode.Joystick1Button4) && haveIWallJumped == false && wallJumpTimer <= 0.2f)
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

        //if (player1Playing == true && other.CompareTag("Player02") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        //{
        //    //Debug.Log(canStun);
        //    canStun = true;
        //}
        //if (player2Playing == true && other.CompareTag("Player01") || other.CompareTag("Player03") || other.CompareTag("Player04"))
        //{
        //    //Debug.Log(canStun);
        //    canStun = true;
        //}
        //if (player3Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player04"))
        //{
        //    //Debug.Log(canStun);
        //    canStun = true;
        //}
        //if (player4Playing == true && other.CompareTag("Player01") || other.CompareTag("Player02") || other.CompareTag("Player03"))
        //{
        //    //Debug.Log(canStun);
        //    canStun = true;
        //}
        //else
        //{
        //    canStun = false;
        //}
        ////stunning the player by pressing the "A" button
        //if (canStun == true && Input.GetKey(KeyCode.Joystick1Button0) && !other.CompareTag("Player01"))
        //{
        //    Debug.Log("stunning");
        //    other.GetComponent<PlayerMovement>().isStunned = true;
        //}
        //if (canStun == true && Input.GetKey(KeyCode.Joystick2Button0) && !other.CompareTag("Player02"))
        //{
        //    Debug.Log("stunning");
        //    other.GetComponent<PlayerMovement>().isStunned = true;
        //}
        //if (canStun == true && Input.GetKey(KeyCode.Joystick3Button0) && !other.CompareTag("Player03"))
        //{
        //    Debug.Log("stunning");
        //    other.GetComponent<PlayerMovement>().isStunned = true;
        //}
        //if (canStun == true && Input.GetKey(KeyCode.Joystick4Button0) && !other.CompareTag("Player04"))
        //{
        //    Debug.Log("stunning");
        //    other.GetComponent<PlayerMovement>().isStunned = true;
        //}
    }

    public bool JumpRaycastHit()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask) /*rays[0]*/)
        {
            JumpHit = Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f, LayerMask))
        {
            JumpHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask))
        {
            JumpHit = Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MoveRaycastHit()
    {
        if (Input.GetAxis(Horizontal) > 0 && Physics2D.Raycast(transform.position, Vector2.right, 0.25f, LayerMask))
        {
            //Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.right * 0.25f, Color.blue);
            MoveHit = Physics2D.Raycast(transform.position, Vector2.right, 0.25f, LayerMask);
            return false;
        }
        else if (Input.GetAxis(Horizontal) < 0 && Physics2D.Raycast(transform.position, Vector2.left, 0.25f, LayerMask))
        { 
            //Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.left * 0.25f, Color.blue);
            MoveHit = Physics2D.Raycast(transform.position, Vector2.left, 0.25f, LayerMask);
            return false;
        }
        else
        {
            return true;
        }
    }

    void IFrames()
    {
        if (enableIframes == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (enableIframes == false)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public bool RollRaycastHit()
    {
        if (RollingRight && Physics2D.Raycast(transform.position, Vector2.right, 04f, LayerMask))
        {
            RollHit = Physics2D.Raycast(transform.position, Vector2.right, 0.4f, LayerMask);
            return false;
        }
        else if (RollingLeft && Physics2D.Raycast(transform.position, Vector2.left, 0.4f, LayerMask))
        { 
            RollHit = Physics2D.Raycast(transform.position, Vector2.left, 0.4f, LayerMask);
            return false;
        }
        else
        {
            return true;
        }
    }
}
