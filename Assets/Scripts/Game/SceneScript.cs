using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Vehicles.Car;

public class SceneScript : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameUI;

    private bool isAxisInUse = false;

    public static int currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (gameObject.tag == "UI")
        {

            float escape = Input.GetAxisRaw("Cancel");

            if (escape != 0)
            {
                if (isAxisInUse == false)
                {

                    if (isPaused)
                    {
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }

                    isAxisInUse = true;

                }
            }
            else
            {
                isAxisInUse = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OpenLoading();
        }
    }

    void OpenLoading()
    {
        SceneManager.LoadScene("Loading");
    }

    public static void NextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(currentScene);
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(isPaused);
        gameUI.SetActive(!isPaused);
        Time.timeScale = 1f;

        GameObject player = GameObject.FindWithTag("Player");
        FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        if (fpc != null)
        {
            fpc.enabled = !isPaused;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(isPaused);
        gameUI.SetActive(!isPaused);
        Time.timeScale = 0f;

        GameObject player = GameObject.FindWithTag("Player");
        FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        if (fpc != null)
        {
            fpc.enabled = !isPaused;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}