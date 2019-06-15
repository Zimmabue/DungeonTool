using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterInfoUI : MonoBehaviour
{

    [Header("Basics")]
    public InputField characterName;
    public InputField classesLevel;
    public InputField races;
    public InputField alignment;
    public InputField divinity;
    public InputField age;
    public InputField gender;
    public InputField height;
    public InputField weight;
    public InputField eyes;
    public InputField hair;
    public InputField skin;

    [Header("Attribues")]

    public InputField strenght; public InputField strenghtModifier;
    public InputField dexterity, dexterityModifier;
    public InputField consitution, consitutionModifier;
    public InputField intelligence, intelligenceModifier;
    public InputField wisdom, wisdomModifier;
    public InputField charisma, charismaModifier;

    [Header("SavignThrows")]
    public InputField st_strenght;
    public InputField st_dexterity;
    public InputField st_constitutio;
    public InputField st_intelligence;
    public InputField st_wisdom;
    public InputField st_charisma;

    [Header("Hit")]
    public InputField armorClass;
    public InputField currentHitPoint;
    public InputField temporaryHitPoint;
    public InputField speed;

    public Button saveButton;

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

    private WorldEntityNetwork _worldEntityNetwork;

    public void View(WorldEntityNetwork entity)
    {
        _worldEntityNetwork = entity;
        var c = entity.worldEntity.character;
        characterName.text = entity.worldEntity.name;
        classesLevel.text = c.classesLevel;
        races.text = c.races;
        alignment.text = c.alignment;
        divinity.text = c.divinity;
        age.text = c.age;
        gender.text = c.gender;
        height.text = c.height.ToString();
        weight.text = c.weight.ToString();
        eyes.text = c.eyes;
        hair.text = c.hair;
        skin.text = c.skin;

        strenght.text = c.strenght.ToString();
        dexterity.text = c.dexterity.ToString();
        consitution.text = c.consitution.ToString();
        intelligence.text = c.intelligence.ToString();
        wisdom.text = c.wisdom.ToString();
        charisma.text = c.charisma.ToString();

        armorClass.text = c.armorClass.ToString();
        currentHitPoint.text = c.currentHitPoint.ToString();
        temporaryHitPoint.text = c.temporaryHitPoint.ToString();
        speed.text = c.speed.ToString();

        st_strenght.text = c.st_strenght.ToString();
        st_dexterity.text = c.st_dexterity.ToString();
        st_constitutio.text = c.st_constitutio.ToString();
        st_intelligence.text = c.st_intelligence.ToString();
        st_wisdom.text = c.st_wisdom.ToString();
        st_charisma.text = c.st_charisma.ToString();

        _worldEntityNetwork.onWorldEntityChange = () => View(entity);
    }

    public void Save()
    {
        var c = _worldEntityNetwork.worldEntity.character;
        c.classesLevel = classesLevel.text;
        c.races = races.text;
        c.alignment = alignment.text;
        c.divinity = divinity.text;
        c.age = age.text;
        c.gender = gender.text;
        c.height = float.Parse(height.text);
        c.weight = float.Parse(weight.text);
        c.eyes = eyes.text;
        c.hair = hair.text;
        c.skin = skin.text;

        c.strenght = int.Parse(strenght.text);
        c.dexterity = int.Parse(dexterity.text);
        c.consitution = int.Parse(consitution.text);
        c.intelligence = int.Parse(intelligence.text);
        c.wisdom = int.Parse(wisdom.text);
        c.charisma = int.Parse(charisma.text);

        c.armorClass = int.Parse(armorClass.text);
        c.currentHitPoint = int.Parse(currentHitPoint.text);
        c.temporaryHitPoint = int.Parse(temporaryHitPoint.text);
        c.speed = int.Parse(speed.text);

        c.st_strenght = int.Parse(st_strenght.text);
        c.st_dexterity = int.Parse(st_dexterity.text);
        c.st_constitutio = int.Parse(st_constitutio.text);
        c.st_intelligence = int.Parse(st_intelligence.text);
        c.st_wisdom = int.Parse(st_wisdom.text);
        c.st_charisma = int.Parse(st_charisma.text);

        _worldEntityNetwork.UpdateWorldEntity();
    }

    private void SetEnable(bool value)
    {
        characterName.readOnly = !value;
        classesLevel.readOnly = !value;
        races.readOnly = !value;
        alignment.readOnly = !value;
        divinity.readOnly = !value;
        age.readOnly = !value;
        gender.readOnly = !value;
        height.readOnly = !value;
        weight.readOnly = !value;
        eyes.readOnly = !value;
        hair.readOnly = !value;
        skin.readOnly = !value;

        strenght.readOnly = !value;
        dexterity.readOnly = !value;
        consitution.readOnly = !value;
        intelligence.readOnly = !value;
        wisdom.readOnly = !value;
        charisma.readOnly = !value;

        st_strenght.readOnly = !value;
        st_dexterity.readOnly = !value;
        st_constitutio.readOnly = !value;
        st_intelligence.readOnly = !value;
        st_wisdom.readOnly = !value;
        st_charisma.readOnly = !value;

        armorClass.readOnly = !value;
        currentHitPoint.readOnly = !value;
        temporaryHitPoint.readOnly = !value;
        speed.readOnly = !value;

        saveButton.gameObject.SetActive(value);
    }

}
