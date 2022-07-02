using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable_Blocker : MonoBehaviour
{
    GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.Find("Enemy");
        if (enemies == null)
        {
            Destroy(gameObject, 2f);
            Debug.Log("all enemies are gone");
        }
        else
        {
            Debug.Log("there are still enemies");
        }
    }
}
