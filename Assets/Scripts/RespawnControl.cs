using UnityEngine;
using System.Collections;

public class RespawnControl : MonoBehaviour
{
    public bool player01Dead = false;
    public bool player02Dead = false;
    public bool player03Dead = false;
    public bool player04Dead = false;
    public float respawnTimer01;
    public float respawnTimer02;
    public float respawnTimer03;
    public float respawnTimer04;
    public GameObject deadPlayer01;
    public GameObject deadPlayer02;
    public GameObject deadPlayer03;
    public GameObject deadPlayer04;
    public Vector2 respawnLocation01;
    public Vector2 respawnLocation02;
    public Vector2 respawnLocation03;
    public Vector2 respawnLocation04;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player01Dead == true)
        {
            respawnTimer01 += Time.deltaTime;
        }
        if (player02Dead == true)
        {
            respawnTimer02 += Time.deltaTime;
        }
        if (player03Dead == true)
        {
            respawnTimer03 += Time.deltaTime;
        }
        if (player04Dead == true)
        {
            respawnTimer04 += Time.deltaTime;
        }
        if (respawnTimer01 >= 5f)
        {
            Instantiate(deadPlayer01, respawnLocation01, Quaternion.identity);
            respawnTimer01 = 0;
            player01Dead = false;
        }
        if (respawnTimer02 >= 5f)
        {
            Instantiate(deadPlayer02, respawnLocation02, Quaternion.identity);
            respawnTimer02 = 0;
            player02Dead = false;
        }
        if (respawnTimer03 >= 5f)
        {
            Instantiate(deadPlayer03, respawnLocation03, Quaternion.identity);
            respawnTimer03 = 0;
            player03Dead = false;
        }
        if (respawnTimer04 >= 5f)
        {
            Instantiate(deadPlayer04, respawnLocation04, Quaternion.identity);
            respawnTimer04 = 0;
            player04Dead = false;
        }
	}
}
