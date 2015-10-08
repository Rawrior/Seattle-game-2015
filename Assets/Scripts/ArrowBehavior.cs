using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour
{
    public float ShootStrength;
	// Use this for initialization
	void Start () 
    {
	    GetComponent<Rigidbody2D>().AddForce(ShootStrength, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
