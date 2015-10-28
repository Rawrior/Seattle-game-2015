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
    public bool CanKill;

    //The list of tags the arrow should NOT kill.
    //Just shortens down the if-statement condition line.
    public string[] IgnoreTags;

    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start () 
    {
        //Adds the ShootStrength to the arrow when it's spawned
        GetComponent<Rigidbody2D>().AddForce(-transform.right*ShootStrength,ForceMode2D.Impulse);

        //Makes the arrow a cold-blooded murderer
	    CanKill = true;
    }

    void Update()
    {
        //Calls the method for rotating the arrow mid-air. Makes it look more realistic as it flies.
        FlyRotation();
    }

    //Method for rotating the arrow midair.
    private void FlyRotation()
    {
        //Check if the arrow has a velocity. Don't want it rotating when it's stuck in the wall.
        if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            //Declare the velocity of the arrow. Just shortens down the next line.
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

            //Declare and calculate the angle. 
            //Take the tan^-1 of the velocity in x- and y-axises, then translate to degrees.
            float angle = Mathf.Atan2(velocity.y, velocity.x)*Mathf.Rad2Deg;

            //Updates the arrow's rotation to be the calculated angle on the 3D-space forward axis.
            transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        }
    }

    //When a collider enters the trigger (or vice-versa. Technically not, but y'know)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (!IgnoreTags.Skip(1).Contains(other.tag))
        //{
            //Debug.Log("Hit a player" + other.tag);
        //}

        //Check if the collider has any of the tags in the ignore list.
        //Also check if the arrow can kill and that the target has a trigger. Default is true.
        if (!IgnoreTags.Contains(other.tag) && CanKill && other.GetComponent<BoxCollider2D>().isTrigger)
        {
            //Check individually which of the players the arrow has hit.
            //When the right player is found, the player's object is destroyed, and the respawn process begins.
            if (other.CompareTag("Player01"))
            {
                Destroy(other.gameObject);
                RespawnControl respawnControl = GameObject.Find("ScriptProcessor").GetComponent<RespawnControl>();
                respawnControl.player01Dead = true;
            }

            else if (other.CompareTag("Player02"))
            {
                Destroy(other.gameObject);
                RespawnControl respawnControl = GameObject.Find("ScriptProcessor").GetComponent<RespawnControl>();
                respawnControl.player02Dead = true;
            }

            else if (other.CompareTag("Player03"))
            {
                Destroy(other.gameObject);
                RespawnControl respawnControl = GameObject.Find("ScriptProcessor").GetComponent<RespawnControl>();
                respawnControl.player03Dead = true;
            }

            else if (other.CompareTag("Player04"))
            {
                Destroy(other.gameObject);
                RespawnControl respawnControl = GameObject.Find("ScriptProcessor").GetComponent<RespawnControl>();
                respawnControl.player04Dead = true;
            }

            //Then add 1 point to the player that shot the arrow.
            if (IgnoreTags.Contains("Player01"))
            {
                PlayersPlaying.player1Points++;
            }
            else if (IgnoreTags.Contains("Player02"))
            {
                PlayersPlaying.player2Points++;
            }
            else if (IgnoreTags.Contains("Player03"))
            {
                PlayersPlaying.player3Points++;
            }
            else if (IgnoreTags.Contains("Player04"))
            {
                PlayersPlaying.player4Points++;
            }
        }

        //Else, check if the collider is the ground or a wall.
        else if (IgnoreTags.Contains(other.tag) && !other.CompareTag(IgnoreTags[0]))
        {
            //Make the object kinematic so it stays where it hits
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            //Make it not kill players that want to pick it up.
            //They need it to start killing again...
            CanKill = false;
        }
    }
}
