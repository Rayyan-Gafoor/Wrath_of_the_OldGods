using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour
{
    public Transform camera_pos;
    // Update is called once per frame
    void Update()
    {
        transform.position = camera_pos.position;
    }
}
