using UnityEngine;
using System.Collections;

public class ArrowCountAnimController : MonoBehaviour
{
    private int Arrows;
    public Animator Animator;
    public GameObject Object;

	// Use this for initialization
	void Start ()
	{
	    Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Arrows = Object.GetComponent<ShootScript>().ArrowCount;
        Animator.SetInteger("state", Arrows);
	}
}
