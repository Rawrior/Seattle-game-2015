using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour
{
    public float ShootStrength;
	// Use this for initialization
	void Start () 
    {
        Debug.Log("Runnin Start");
        GetComponent<Rigidbody2D>().AddForce(transform.right*ShootStrength,ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
