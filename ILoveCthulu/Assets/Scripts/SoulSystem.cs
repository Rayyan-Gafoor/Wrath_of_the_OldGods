using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulSystem : MonoBehaviour
{
  
    public float soul_count;
    public float soul_count2;
    public TextMeshProUGUI soul_text;
    public TextMeshProUGUI soul_text2;
    public Image soul_bar;
    int menu = 0;
    //Referenced Scripts
    [Header("References")]
    public GameObject skill_menu;
    public Transform player;
    public Transform gun;
    Weapon weapon;
    damage dmg;

    #region weapon variables
    [Header("Texts for Weapon")]
    public TextMeshProUGUI current_dmg_text;
    public TextMeshProUGUI next_dmg_text;
    public TextMeshProUGUI current_firerate_text;
    public TextMeshProUGUI next_firerate_text;
    public TextMeshProUGUI current_reload_text;
    public TextMeshProUGUI next_reload_text;
    public TextMeshProUGUI current_ammo_text;
    public TextMeshProUGUI next_ammo_text;
    
    [Header("Weapon Cost Texts")]
    public TextMeshProUGUI damage_cost_text;
    public float damage_cost;
    public TextMeshProUGUI firerate_cost_text;
    public float firerate_cost;
    public TextMeshProUGUI ammo_cost_text;
    public float ammo_cost;
    public TextMeshProUGUI reload_cost_text;
    public float reload_cost;
    #endregion
    #region ability variables
    [Header("abilities active")]
    public float ability_cost;
    public bool evade = false;
    public GameObject evade_button;
    public bool shield = false;
    public GameObject shield_button;
    public bool push = false;
    public GameObject push_button;
    [Header("Ability Status Text")]
    public TextMeshProUGUI evade_text;
    public TextMeshProUGUI shield_text;
    public TextMeshProUGUI push_text;
    [Header("Magic")]
    public Image magic_bar;
    public float max_magic;
    public float magic_regen;
    public TextMeshProUGUI current_magic_text;
    public TextMeshProUGUI next_magic_text;
    public TextMeshProUGUI magic_cost_text;
    public float magic_cost;
    #endregion


    void Start()
    {
        magic_bar.fillAmount = 0;
        skill_menu.SetActive(false);
        player = GameObject.Find("Player").transform;
        weapon = gun.GetComponent<Weapon>();
        dmg = player.GetComponent<damage>();
    }

    // Update is called once per frame
    void Update()
    {
        soul_count2 = soul_count;
        soul_text.SetText(soul_count + "");
        soul_text2.SetText(soul_count2 + "");
        increase_magic();
        soul_control();
        if (Input.GetKeyUp(KeyCode.Escape) && menu == 0) 
        {
            skill_menu.SetActive(true);
            menu = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; ;
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && menu == 1)
        {
            skill_menu.SetActive(false);
            menu = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void add_souls(float soul_num)
    {
        soul_count = soul_count + soul_num;
    }
    public void remove_souls(float soul_num)
    {
        soul_count = soul_count - soul_num;
    }
    //Functions to upgrade weapon
    #region weapon upgrades
    public void damage_upgrade()
    {

        if (soul_count > damage_cost)
        {
            //remove cost from soul count;
            remove_souls(damage_cost);
            //upgrade damage
            float current_damage = weapon.damage;
            float next_damage = 0;
            current_damage = current_damage + 5f;
            current_dmg_text.SetText(current_damage + "");
            //set new next damage
            next_damage = current_damage + 5f;
            next_dmg_text.SetText(next_damage + "");
            //set damage
            weapon.damage = current_damage;
            //increase damage cost
            damage_cost += (damage_cost * 0.75f);
            //set damage cost text qual to float value
            damage_cost_text.SetText(damage_cost + "");
           
        }
        else
        {
            return;
        }
       
    }
    public void firerate_upgrade()
    {

        if (soul_count > firerate_cost)
        {
            //remove cost from soul count;
            remove_souls(firerate_cost);
            //upgrade damage
            float current_firerate = weapon.time_between_shots;
            float next_firerate = 0;
            current_firerate = current_firerate + 5f;
            current_firerate_text.SetText(current_firerate + "");
            //set new next damage
            next_firerate = current_firerate + 5f;
            next_firerate_text.SetText(next_firerate + "");
            //set damage
            weapon.time_between_shots = current_firerate;
            //increase damage cost
            firerate_cost += (firerate_cost * 0.75f);
            //set damage cost text qual to float value
            firerate_cost_text.SetText(firerate_cost + "");
            
        }
        else
        {
            return;
        }

    }
    public void ammo_cap_upgrade()
    {
        if (soul_count > ammo_cost)
        {
            //remove cost from soul count;
            remove_souls(ammo_cost);
            //upgrade damage
            float current_ammo = weapon.magazine_size;
            float next_ammo = 0;
            current_ammo = current_ammo + 5f;
            current_ammo_text.SetText(current_ammo + "");
            //set new next damage
            next_ammo = current_ammo + 5f;
            next_ammo_text.SetText(next_ammo + "");
            //set damage
            weapon.magazine_size = current_ammo;
            //increase damage cost
            ammo_cost += (ammo_cost * 0.75f);
            //set damage cost text qual to float value
            ammo_cost_text.SetText(ammo_cost + "");

        }
        else
        {
            return;
        }
    }
    public void reload_upgrade()
    {
        if (soul_count > reload_cost)
        {
            //remove cost from soul count;
            remove_souls(reload_cost);
            //upgrade damage
            float current_reload = weapon.reload_time;
            float next_reload = 0;
            current_reload = current_reload - 0.05f;
            current_reload_text.SetText(current_reload + "");
            //set new next damage
            next_reload = current_reload - 0.05f;
            next_reload_text.SetText(next_reload + "");
            //set damage
            weapon.reload_time = current_reload;
            //increase damage cost
            reload_cost += (reload_cost * 0.75f);
            //set damage cost text qual to float value
            reload_cost_text.SetText(reload_cost + "");

        }
        else
        {
            return;
        }
    }
    #endregion
    //functions to upgrade abilities
    #region ability upgrades
    void abilty_unlock(bool ability_active, GameObject button, TextMeshProUGUI text)
    {
        if (soul_count > ability_cost)
        {
            remove_souls(ability_cost);
            ability_active = true;
            button.SetActive(false);
            text.SetText("Unlocked");

        }
        else
        {
            return;
        }
    }
    public void evade_abaility()
    {
        abilty_unlock(evade, evade_button, evade_text);
    }
    public void shield_abilty()
    {
        abilty_unlock(shield, shield_button, shield_text);
    }
    public void push_ability()
    {
        abilty_unlock(push, push_button, push_text);
    }
    public void magic_upgrade()
    {
        if (soul_count > magic_cost)
        {
            //remove cost from soul count;
            remove_souls(magic_cost);
            //upgrade damage
            float current_magic = weapon.reload_time;
            float next_magic = 0;
            current_magic = current_magic + 10f;
            current_magic_text.SetText(current_magic + "");
            //set new next damage
            next_magic = current_magic + 10f;
            next_magic_text.SetText(next_magic + "");
            //set damage
            max_magic = current_magic;
            //increase damage cost
            magic_cost += (reload_cost * 0.75f);
            //set damage cost text qual to float value
            magic_cost_text.SetText(magic_cost + "");

        }
        else
        {
            return;
        }
    }
   
   public void decrease_magic(float deplete_amount)
    {
        magic_bar.fillAmount -= deplete_amount / max_magic;
    }
    void increase_magic()
    {
        magic_bar.fillAmount += magic_regen / 100;
    }
    #endregion
    void soul_control()
    {
        soul_bar.fillAmount = soul_count / 100;
    }
}
