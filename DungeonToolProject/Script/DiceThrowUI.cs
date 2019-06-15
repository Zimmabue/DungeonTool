using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceThrowUI : MonoBehaviour
{

    public DiceUISlot[] dices;
    public Text result;

    public PlayerConnection_Test player { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    public void LocalThrow()
    {
        string res = "";
        foreach (var dice in dices)
        {
            if (dice._numberOfDice == 0)
                continue;

            res += "<color=#00c><b>" + dice.diceType.ToString() + "</b></color>: ";
            for (int i = 0; i < dice._numberOfDice; i++)
            {
                res += Dice.Throw(dice.diceType) + ", ";
            }
        }
        SetResult(res);
    }

    public void Throw()
    {
        string res = "";
        foreach (var dice in dices)
        {
            if (dice._numberOfDice == 0)
                continue;

            res += "<color=#00c><b>" + dice.diceType.ToString() + "</b></color>: ";
            for (int i = 0; i < dice._numberOfDice; i++)
            {
                res += Dice.Throw(dice.diceType) + ", ";
            }
        }
        player.SendThrowDiceResult(res);
    }

    public void SetResult(string result)
    {
        this.result.text = result;
    }
}
