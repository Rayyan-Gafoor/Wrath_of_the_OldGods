using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSouls : MonoBehaviour
{
    public GameObject player;
    public float move_speed;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
    }

    private void FixedUpdate()
    {
        //calculate direction
        Vector3 dir = player.transform.position- transform.position ;
        dir = dir.normalized;
        //move in dir
        transform.transform.position += dir * Time.deltaTime * move_speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }

}
