using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour
{
    public LayerMask IgnoreMask;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    Debug.DrawRay(transform.position, Vector2.down*0.6f);
        RaycastMethod();
	}

    private void RaycastMethod()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
        }
    }
}
