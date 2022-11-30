using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private const int deepMinX = -76;
    private const int deepMaxX = 86;
    private const int deepMinY = -62;
    private const int deepMaxY = 66;

    private void Start()
    {
        int innerTotal = 0;
        int outerTotal = 0;
        Coin[] coins = FindObjectsOfType<Coin>();
        foreach (Coin coin in coins)
        {
            float x = coin.transform.position.x;
            float y = coin.transform.position.y;
            if (x > deepMinX && x < deepMaxX && y > deepMinY && y < deepMaxY)
            {
                innerTotal += coin.value;
            }
            else
            {
                outerTotal += coin.value;
            }
        }
        print("Inner coins: " + innerTotal);
        print("Outer coins: " + outerTotal);
    }
}
