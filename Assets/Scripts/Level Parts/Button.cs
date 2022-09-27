using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door doorToOpen;

    public string tagForButton = "Player";

    public GameObject buttonGameObject;
    private Vector3 orignalScale;

    // Start is called before the first frame update
    void Start()
    {
        orignalScale = new Vector3(buttonGameObject.transform.localScale.x, buttonGameObject.transform.localScale.y, buttonGameObject.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagForButton))
        {
            buttonGameObject.transform.localScale = new Vector3(buttonGameObject.transform.localScale.x, buttonGameObject.transform.localScale.y / 2, buttonGameObject.transform.localScale.z);
            doorToOpen.isOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagForButton))
        {
            buttonGameObject.transform.localScale = orignalScale;
            doorToOpen.isOpen = false;
        }
    }
}
