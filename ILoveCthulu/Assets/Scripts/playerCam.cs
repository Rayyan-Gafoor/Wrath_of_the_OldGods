using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour
{
    [Header("Camera Variables")]
    public float sensX;
    public float sensY;
    public Transform player_orientation;
    float x_rotation;
    float y_rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;
        y_rotation += mouseX;
        x_rotation -= mouseY;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(x_rotation, y_rotation, 0);
        player_orientation.rotation = Quaternion.Euler(0, y_rotation, 0);
    }
}
