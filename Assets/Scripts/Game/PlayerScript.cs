using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;
    public InventoryScript inventory;

    public GameObject DuckEmmiter;
    public Rigidbody RedDuck;
    public Rigidbody YellowDuck;
    public int projectileSpeed = 3000;


    private int redCount = 0;
    private int yellowCount = 0;
    private DuckColor selectedInv = DuckColor.Red;

    private bool isFireInUse = false;
    private bool isRedInUse = false;
    private bool isYellowInUse = false;
    //private bool isScrollInUse = false;

    Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        if (inventory != null)
        {
            inventory.ChangeSelection(selectedInv);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float fire = Input.GetAxisRaw("Fire1");
        if (fire > 0)
        {
            if (isFireInUse == false)
            {
                if (!SceneScript.isPaused)
                {
                    ShootDuck(selectedInv);
                }
                isFireInUse = true;
            }
        }
        else
        {
            isFireInUse = false;
        }


        float red = Input.GetAxisRaw("OneKey");
        if (red > 0)
        {
            if (isRedInUse == false)
            {
                selectedInv = DuckColor.Red;
                inventory.ChangeSelection(selectedInv);
                isRedInUse = true;
            }
        }
        else
        {
            isRedInUse = false;
        }

        float yellow = Input.GetAxisRaw("TwoKey");
        if (yellow > 0)
        {
            if (isYellowInUse == false)
            {
                selectedInv = DuckColor.Yellow;
                inventory.ChangeSelection(selectedInv);
                isYellowInUse = true;
            }
        }
        else
        {
            isYellowInUse = false;
        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (selectedInv == DuckColor.Red)
                selectedInv = DuckColor.Yellow;
            else
                selectedInv = DuckColor.Red;

                inventory.ChangeSelection(selectedInv);
                isYellowInUse = true;
            
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            kill();
        }

        void kill()
        {
            Debug.Log("Dead");
            currentHealth = maxHealth;
            SceneManager.LoadScene("Loser");
        }
    }

    public void Heal(int healthPoints)
    {
        currentHealth += healthPoints;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public bool IsNeedHeal()
    {
        return currentHealth < maxHealth;
    }

    public void GetDuck(DuckColor color, int count = 3)
    {
        switch (color)
        {
            case DuckColor.Red:
                redCount += count;
                inventory.setColorCount(color, redCount);
                break;
            case DuckColor.Yellow:
                yellowCount += count;
                inventory.setColorCount(color, yellowCount);
                break;
            default:
                break;
        }

    }

    public void ShootDuck(DuckColor color)
    {
        switch (color)
        {
            case DuckColor.Red:
                if (redCount > 0)
                {
                    redCount--;
                    inventory.setColorCount(color, redCount);
                    ShootDuck(RedDuck);
                }
                break;
            case DuckColor.Yellow:
                if (yellowCount > 0)
                {
                    yellowCount--;
                    inventory.setColorCount(color, yellowCount);
                    ShootDuck(YellowDuck);
                }
                break;
            default:
                break;
        }

    }

    public void ShootDuck(Rigidbody duck)
    {
        Rigidbody copy = Instantiate(duck, DuckEmmiter.transform.position, DuckEmmiter.transform.rotation);
        copy.transform.Rotate(Vector3.left * 90);
        copy.AddForce(transform.forward * projectileSpeed);
        //Destroy(copy, 10.0f);
    }

}
