using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D playerCameraConfiner;
    public Collider2D currentCameraConfiner;

    public List<Level> levels;

    public GameObject gameOverScreen;

    public int currentLevel = 0;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        currentCameraConfiner = levels[0].levelConfiner;

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCameraConfiner.m_BoundingShape2D = currentCameraConfiner;

        if (!player.isActiveAndEnabled)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ResetTimer(Timer timerToReset, int time)
    {
        timerToReset.time = time;
    }
}
