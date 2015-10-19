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
        Debug.Log(ShootButton);
	}
	
	// Update is called once per frame
	void Update () 
    {
		arrowCount = GetComponentInParent<ShootScript>().ArrowCount;
        StateChange(ShootButton, arrowCount);

        Debug.Log(Animator.GetInteger("state"));
	}

    void StateChange(string shootButton, int arrows)
    {
        if (Input.GetButton(shootButton) && arrows > 0)
        {
            Animator.SetInteger("state", 1);
        }
        else if (Input.GetButton(shootButton) && arrows == 0)
        {
            Animator.SetInteger("state", 2);
        }
        else
        {
            Animator.SetInteger("state", 0);
        }
    }
}
