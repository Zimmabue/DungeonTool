using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerRoundSlot : MonoBehaviour
{

    public Text playerName;
    public Image roundImage;
    public Color roundColor;
    public Color notRoundColor;

    public void SetPlayer(string name, bool isHisRound)
    {
        playerName.text = name;
        roundImage.color = isHisRound ? roundColor : notRoundColor;
    }

    public void SetRound(bool isHisRound)
    {
        roundImage.color = isHisRound ? roundColor : notRoundColor;
    }

}
