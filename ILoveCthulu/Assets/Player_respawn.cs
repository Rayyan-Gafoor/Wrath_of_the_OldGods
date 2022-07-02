using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_respawn : MonoBehaviour
{
    public GameObject player;
    respawn respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = player.GetComponent<respawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(respawn.respawn__player());
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
