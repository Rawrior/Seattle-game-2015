using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //Defines the arrowspeed
    public float ShootStrength;

    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start () 
    {
        //Debug.Log("Runnin Start"); 

        //Adds the ShootStrength to the arrow when it's spawned
        GetComponent<Rigidbody2D>().AddForce(transform.right*ShootStrength,ForceMode2D.Impulse);
	}
}
