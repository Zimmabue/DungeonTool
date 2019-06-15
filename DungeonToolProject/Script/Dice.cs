using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public enum DiceType
{
    D4,
    D6,
    D8,
    D10,
    D12,
    D20,
    D90
}

public static class Dice
{

    public static int Throw(params DiceType[] dice)
    {
        int res = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            switch (dice[i])
            {
                case DiceType.D4:
                    res += Random.Range(1, 5);
                    break;
                case DiceType.D6:
                    res += Random.Range(1, 7);
                    break;
                case DiceType.D8:
                    res += Random.Range(1, 9);
                    break;
                case DiceType.D10:
                    res += Random.Range(0, 10);
                    break;
                case DiceType.D12:
                    res += Random.Range(1, 13);
                    break;
                case DiceType.D20:
                    res += Random.Range(1, 21);
                    break;
                case DiceType.D90:
                    res += Random.Range(0, 10) * 10;
                    break;
                default:
                    break;
            }
        }

        return res;
    }
    
}
