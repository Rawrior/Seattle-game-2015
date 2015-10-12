using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //Defines the arrowspeed
    public float ShootStrength;
    private bool CanKill;

    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start () 
    {
        //Debug.Log("Runnin Start"); 

        //Adds the ShootStrength to the arrow when it's spawned
        GetComponent<Rigidbody2D>().AddForce(transform.right*ShootStrength,ForceMode2D.Impulse);
	    CanKill = true;
    }

    void Update()
    {
        Debug.Log(CanKill);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground") && !other.CompareTag("Wall") && CanKill)
        {
            Debug.Log("Hit " + other.tag);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().isKinematic = true;
            CanKill = false;
        }
    }
}
