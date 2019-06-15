
[System.Serializable]
public class Stats
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
    public int strenght = 0,        strenghtModifier = 0;
    public int dexterity = 0,       dexterityModifier = 0;
    public int consitution = 0,     consitutionModifier = 0;
    public int intelligence = 0,    intelligenceModifier = 0;
    public int wisdom = 0,          wisdomModifier = 0;
    public int charisma = 0,        charismaModifier = 0;

    //saving throws
    public int st_strenght = 0;
    public int st_dexterity = 0;
    public int st_constitutio = 0;
    public int st_intelligence = 0;
    public int st_wisdom = 0;
    public int st_charisma = 0;

    //hit points
    public int currentHitPoint = 0;
    public int temporaryHitPoint = 0;


    public int speed;

    public Stats() { }

}
