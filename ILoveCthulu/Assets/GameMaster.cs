using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject tutorial, credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            close_game();
        }
    }
    public void play()
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }
    public void close_game()
    {
        Application.Quit();
    }
    public void control()
    {
        tutorial.SetActive(true);
    }
    public void credit()
    {
        credits.SetActive(true);
    }
    public void back()
    {
        credits.SetActive(false);
        tutorial.SetActive(false);
    }
    
}
