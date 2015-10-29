using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //Variables for handling jumping.
    private float jumpDuration;
    private float jumpSpeed;
    private bool airborne;
    private float jumpTime;
    private float wallJumpDirection;

    //Variables for handling rolling.
    private float rollTimer;
    private bool rollCooldown;
    public bool RollingLeft;
    public bool RollingRight;
    public bool canWallJump;
    public int wallJumpHeight = 10;
    public int moveSpeed = 10;
    public bool HasWallJumped;
    public float wallJumpTimer;
    public bool haveTouchedWall;
    //public bool isStunned = false;
    //public float stunTimer;
    //public bool canStun;
    private int rollDistance = 15;
    //private RaycastHit2D JumpHit;
    //private RaycastHit2D MoveHit;
    //private RaycastHit2D RollHit;
    public LayerMask LayerMask;
    //private RaycastHit2D[] rays;
    public float spawnCampProtection;
    public bool enableIframes;

    public string Horizontal;
    public string Jump;
    public string Jump2;
    public string RollLeft;
	
    // Use this for initialization
	void Start ()
    {
        jumpTime = 0.2f;
	    jumpSpeed = 12f;

        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        RollLeft = "LT0" + tag[gameObject.tag.Length - 1];
	    Jump = "Jump0" + tag[gameObject.tag.Length - 1];
        Jump2 = "Jump0" + tag[gameObject.tag.Length - 1];
        //RollRight = "RT0" + tag[gameObject.tag.Length - 1];
        LayerMask = ~LayerMask;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (enableIframes == true)
        {
            GetComponentInChildren<ShootScript>().ChargeTime = 0;
        }

         //JumpHit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        //Debug.DrawRay(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down * 0.3f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(0,-0.4f,0), Vector2.down * 0.3f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down * 0.3f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.right * 0.25f, Color.blue);
        //Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.left * 0.25f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.right * 0.3f, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 0.3f, Color.red);
        //Debug.Log(JumpRaycastHit());

        //in update we have all our methods placed, and also a lot of different timers that start and stop individually and is controlled by Time.deltaTime
        if (haveTouchedWall == true)
        {
            wallJumpTimer += Time.deltaTime;
        }
        if (rollCooldown)
        {
            rollTimer += Time.deltaTime;
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

        
        movement(Horizontal);
        Roll(RollLeft);
        //RollMethod(RollLeft, RollRight);
        wallJump();
        JumpMethod(Jump, Jump2);
        RaycastMethod();
        IFrames();

	    if (spawnCampProtection <= 2.1f)
	    {
	        SpawnCampMethod();   
	    }

        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector3.left*0.3f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.left*0.3f);
        Debug.DrawRay(transform.position + new Vector3(-0.3f, 0.5f, 0), Vector3.down*1);

        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector3.right * 0.3f);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.right * 0.3f);
        Debug.DrawRay(transform.position + new Vector3(0.3f, 0.5f, 0), Vector3.down * 1);

	}

    void movement(string horizontal)
    {
        //checks if you're tolling or stunned, and if not, you can move horizontaly
        if ((rollCooldown == false || rollTimer >= 0.15f) && MoveRaycastHit())
        {
            transform.Translate(new Vector2(Input.GetAxis(horizontal), 0) * moveSpeed * Time.deltaTime);
        }   
    }

    void JumpMethod(string JumpButton, string JumpButton2)
    {
        //adds velocity to the current playing player to give it a jump.
        if (airborne == false && (Input.GetButtonDown(JumpButton) || Input.GetButtonDown(JumpButton2)) && jumpDuration <= jumpTime)
        {
            Debug.Log("Jumping");
            airborne = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(0, jumpSpeed));
        }
    }

    void RaycastMethod()
    {
        if (JumpRaycastHit()/* && JumpHit.collider.gameObject.tag != "Arrow"*/)
        {
            airborne = false;
            jumpDuration = 0;
        }
        else if (!JumpRaycastHit())
        {
            airborne = true;
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        //checking if you're grounded and touching a wall for elighability to do a walljump
        //if (other.CompareTag("Wall") && Airborne == true)
        //{
        //    haveTouchedWall = true;
        //    canWallJump = true;
        //}
        //returns false peramiters so you will be unable to walljump when standing on the ground.
        if (other.CompareTag("Ground") && airborne == false)
        {
            haveTouchedWall = false;
            wallJumpTimer = 0;
            HasWallJumped = false;
            canWallJump = false;
        }

        //Pick up arrows
        if (other.CompareTag("Arrow") && GetComponentInChildren<ShootScript>().ArrowCount < 3 && !other.GetComponent<ArrowBehavior>().CanKill)
        {
            Debug.Log("Picked up an arrow");
            GetComponentInChildren<ShootScript>().ArrowCount++;
            Destroy(other.gameObject);
        }
    }


    void Roll(string rollDirectionLeft)
    {
        //in here we are making a rollfunction when you click the right and left trigger on an xbox 360 controller.
        //first we check if you are allowed to perform a roll, turning false if you're already RollCooldown,  are in the air, or have rolled within the last second.
        if (Input.GetAxis(rollDirectionLeft) >= 0.1f && !rollCooldown)
        {
            if (spawnCampProtection >= 2)
            {
                enableIframes = true;

            }
            RollingLeft = true;
            rollCooldown = true;
        }
        
        if (-Input.GetAxis(rollDirectionLeft) >= 0.1f && !rollCooldown)
        {
            if (spawnCampProtection >= 2)
            {
                enableIframes = true;

            }
            RollingRight = true;
            rollCooldown = true;
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
        if (rollTimer >= 0.15f)
        {
            if (spawnCampProtection >= 2)
            {
                enableIframes = false;

            }
            RollingLeft = false;
            RollingRight = false;
        }
        if (rollTimer >= 1f)
        {
            rollTimer = 0;
            rollCooldown = false;
        }
    }

    void wallJump()
    {
        //If you've touched the wall you have 0.2 seconds to perfom a walljump, increasing you velocity to shoot you upwards and towards the other sida than the wall.
        if (airborne && WallRaycastHit() && Input.GetButtonDown(Jump) && !HasWallJumped)
        {
            Debug.Log("Walljumping");
            HasWallJumped = true;
            GetComponent<Rigidbody2D>().velocity = (new Vector2(wallJumpDirection, 12));
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(wallJumpDirection,0), ForceMode2D.Impulse);
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

    private void SpawnCampMethod()
    {
        spawnCampProtection += Time.deltaTime;
        if (spawnCampProtection <= 2f)
        {
            
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            enableIframes = true;
            //Debug.Log("Iframes are " + enableIframes);
        }
        else if (spawnCampProtection >= 2f)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            enableIframes = false;
        }
    }

    public bool JumpRaycastHit()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(0.18f, -0.4f, 0), Vector2.down, 0.3f, LayerMask))
        {
            //JumpHit = Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f, LayerMask))
        {
            //JumpHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(-0.18f, -0.4f, 0), Vector2.down, 0.3f, LayerMask))
        {
            //JumpHit = Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.4f, 0), Vector2.down, 0.3f, LayerMask);
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
            //MoveHit = Physics2D.Raycast(transform.position, Vector2.right, 0.25f, LayerMask);
            return false;
        }
        else if (Input.GetAxis(Horizontal) < 0 && Physics2D.Raycast(transform.position, Vector2.left, 0.25f, LayerMask))
        { 
            //Debug.DrawRay(transform.position + new Vector3(0, -0.1f, 0), Vector2.left * 0.25f, Color.blue);
            //MoveHit = Physics2D.Raycast(transform.position, Vector2.left, 0.25f, LayerMask);
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool RollRaycastHit()
    {
        if (RollingRight && Physics2D.Raycast(transform.position, Vector2.right, 0.4f, LayerMask))
        {
            //RollHit = Physics2D.Raycast(transform.position, Vector2.right, 0.4f, LayerMask);
            return false;
        }
        else if (RollingLeft && Physics2D.Raycast(transform.position, Vector2.left, 0.4f, LayerMask))
        { 
            //RollHit = Physics2D.Raycast(transform.position, Vector2.left, 0.4f, LayerMask);
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool WallRaycastHit()
    {
        if (airborne)
        {
            
            if (Physics2D.BoxCast(transform.position, new Vector2(0.3f, 1), 0, Vector2.right,0.3f,LayerMask))
            {
                wallJumpTimer += Time.deltaTime;
                wallJumpDirection = -5;
                return true;
            }
            else if (Physics2D.BoxCast(transform.position, new Vector2(0.3f, 1), 0, Vector2.left, 0.3f, LayerMask))
            {

                wallJumpTimer += Time.deltaTime;
                wallJumpDirection = 5;
                return true;
            }
            else
            {
                wallJumpTimer = 0;
                return false;
            }
        }
        else
        {
            return false;
        }

        //if (wallJumpTimer <= 0.2f)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }
}
