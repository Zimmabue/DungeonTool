using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponInfoUI : MonoBehaviour
{

    private bool _isEditable;
    public bool isEditable
    {
        get { return _isEditable; }
        set
        {
            _isEditable = value;
            SetEnable(value);
        }
    }

    public InputField weaponName;
    public InputField numberOfDices;
    public Dropdown dice;
    public InputField rangeMin;
    public InputField rangeMax;
    public Button saveButton;

    private WorldEntityNetwork _worldEntityNetwork;

    public void View(WorldEntityNetwork entity)
    {
        _worldEntityNetwork = entity;
        var w = entity.worldEntity.weapon;
        weaponName.text = entity.worldEntity.name;
        numberOfDices.text = w.numberOfDices.ToString();
        dice.value = (int)w.dice;
        rangeMin.text = w.rangeMin.ToString();
        rangeMax.text = w.rangeMax.ToString();

        _worldEntityNetwork.onWorldEntityChange = () => View(entity);
    }

    public void Save()
    {
        _worldEntityNetwork.ChangeName(weaponName.text);
        var w = _worldEntityNetwork.worldEntity.weapon;
        w.numberOfDices = int.Parse(numberOfDices.text);
        w.dice = (DiceType)dice.value;
        w.rangeMin = int.Parse(rangeMin.text);
        w.rangeMax = int.Parse(rangeMax.text);

        _worldEntityNetwork.UpdateWorldEntity();
    }

    private void SetEnable(bool value)
    {
        weaponName.readOnly = !value;
        numberOfDices.readOnly = !value;
        dice.interactable = value;
        rangeMin.readOnly = !value;
        rangeMax.readOnly = !value;
        saveButton.gameObject.SetActive(value);
    }
}
