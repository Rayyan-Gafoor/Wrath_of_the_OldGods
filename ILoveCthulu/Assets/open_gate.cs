using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_gate : MonoBehaviour
{
    public Vector3 current_size;
    public Vector3 shrink_size;
    public float speed;

    private void Start()
    {
        current_size = transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            transform.position = Vector3.Lerp(transform.position, shrink_size, speed*Time.deltaTime);
            Debug.Log("Open");
        }
    }
}
