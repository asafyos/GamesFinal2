using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public GameObject holders;

    private void OnTriggerEnter(Collider other)
    {
        if (holders != null)
        {
            holders.SetActive(false);
        }
    }
}
