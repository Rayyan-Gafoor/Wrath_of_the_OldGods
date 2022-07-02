using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class damage : MonoBehaviour
{
    public Image health_bar;
    public float player_health;
    // Start is called before the first frame update
    void Start()
    {
        player_health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        control_health_bar();
    }
    void control_health_bar()
    {
        health_bar.fillAmount = player_health / 100;
        if (player_health > 100)
        {
            player_health = 100;
        }
        if (player_health < 0)
        {
            game_over();
        }
    }
    public void increase_health(float health_amount)
    {
        player_health = player_health + health_amount;
    }
    public void take_damage(float damage)
    {
        Debug.Log("Take Damage called");
        player_health = player_health - damage;
    }
    public void game_over()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
        Debug.Log("game Over");
    }
}
