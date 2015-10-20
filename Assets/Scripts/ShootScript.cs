using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour
{
    //---------
    //Variables
    //---------

    //These are used to point to the right inputs in the InputManager. See Start()
    private string HoriAim;
    private string VertAim;
    private string Fire;

    //Defines how long the player has been charging, in seconds.
    private float ChargeTime;

    //The object to shoot out from the player.
    public GameObject ArrowObject;
    
    //The threshold for when the player can shoot, in seconds. Change in Unity Editor.
    public float ChargeThreshold;

    //The amount of arrows the player has.
    public int ArrowCount;
    
    //---------
    //Scripting
    //---------

	// Use this for initialization
	void Start ()
	{
        //Each of these are defined by looking at the very last character in the objects tag
        //This will be 1 through 4 for each player, which points to the appropriate players joystick input for each axis and button
	    HoriAim = "HoriAim" + tag[gameObject.tag.Length - 1];
        VertAim = "VertAim" + tag[gameObject.tag.Length - 1];
        Fire = "Fire" + tag[gameObject.tag.Length - 1];

        //Starts the player with 3 arrows
	    ArrowCount = 3;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Call ShootingMethod with the appropriate arguments.
        ShootingMethod(Fire, ArrowObject, HoriAim, VertAim);
        //Debug.Log(Input.GetButton("Fire1"));
        //Debug.Log(Input.GetAxis("HoriAim1"));
        //Debug.Log(Input.GetAxis("VertAim1"));
	}

    //Handles aiming and shooting.
    //All controls are changed in Unity's InputManager
    //shootButton is the button used for shooting, default right bumper.
    //arrow is the GameObject the player shoots.
    //horizontal and vertical are the axes used for aiming, default for the right thumbstick
    private void ShootingMethod(string shootButton, GameObject arrow, string horizontal, string vertical)
    {
        //Aiming is calculated in the following way to have a small radial deadzone for smooth aiming.
        //A deadzone for each axis individually would make a cross-shape.
        //That would make aiming snap to caridnal directions over and under certain values.
        //That's no good for twin-stick shooter aiming.

        //Define a deadzone size. Current is 20% of full input.
        //Doesn't need to be less or have gradient sensitivity since the aiming works like twin-stick shooter.
        //Make bigger to negate "stick whiplash" (even if that is barely a problem)
        float deadZone = 0.5f;

        //Define a vector from the inputs of each axis. Will be used to check for 
        Vector2 stickInput = new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical));

        //If the aiming vector gets outside the deadzone, evaluate the code inside
        //Otherwise, do nothing. This keeps the aim at the previous input when the stick isn't used.
        if (stickInput.magnitude > deadZone)
        {
            //Define the angle. Found by calculating the angle between an always-up vector and the input.
            float angle = Vector2.Angle(Vector2.up, stickInput);

            //Finds the cross-product between the vectors.
            Vector3 cross = Vector3.Cross(Vector2.up, stickInput);

            //If the crossproduct is bigger than 0 run code.
            if (cross.z > 0)
            {
                //Subtract 360 from the angle. Effectively, this lets the player aim the full 360 degrees.
                //Instead of only aiming to the right half-circle.
                angle = 360 - angle;
            }

            //Rotates the GameObject used for aiming to the angle, compensated by 90 degrees.
            //Otherwise, aiming up would aim left, aiming right would aim up, etc etc.
            transform.localRotation = Quaternion.Euler(0, 0, angle + 90);
        }

        //If the shootButton is used, run code. shootButton is right bumper by default.
        if (Input.GetButton(shootButton))
        {
            //Increase the charged time by 1 second per second.
            ChargeTime += Time.deltaTime;

        }

        //When shootButton is released, run code.
        if (Input.GetButtonUp(shootButton))
        {
            //Check if the time charged is above the threshold for shooting, and if the player has any arrows.
            //If it is, run code below...
            if (ChargeTime > ChargeThreshold && ArrowCount > 0)
            {
                //Spawns the arrow at the edge of the bow and with current rotation.
                Debug.Log("Shot an arrow");
                GameObject arrowObject = (GameObject)Instantiate(arrow, transform.position + -transform.right * 0.5f, transform.rotation);

                //Sets the Arrow's last tag to ignore to the player's current tag.
                arrowObject.GetComponent<ArrowBehavior>().IgnoreTags[0] = "Player0" + tag[gameObject.tag.Length - 1];

                //Depletes one arrow from the players "quiver".
                ArrowCount--;

                //Then reset the chargetime
                ChargeTime = 0;
            }
            //... Else, do this
            else
            {
                //Reset the chargetime to 0.
                ChargeTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow") && ArrowCount < 3 && other.GetComponent<ArrowBehavior>().CanKill == false)
        {
            Debug.Log("Picked up an arrow");
            ArrowCount++;
            Destroy(other.gameObject);
        }
    }
}
