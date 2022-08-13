using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public GameObject parent;
    public int healthPoints = 20;
    private PlayerScript player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.IsNeedHeal())
            {
                player.Heal(healthPoints);

                Destroy(parent);
            }

        }
    }

}
