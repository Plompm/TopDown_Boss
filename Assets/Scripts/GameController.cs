using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject _boss;
    public bool isPaused;

    [SerializeField] GameObject _bossWall;

    private void OnEnable()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");

        _boss.GetComponent<Health>().OnHit += onhitPause;

        _bossWall.SetActive(false);
    }

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

    void onhitPause(float nah)
    {
        isPaused = true;
        StartCoroutine(PhaseTransition());
    }

    IEnumerator PhaseTransition()
    {
        _bossWall.SetActive(true);
        yield return new WaitForSeconds(1f);
        isPaused = false;
        _bossWall.SetActive(false);
    }
}
