using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    public float timeRunningMultiplier = 3;
    public float timeRefillingMultiplier = 1;
    public string prefix;
    public TMP_Text uiText;

    public bool timerIsRunning;

    private bool isOn;

    void Start()
    {
        uiText = GetComponent<TMP_Text>();
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0 && isOn)
        {
            if (timerIsRunning)
            {

                time -= Time.deltaTime * timeRunningMultiplier;
            }
            else
            {
                time += Time.deltaTime * timeRefillingMultiplier;
            }
        }
        else
        {
            Debug.Log("Time ran out");
            time = 0;
            isOn = false;
        }

        uiText.text = prefix + DisplayTime(time);
    }

    string DisplayTime(float timeToDisplay)
    {
        int seconds = Mathf.FloorToInt(timeToDisplay);

        return seconds.ToString();
    }
}
