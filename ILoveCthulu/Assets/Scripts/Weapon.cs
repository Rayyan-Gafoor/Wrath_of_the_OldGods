using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage;
    public float time_between_shooting, spread, range, reload_time, time_between_shots;
    public float magazine_size, bullets_per_tap;
    public bool allow_button_hold;
    public float bullets_left, bullets_shot;

    [Header("weapon States")]
    [SerializeField] bool shooting;
    [SerializeField] bool ready_to_shoot;
    [SerializeField] bool reloading;

    [Header("Referenced Objects")]
    public Camera fps_cam;
    public Transform attack_point;
    public RaycastHit ray_hit;
    public LayerMask enemy_mask;

    [Header("Game Feel")]
    public GameObject flash, bullet_hole;
    public Image crosshair;
    //public CamShake cam_shake;
    public float cam_shake_mag, cam_shake_duration;
    public TextMeshProUGUI text;
    public Vector3 direction;

    private void Awake()
    {
        bullets_left = magazine_size;
        ready_to_shoot = true;
    }
    private void Update()
    {
        player_input();
        text.SetText(bullets_left + "/" + magazine_size);
        control_crosshair();
    }
    void player_input()
    {
        if (allow_button_hold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if(Input.GetKeyDown(KeyCode.R) && bullets_left< magazine_size && !reloading)
        {
            reload();
        }
        if(ready_to_shoot && shooting && ! reloading && bullets_left > 0)
        {
            bullets_shot = bullets_per_tap;
            shoot();
        }
    }
    void control_crosshair()
    {
        direction = fps_cam.transform.forward ;
        if (Physics.Raycast(fps_cam.transform.position, direction, out ray_hit, range, enemy_mask))
        {
           
            if (ray_hit.collider.name == "Enemy")
            {
                crosshair.color = Color.red;
                Debug.Log("this is a Enemy");
            }
            else
            {
                crosshair.color = Color.white;
               
            }
        }
    }
    void shoot()
    {
        ready_to_shoot = false;
        float x=Random.Range(-spread, spread);
        float y=Random.Range(-spread, spread);
        direction = fps_cam.transform.forward + new Vector3(x, y, 0);
        //direction = fps_cam.transform.forward;

        if (Physics.Raycast(fps_cam.transform.position, direction, out ray_hit, range, enemy_mask))
        {
           
            if (ray_hit.collider.name=="Enemy")
            {
                Debug.Log("Enemy was HIT");
                Debug.Log(ray_hit.collider.name);
                ray_hit.collider.GetComponent<EnemyBehaviour>().take_damage(damage);
            }
           
        }
       
        //camShake.shake....

        //graphics
        Instantiate(bullet_hole, ray_hit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(flash, attack_point.position, Quaternion.identity);
        bullets_left--;
        bullets_shot--;

        Invoke("reset_shot", time_between_shooting);

        if(bullets_shot>0 && bullets_left > 0)
        {
            Invoke("shoot", time_between_shots);
        }
    }
    void reset_shot()
    {
        ready_to_shoot = true;
    }
    void reload()
    {
        reloading = true;
        Invoke("reload_done", reload_time);
    }
    void reload_done()
    {
        bullets_left = magazine_size;
        reloading = false;
    }
}
