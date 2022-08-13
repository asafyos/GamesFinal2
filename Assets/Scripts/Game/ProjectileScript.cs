using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Kiil());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Kiil()
    {
        yield return new WaitForSeconds(10.0f);

        Destroy(gameObject);
    }
}
