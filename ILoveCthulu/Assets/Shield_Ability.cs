using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Ability : MonoBehaviour
{
    public GameObject shield_spher, shield_screen;
    public float magic_cost;
    public GameObject Player;
    SoulSystem souls;

    // Start is called before the first frame update
    void Start()
    {
        shield_screen.SetActive(false);
        shield_spher.SetActive(false);
        souls = Player.GetComponent<SoulSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {

            activate_shield();
        }
        else
        {
            shield_screen.SetActive(false);
            shield_spher.SetActive(false);
        }
    }
    void activate_shield()
    {
        if (souls.magic_bar.fillAmount > 0)
        {
            souls.decrease_magic(magic_cost);
            shield_screen.SetActive(true);
            shield_spher.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
