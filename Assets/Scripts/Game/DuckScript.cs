using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DuckColor
{
    Red,
    Yellow
}

public class DuckScript : MonoBehaviour
{
    public GameObject parent;
    private PlayerScript player;

    public DuckColor color;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetDuck(color);
            Destroy(parent);
        }
    }

}
