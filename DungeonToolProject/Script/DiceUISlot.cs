using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceUISlot : MonoBehaviour
{

    public Text numberOfDice;
    public DiceType diceType;
    public int _numberOfDice { get; private set; }

    // Use this for initialization
    void Start()
    {
        _numberOfDice = 0;
    }
    
    public void AddDice()
    {
        _numberOfDice++;
        _numberOfDice = Mathf.Clamp(_numberOfDice, 0, 4);
        numberOfDice.text = _numberOfDice.ToString();
    }

    public void RemoveDice()
    {
        _numberOfDice--;
        _numberOfDice = Mathf.Clamp(_numberOfDice, 0, 4);
        numberOfDice.text = _numberOfDice.ToString();
    }

}
