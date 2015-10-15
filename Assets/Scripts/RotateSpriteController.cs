using UnityEngine;
using System.Collections;

public class RotateSpriteController : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    RotateMethod();
        Debug.Log(gameObject.transform.parent.transform.localEulerAngles.z);
	}

    private void RotateMethod()
    {
        if (gameObject.transform.parent.transform.localEulerAngles.z <= 270 && gameObject.transform.parent.transform.localEulerAngles.z >= 90)
        {
            transform.localScale = new Vector3(1,-1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }

}
