using UnityEngine;
using System.Collections;

public class UpperAnimController : MonoBehaviour
{
    private Animator Animator;
    private int arrowCount;
    private string ShootButton;

	// Use this for initialization
	void Start ()
	{
	    Animator = gameObject.GetComponent<Animator>();
	    ShootButton = "Fire" + tag[gameObject.tag.Length - 1];
	}
	
	// Update is called once per frame
	void Update () 
    {
		arrowCount = GetComponentInParent<ShootScript>().ArrowCount;
        StateChange(ShootButton, arrowCount);

	}

    void StateChange(string shootButton, int arrows)
    {
        if (Input.GetButton(shootButton) && arrows > 0 && GetComponentInParent<PlayerMovement>().enableIframes == false)
        {
            Animator.SetInteger("state", 1);
        }
        else if ((Input.GetButton(shootButton) && arrows == 0) && GetComponentInParent<PlayerMovement>().enableIframes == false)
        {
            Animator.SetInteger("state", 2);
        }
        else
        {
            Animator.SetInteger("state", 0);
        }
    }
}
