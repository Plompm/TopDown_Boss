using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("reload");
            SceneManager.LoadScene("PlayScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Exit");
            Application.Quit();
        }
    }
}
