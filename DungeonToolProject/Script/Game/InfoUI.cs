using UnityEngine;
using System.Collections;

public class InfoUI : MonoBehaviour
{

    public static InfoUI Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public CharacterInfoUI characterInfoUI;
    public WeaponInfoUI weaponInfoUI;
    public ItemInfoUI itemInfoUI;
    public ArmorInfoUI armorInfoUI;

    public void View(WorldEntityNetwork worldEntityNet, bool isEditable)
    {
        characterInfoUI.gameObject.SetActive(false);
        weaponInfoUI.gameObject.SetActive(false);
        itemInfoUI.gameObject.SetActive(false);
        armorInfoUI.gameObject.SetActive(false);

        switch (worldEntityNet.worldEntity.type)
        {
            case WorldEntityType.Character:
                characterInfoUI.gameObject.SetActive(true);
                characterInfoUI.isEditable = isEditable;
                characterInfoUI.View(worldEntityNet);
                break;
            case WorldEntityType.Container:
                itemInfoUI.gameObject.SetActive(true);
                itemInfoUI.isEditable = isEditable;
                itemInfoUI.View(worldEntityNet);
                break;
            case WorldEntityType.Weapon:
                weaponInfoUI.gameObject.SetActive(true);
                weaponInfoUI.isEditable = isEditable;
                weaponInfoUI.View(worldEntityNet);
                break;
            case WorldEntityType.Armour:
                armorInfoUI.gameObject.SetActive(true);
                armorInfoUI.isEditable = isEditable;
                armorInfoUI.View(worldEntityNet);
                break;
            default:
                break;
        }
    }
    
}
