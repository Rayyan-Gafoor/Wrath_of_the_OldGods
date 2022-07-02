using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject checkpoint;
    public GameObject Player;
    damage damage_;
    public bool is__dead = false;
    public int life__count = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        damage_ = Player.GetComponent<damage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is__dead)
        {
            //Debug.Log("player respawn");

            StartCoroutine(respawn__player());

        }
    }
    public IEnumerator respawn__player()
    {
       
        Debug.Log("respawn");
        transform.position = checkpoint.transform.position;
        damage_.take_damage(5f);
        is__dead = false;

        //life__count = life__count - 1;


        yield break;
    }

}
