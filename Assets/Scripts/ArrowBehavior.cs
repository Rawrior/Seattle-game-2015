using UnityEngine;
using System.Collections;
using System.Linq;

public class ArrowBehavior : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //Defines the arrowspeed
    public float ShootStrength;
    private bool CanKill;
    public string[] IgnoreTags;

    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start () 
    {
        Debug.Log(GetComponent<Rigidbody2D>().isKinematic);
        //Debug.Log("Runnin Start"); 

        //Adds the ShootStrength to the arrow when it's spawned
        GetComponent<Rigidbody2D>().AddForce(transform.right*ShootStrength,ForceMode2D.Impulse);

        //Makes the arrow a cold-blooded murderer
	    CanKill = true;
    }

    //void Update()
    //{
    //    Debug.Log(CanKill);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IgnoreTags.Contains(other.tag) && CanKill)
        {
            Debug.Log("Hit " + other.tag);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            CanKill = false;
        }

        if (!CanKill && !other.CompareTag("Ground") && !other.CompareTag("Wall") /*other.GetComponent<ShootScript>().ArrowCount < 3)*/)
        {
            if (other.transform.GetChild(0).GetComponent<ShootScript>().ArrowCount < 3)
            {
                other.transform.GetChild(0).GetComponent<ShootScript>().ArrowCount++;
                Destroy(gameObject);
            }
        }
    }
}
