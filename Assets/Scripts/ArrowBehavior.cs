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

    //Used to make sure the player won't die when trying to pick up arrows.
    private bool CanKill;

    //The list of tags the arrow should NOT kill.
    //Just shortens down the if-statement condition line.
    public string[] IgnoreTags;

    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start () 
    {
        //Debug.Log(GetComponent<Rigidbody2D>().isKinematic);
        //Debug.Log("Runnin Start"); 

        //Adds the ShootStrength to the arrow when it's spawned
        GetComponent<Rigidbody2D>().AddForce(-transform.right*ShootStrength,ForceMode2D.Impulse);

        //Makes the arrow a cold-blooded murderer
	    CanKill = true;
    }

    void Update()
    {
        FlyRotation();
    }

    private void FlyRotation()
    {
        if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //When a collider enters the trigger (or vice-versa. Technically not, but y'know)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the collider has any of the tags in the ignore list.
        //This will be Ground, Wall and the player that shot the arrow.
        //Also check if the arrow can kill. Default is true.
        if (!IgnoreTags.Contains(other.tag) && CanKill)
        {
            //Debug.Log("Hit " + other.tag);
            //If the collider is then another player, destroy them. DESTROOOOY THEM!
            Destroy(other.gameObject);
        }

        //Else, check if the collider is the ground or a wall.
        else if (IgnoreTags.Contains(other.tag) && !other.CompareTag(IgnoreTags[0]))
        {
            ////Set the arrow's velocity to 0
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            
            //Make the object kinematic so it stays where it hits
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            //Make it not kill players that want to pick it up.
            //They need it to start killing again...
            CanKill = false;
        }

        ////Check if the collider is not the ground and not the wall.
        //if (!CanKill && !other.CompareTag(IgnoreTags[1]))
        //{
        //    //If it's neither, then check for the collider's (player's) first child object (the bow arm)
        //    //and see if it has the ShootScript component, and if the player's arrows is under 3.
        //    if (other.transform.GetChild(0).GetComponent<ShootScript>().ArrowCount < 3)
        //    {
        //        //If it is, add 1 to the count.
        //        other.transform.GetChild(0).GetComponent<ShootScript>().ArrowCount++;

        //        //Then destroy self.
        //        Destroy(gameObject);
        //    }
        //}
    }
}
