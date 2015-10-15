using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    private Animator Animator;
    public string Horizontal;
    public float YSpeed;
    //public float currentX;
    //public float currentY;
    //public float previousX;
    //public float previousY;

	// Use this for initialization
	void Start ()
	{
	    Animator = gameObject.GetComponent<Animator>();
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        
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
        //Debug.Log(Input.GetAxis(Horizontal));
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.y);
	}

    private void StateChange(string horizontal, float ySpeed)
    {
        if (Input.GetAxis(horizontal) < 0)
        {
            Animator.SetInteger("state", 1);
            transform.localScale = new Vector3(1,1,1);
        }
        else if (Input.GetAxis(horizontal) > 0)
        {
            Animator.SetInteger("state", 1);
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (Input.GetAxis(horizontal) == 0)
        {
            Animator.SetInteger("state", 0);
        }

        //currentX = transform.position.x;
        //currentY = transform.position.y;

        //if (currentX < previousX)
        //{
        //    //transform.Rotate(0,180,0);
        //    Animator.SetInteger("state", 1);
        //}
        //else if (currentX > previousX)
        //{
        //    Animator.SetInteger("state", 2);
        //}

        //previousX = currentX;
        //previousY = currentY;
    }
}
