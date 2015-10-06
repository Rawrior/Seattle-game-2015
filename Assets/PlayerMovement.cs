using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement();
	}

    void movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)* 10 * Time.deltaTime);
    }
}
