using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameObject_life : MonoBehaviour
{
    public float life_time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy_self());
    }

    IEnumerator destroy_self()
    {
        yield return new WaitForSeconds(life_time);
        Destroy(gameObject);
    }
}
