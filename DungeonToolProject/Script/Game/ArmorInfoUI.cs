using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorInfoUI : MonoBehaviour
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

    public InputField armorName;
    public InputField resitence;
    public Button saveButton;

    private WorldEntityNetwork _worldEntityNetwork;

    public void View(WorldEntityNetwork entity)
    {
        _worldEntityNetwork = entity;
        armorName.text = entity.worldEntity.name;

        _worldEntityNetwork.onWorldEntityChange = () => View(entity);
    }

    public void Save()
    {
        _worldEntityNetwork.worldEntity.name = armorName.text;
        _worldEntityNetwork.worldEntity.armour.resistance = int.Parse(armorName.text);

        _worldEntityNetwork.UpdateWorldEntity();
    }

    private void SetEnable(bool value)
    {
        armorName.readOnly = !value;
        resitence.readOnly = !value;
        saveButton.gameObject.SetActive(value);
    }

}
