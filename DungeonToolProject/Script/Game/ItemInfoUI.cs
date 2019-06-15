using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoUI : MonoBehaviour
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

    public InputField itemName;
    public Button saveButton;

    private WorldEntityNetwork _worldEntityNetwork;

    public void View(WorldEntityNetwork entity)
    {
        _worldEntityNetwork = entity;
        itemName.text = entity.worldEntity.name;

        _worldEntityNetwork.onWorldEntityChange = () => View(entity);
    }

    public void Save()
    {
        _worldEntityNetwork.worldEntity.name = itemName.text;

        _worldEntityNetwork.UpdateWorldEntity();
    }

    private void SetEnable(bool value)
    {
        itemName.readOnly = !value;
        saveButton.gameObject.SetActive(value);
    }

}
