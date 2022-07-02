using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_type : MonoBehaviour
{
    public enum projectile_type
    {
        normal_bullet
    }
    public projectile_type Projectile;
    public float enemy_damage;
    public Transform player;
    damage damage;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        damage = player.GetComponent<damage>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Player")
        {
            Debug.Log("Contact with player");
            damage.take_damage(enemy_damage);
        }
        else if(other.tag!="Enemy")
        {
            Destroy(gameObject);
        }
    }

}
