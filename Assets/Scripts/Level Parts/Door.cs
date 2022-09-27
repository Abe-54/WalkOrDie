using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public GameObject doorGameObject;

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            doorGameObject.SetActive(true);
        }
        else
        {
            doorGameObject.SetActive(false);
        }
    }
}
