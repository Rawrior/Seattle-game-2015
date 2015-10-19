using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour
{
    //public LayerMask IgnoreMask;
    private RaycastHit2D hit;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    Debug.DrawRay(transform.position + new Vector3(0.1f,0,0), Vector2.down*0.6f);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f);
        Debug.DrawRay(transform.position + new Vector3(-0.1f,0,0), Vector2.down * 0.6f);
        RaycastMethod();
    }

    private void RaycastMethod()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (RaycastHit())
        {
            Debug.Log("It's working");
        }
    }

    public bool RaycastHit()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.down, 0.6f))
        {
            //Debug.Log("Hit");
            hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.down, 0.6f);
            return true;
        }
        else if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f))
        {
            //Debug.Log("Hit");
            hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.down, 0.6f);
            return true;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector2.down, 0.6f))
        {
            //Debug.Log("Hit");
            hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.down, 0.6f);
            return true;
        }
        else
        {
            return false;
        }
    }
}
