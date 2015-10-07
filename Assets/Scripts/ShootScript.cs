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
	    Debug.Log(Input.GetAxis("Fire1"));
	}

    private void ShootingMethod(bool shootButton)
    {
        if (shootButton)
        {
            
        }
    }
}
