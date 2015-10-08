using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour
{
    public string HoriAim;
    public string VertAim;
    public string Fire;
    public GameObject ArrowObject;

	// Use this for initialization
	void Start ()
	{
	    HoriAim = "HoriAim" + tag[gameObject.tag.Length - 1];
        VertAim = "VertAim" + tag[gameObject.tag.Length - 1];
        Fire = "Fire" + tag[gameObject.tag.Length - 1];
	}
	
	// Update is called once per frame
	void Update () 
    {
        ShootingMethod(Fire, ArrowObject, HoriAim, VertAim);
        //Debug.Log(Input.GetButton("Fire1"));
        //Debug.Log(Input.GetAxis("HoriAim1"));
        //Debug.Log(Input.GetAxis("VertAim1"));
	}

    private void ShootingMethod(string shootButton, GameObject arrow, string horizontal, string vertical)
    {
        float angle = Mathf.Atan2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical)) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        //Debug.Log(Input.GetAxisRaw(vertical));
        //Debug.Log(Input.GetAxisRaw(horizontal));

        //transform.LookAt(transform.position + new Vector3(Input.GetAxis(horizontal), Input.GetAxis(vertical), 0), -Vector3.forward);
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetButtonDown(shootButton))
        {
            Instantiate(arrow, transform.position, transform.rotation);
        }
    }
}
