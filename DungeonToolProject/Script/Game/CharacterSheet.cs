using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSheet : MonoBehaviour
{

    public delegate void OnSave(string name);
    public OnSave onSave;

    public static CharacterSheet Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [Header("Basics")]
    public GameObject sheet;
    public Text characterName;
    public Text classesLevel;
    public Text races;
    public Text alignment;
    public Text divinity;
    public Text age;
    public Text gender;
    public Text height;
    public Text weight;
    public Text eyes;
    public Text hair;
    public Text skin;

    [Header("Attribues")]

    public Text strenght; public Text strenghtModifier;
    public Text dexterity, dexterityModifier;
    public Text consitution, consitutionModifier;
    public Text intelligence, intelligenceModifier;
    public Text wisdom, wisdomModifier;
    public Text charisma, charismaModifier;

    [Header("SavignThrows")]
    public Text st_strenght;
    public Text st_dexterity;
    public Text st_constitutio;
    public Text st_intelligence;
    public Text st_wisdom;
    public Text st_charisma;

    [Header("Hit")]
    public Text armorClass;
    public Text currentHitPoint;
    public Text speed;

    [Header("Sprites")]
    public Image characterSprite;

    public PlayerConnection_Test player { get; set; }

    private int spriteCounter;
    private Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        spriteCounter = 0;
        sprites = SpriteLoader.Instance.GetAllCharacters();
        characterSprite.sprite = sprites[spriteCounter];
    }

    public void NextSprite()
    {
        spriteCounter++;
        spriteCounter = Mathf.Clamp(spriteCounter, 0, sprites.Length - 1);
        characterSprite.sprite = sprites[spriteCounter];
    }

    public void PrecSprite()
    {
        spriteCounter--;
        spriteCounter = Mathf.Clamp(spriteCounter, 0, sprites.Length - 1);
        characterSprite.sprite = sprites[spriteCounter];
    }

    public void Activate()
    {
        sheet.SetActive(true);
    }

    public void Disable()
    {
        sheet.SetActive(false);
    }

    public void Save()
    {
        Storage.StorageManager.GetDataAccess().Save(Application.persistentDataPath + "/local.pl", characterName.text);

        WorldEntity pl = WorldEntityFabric.Character(characterName.text, sprites[spriteCounter].name);
        Character c = new Character(true);
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

        c.strenght = int.Parse(strenght.text); c.strenghtModifier = int.Parse(strenghtModifier.text);
        c.dexterity = int.Parse(dexterity.text); c.dexterityModifier = int.Parse(dexterityModifier.text);
        c.consitution = int.Parse(consitution.text); c.consitutionModifier = int.Parse(consitutionModifier.text);
        c.intelligence = int.Parse(intelligence.text); c.intelligenceModifier = int.Parse(intelligenceModifier.text);
        c.wisdom = int.Parse(wisdom.text); c.wisdomModifier = int.Parse(wisdomModifier.text);
        c.charisma = int.Parse(charisma.text); c.charismaModifier = int.Parse(charismaModifier.text);

        c.st_strenght = int.Parse(st_strenght.text);
        c.st_dexterity = int.Parse(st_dexterity.text);
        c.st_constitutio = int.Parse(st_constitutio.text);
        c.st_intelligence = int.Parse(st_intelligence.text);
        c.st_wisdom = int.Parse(st_wisdom.text);
        c.st_charisma = int.Parse(st_charisma.text);

        c.armorClass = int.Parse(armorClass.text);
        c.currentHitPoint = int.Parse(currentHitPoint.text);
        c.speed = int.Parse(speed.text);

        pl.character = c;

        player.CreateNewPlayer(pl);
        if (onSave != null)
            onSave(characterName.text);
    }

}
