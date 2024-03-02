using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    // Update is called once per frame
    void OnGUI()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
