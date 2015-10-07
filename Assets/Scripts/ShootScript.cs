using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(Input.GetKey("joystick 2 button 5"));
	}

    private void ShootingMethod(string shootButton, GameObject arrow, float horizontal, float vertical)
    {
        transform.LookAt(transform.position + Vector3.);

        if (Input.GetKeyDown(shootButton))
        {
            Instantiate(arrow, transform.position, transform.rotation);
        }
    }
}
