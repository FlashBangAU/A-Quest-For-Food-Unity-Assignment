using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public TextMeshProUGUI coinCounterUI;

    public int coinCount = 0;

    private void Start()
    {
        coinCounterUI.text = "Food: 0";
    }

    void Update()
    {
        coinCounterUI.text = "Food: " + coinCount;
    }
}
