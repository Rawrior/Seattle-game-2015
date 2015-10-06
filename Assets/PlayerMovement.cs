using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public string Horizontal;
    public string Vertical;


	// Use this for initialization
	void Start ()
    {
        Debug.Log(gameObject.tag);
        Horizontal = "Horizontal0" + tag[gameObject.tag.Length - 1];
        Debug.Log(Horizontal);
        Vertical = "Vertical0" + tag[gameObject.tag.Length - 1];
        Debug.Log(Vertical);
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement(Horizontal, Vertical);
	}

    void movement(string horizontal, string vertical)
    {
        transform.Translate(new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical))* 10 * Time.deltaTime);
    }
}
