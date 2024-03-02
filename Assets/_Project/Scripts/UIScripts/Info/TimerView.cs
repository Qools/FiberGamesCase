using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    // Update is called once per frame
    void OnGUI()
    {
        timerText.text = string.Format("{0:00.00}", WaveSpawner.countdown);
    }
}
