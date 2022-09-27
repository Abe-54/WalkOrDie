using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStill : MonoBehaviour
{
    public float miniTimer = 5;
    public GameObject buttonToAppear;

    private PlayerInputManager playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (miniTimer <= 0)
        {
            buttonToAppear.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (playerInput.NormInputX == 0 && playerInput.NormInputY == 0)
            {
                if (miniTimer > 0)
                {
                    miniTimer -= Time.deltaTime;
                }
            }
        }
    }
}
