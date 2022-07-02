using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_ability : MonoBehaviour
{
    public GameObject force_graphic;
    public float magic_cost;
    public float force;
    public float radius_dis;
    public GameObject Player;
    SoulSystem souls;

    private void Start()
    {
        souls = Player.GetComponent<SoulSystem>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (souls.magic_bar.fillAmount > 0)
            {
                push();

            }
            else
            {
                return;
            }
        }
    }
    void push_power()
    {
        GameObject _force = Instantiate(force_graphic, transform.position, transform.rotation);
        Destroy(_force, 0.5f);
        push();
    }
    void push()
    {
        souls.decrease_magic(magic_cost);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius_dis);
        foreach(Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb!= null)
            {
                rb.AddExplosionForce(force, transform.position, radius_dis);
            }
        }
    }
}
