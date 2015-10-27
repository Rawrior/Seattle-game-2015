using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    private Animator Animator;
    public string Horizontal;
    public string Dash;
    public float YSpeed;
    private bool facingRight;
    private bool rollingRight;
    private bool rollingLeft;
    //public float currentX;
    //public float currentY;
    //public float previousX;
    //public float previousY;

	// Use this for initialization
	void Start ()
	{
	    Animator = gameObject.GetComponent<Animator>();
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
	    Dash = "LT0" + tag[gameObject.tag.Length - 1];

	    //currentX = transform.position.x;
	    //currentY = transform.position.y;
	    //previousX = transform.position.x;
	    //previousY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    YSpeed = GetComponentInParent<Rigidbody2D>().velocity.y;
        StateChange(Horizontal, YSpeed);
        FlipMethod(facingRight);

	    rollingRight = GetComponentInParent<PlayerMovement>().RollingRight;
	    rollingLeft = GetComponentInParent<PlayerMovement>().RollingLeft;
	}

    private void StateChange(string horizontal, float ySpeed)
    {
        if (/*Input.GetAxis(Dash) >= 0.1f*/ rollingLeft)
        {
            Animator.SetInteger("state", 5);
            facingRight = true;
        }
        else if (/*-Input.GetAxis(Dash) >= 0.1f*/ rollingRight)
        {
            Animator.SetInteger("state", 5);
            facingRight = false;
        }
        else if (Input.GetAxis(horizontal) < 0 && ySpeed == 0)
        {
            Animator.SetInteger("state", 1);
            facingRight = true;
        }
        else if (Input.GetAxis(horizontal) > 0 && ySpeed == 0)
        {
            Animator.SetInteger("state", 1);
            facingRight = false;
        }
        else if (ySpeed > 0.1f)
        {
            Animator.SetInteger("state", 2);
        }
        else if (ySpeed < -0.1f)
        {
            Animator.SetInteger("state", 3);
        }
        else
        {
            Animator.SetInteger("state", 0);
        }
    }

    private void FlipMethod(bool facingRight)
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
