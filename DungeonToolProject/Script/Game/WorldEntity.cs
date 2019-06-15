using UnityEngine;

public class WorldEntity
{

    public string name;
    public string spriteName;
    public WorldEntityType type;
    public Character character;
    public Inventory inventory;
    public Equipment equipment;
    public Weapon weapon;
    public Armour armour;
    public string description;
    public Vector2 position;

    public WorldEntity() { }

}

[System.Serializable]
public class WESerializable
{
    public string name { get; set; }
    public string spriteName { get; set; }
    public WorldEntityType type { get; set; }
    public Character character { get; set; }
    public Inventory inventory { get; set; }
    public Equipment equipment { get; set; }
    public Weapon weapon { get; set; }
    public Armour armour { get; set; }
    public string description { get; set; }
    public float[] position { get; set; }

    public WESerializable() { }
}

[System.Serializable]
public class Character
{
    public bool enable = false;

    //general pg
    public string classesLevel = "";
    public string races = "";
    public string alignment = "";
    public string divinity = "";
    public string age = "";
    public string gender = "";
    public float height = 0;
    public float weight = 0;
    public string eyes = "";
    public string hair = "";
    public string skin = "";

    //stats
    public int strenght = 0, strenghtModifier = 0;
    public int dexterity = 0, dexterityModifier = 0;
    public int consitution = 0, consitutionModifier = 0;
    public int intelligence = 0, intelligenceModifier = 0;
    public int wisdom = 0, wisdomModifier = 0;
    public int charisma = 0, charismaModifier = 0;

    //saving throws
    public int st_strenght = 0;
    public int st_dexterity = 0;
    public int st_constitutio = 0;
    public int st_intelligence = 0;
    public int st_wisdom = 0;
    public int st_charisma = 0;

    //hit points
    public int armorClass = 0;
    public int currentHitPoint = 0;
    public int temporaryHitPoint = 0;


    public int speed = 0;

    public Character() { }
    public Character(bool enable) { this.enable = enable; }

}

[System.Serializable]
public class Inventory
{
    public bool enable = false;

    public string[] list;

    public Inventory() { }
    public Inventory(bool enable) { this.enable = enable; }
}

[System.Serializable]
public class Equipment
{
    public bool enable = false;

    public string[] list;

    public Equipment() { }
    public Equipment(bool enable) { this.enable = enable; }
}

[System.Serializable]
public class Weapon
{
    public bool enable = false;

    public int numberOfDices = 0;
    public DiceType dice = DiceType.D4;
    public int rangeMin = 0;
    public int rangeMax = 0;
    public string description = "";

    public Weapon() { }
    public Weapon(bool enable) { this.enable = enable; }

}

[System.Serializable]
public class Armour
{

    public bool enable = false;

    public int resistance = 0;
    public string description = "";

    public Armour() { }
    public Armour(bool enable) { this.enable = enable; }

}

public enum WorldEntityType
{
    Character,
    Container,
    Weapon,
    Armour
}

public static class WorldEntityFabric
{

    public static WorldEntity Get(string name, string sprite, WorldEntityType worldEntityType)
    {
        switch (worldEntityType)
        {
            case WorldEntityType.Character:
                return Character(name, sprite);

            case WorldEntityType.Container:
                return Container(name, sprite);

            case WorldEntityType.Weapon:
                return Weapon(name, sprite);

            case WorldEntityType.Armour:
                return Armour(name, sprite);

            default:
                return new WorldEntity
                {
                    name = name,
                    description = "",
                    position = Vector3.zero,
                    spriteName = sprite,
                    character = new Character(),
                    equipment = new Equipment(),
                    inventory = new Inventory(),
                    armour = new Armour(),
                    weapon = new Weapon()
                };
        }
    }

    public static WorldEntity Character(string name, string sprite)
    {
        WorldEntity we = new WorldEntity
        {
            name = name,
            description = "",
            type = WorldEntityType.Character,
            position = Vector2.zero,
            spriteName = sprite,
            character = new Character(true),
            armour = new Armour(),
            equipment = new Equipment(true),
            inventory = new Inventory(true),
            weapon = new Weapon()
        };

        return we;
    }

    public static WorldEntity Weapon(string name, string sprite)
    {
        WorldEntity we = new WorldEntity
        {
            name = name,
            description = "",
            type = WorldEntityType.Weapon,
            position = Vector2.zero,
            spriteName = sprite,
            character = new Character(),
            armour = new Armour(),
            equipment = new Equipment(),
            inventory = new Inventory(),
            weapon = new Weapon(true)
        };

        return we;
    }

    public static WorldEntity Armour(string name, string sprite)
    {
        WorldEntity we = new WorldEntity
        {
            name = name,
            description = "",
            position = Vector2.zero,
            type = WorldEntityType.Armour,
            spriteName = sprite,
            character = new Character(),
            armour = new Armour(true),
            equipment = new Equipment(),
            inventory = new Inventory(),
            weapon = new Weapon()
        };

        return we;
    }

    public static WorldEntity Container(string name, string sprite)
    {
        WorldEntity we = new WorldEntity
        {
            name = name,
            description = "",
            type = WorldEntityType.Container,
            position = Vector2.zero,
            spriteName = sprite,
            character = new Character(),
            armour = new Armour(),
            equipment = new Equipment(),
            inventory = new Inventory(true),
            weapon = new Weapon()
        };

        return we;
    }
}