using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;

    private void OnGUI()
    {
        livesText.text = PlayerStats.Lives.ToString() + " " + "LIVES";
    }
}
