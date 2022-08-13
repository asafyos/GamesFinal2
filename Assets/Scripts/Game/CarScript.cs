using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarScript : MonoBehaviour
{
    public Camera FrontCamera;
    public Camera BackCamera;
    public int maxFuel = 4000;
    public int currentFuel;

    public HealthBarScript healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentFuel = maxFuel;
        healthBar.SetMaxHealth(maxFuel);
        FrontCamera.gameObject.SetActive(false);
        BackCamera.gameObject.SetActive(true);     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (BackCamera.isActiveAndEnabled)
            {
                FrontCamera.gameObject.SetActive(true);
                BackCamera.gameObject.SetActive(false);
            }
            else
            {
                FrontCamera.gameObject.SetActive(false);
                BackCamera.gameObject.SetActive(true);
            }
            
        }
                
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            currentFuel -= 1;
            healthBar.SetHealth(currentFuel);

            if (currentFuel == 0)
            {
                kill();
            }
        }
    }

    void kill()
    {
        Debug.Log("Dead");
        currentFuel = maxFuel;
        SceneManager.LoadScene("Loser");
    }
}
