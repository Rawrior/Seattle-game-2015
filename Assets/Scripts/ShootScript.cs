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
        float deadZone = 0.2f;
        Vector2 stickInput = new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical));

        //Debug.Log(stickInput);

        if (stickInput.magnitude > deadZone)
        {
            float angle = Vector2.Angle(Vector2.up, stickInput);
            Vector3 cross = Vector3.Cross(Vector2.up, stickInput);
            //Debug.Log(angle);

            if (cross.z > 0)
            {
                angle = 360 - angle;
            }

            //float angle = Mathf.Atan2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical)) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            //Debug.Log(Input.GetAxis(vertical));
            //Debug.Log(Input.GetAxis(horizontal));

            //transform.LookAt(transform.position + new Vector3(Input.GetAxis(horizontal), Input.GetAxis(vertical), 0), -Vector3.forward);
            transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
        }

        if (Input.GetButtonDown(shootButton))
        {
            Instantiate(arrow, transform.position, transform.rotation);
        }
    }
}
