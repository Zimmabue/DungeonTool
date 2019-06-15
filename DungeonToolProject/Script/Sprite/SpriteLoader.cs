using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Storage;

public class SpriteLoader
{

    private static SpriteLoader _instance = null;
    public static SpriteLoader Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SpriteLoader();
            return _instance;
        }
    }

    [XmlRoot("Catalog")]
    public struct SpriteCatalogXML
    {
        [XmlElement("FileName")]
        public string fileName;

        [XmlElement("SpriteSize")]
        public int spriteSize;

        [XmlArray("List"), XmlArrayItem("Sprite")]
        public List<SpriteInfoXML> sprites;
    }

    public struct SpriteInfoXML
    {
        [XmlAttribute("name")] public string name;
        [XmlAttribute("x")] public int x;
        [XmlAttribute("y")] public int y;
    }
    
    private IXmlAccess xmlAccess;
    
    private Dictionary<string, Sprite> _itemsCatalog;
    private Dictionary<string, Sprite> _characterCatalog;
    private Dictionary<string, Sprite> _groundCatalog;

    private SpriteLoader()
    {
        _itemsCatalog = new Dictionary<string, Sprite>();
        _characterCatalog = new Dictionary<string, Sprite>();
        _groundCatalog = new Dictionary<string, Sprite>();
        xmlAccess = StorageManager.GetXmlAccess();
        
        LoadCatalog("items", ref _itemsCatalog);
        LoadCatalog("ground", ref _groundCatalog);
        LoadCatalog("character", ref _characterCatalog);
    }

    public Sprite GetSprite(string name)
    {
        if (name == null)
            return null;

        if (_groundCatalog.ContainsKey(name))
            return _groundCatalog[name];

        if (_characterCatalog.ContainsKey(name))
            return _characterCatalog[name];

        if (_itemsCatalog.ContainsKey(name))
            return _itemsCatalog[name];

        return null;
    }

    public Sprite GetGround(string name)
    {
        Sprite sprite;
        if (_groundCatalog.TryGetValue(name, out sprite))
        {
            return sprite;
        }
        return null;
    }

    public Sprite GetCharacter(string name)
    {
        Sprite sprite;
        if (_characterCatalog.TryGetValue(name, out sprite))
        {
            return sprite;
        }
        return null;
    }

    public Sprite GetItem(string name)
    {
        Sprite sprite;
        if(_itemsCatalog.TryGetValue(name, out sprite))
        {
            return sprite;
        }
        return null;
    }

    public Sprite[] GetAllGround()
    {
        Sprite[] sprites = new Sprite[_groundCatalog.Count];
        var e = _groundCatalog.GetEnumerator();
        int i = 0;
        while (e.MoveNext())
        {
            sprites[i] = e.Current.Value;
            i++;
        }
        return sprites;
    }

    public Sprite[] GetAllCharacters()
    {
        Sprite[] sprites = new Sprite[_characterCatalog.Count];
        var e = _characterCatalog.GetEnumerator();
        int i = 0;
        while (e.MoveNext())
        {
            sprites[i] = e.Current.Value;
            i++;
        }
        return sprites;
    }

    public Sprite[] GetAllItems()
    {
        Sprite[] sprites = new Sprite[_itemsCatalog.Count];
        var e = _itemsCatalog.GetEnumerator();
        int i = 0;
        while (e.MoveNext())
        {
            sprites[i] = e.Current.Value;
            i++;
        }
        return sprites;
    }

    private void LoadCatalog(string fileName, ref Dictionary<string, Sprite> catalog)
    {
        string defaultPath = Application.streamingAssetsPath;
        string matchPath = Application.persistentDataPath;

        string path = File.Exists(matchPath + "/" + fileName + ".xml") ? matchPath : defaultPath;

        var iXML = xmlAccess.Load<SpriteCatalogXML>(path + "/" + fileName + ".xml");
        var iTex = LoadImage(path + "/" + iXML.fileName);
        TextureToSprite(iTex, iXML.sprites, iXML.spriteSize, ref catalog);
    }

    private Texture2D LoadImage(string path)
    {
        byte[] byteArray = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.filterMode = FilterMode.Point;
        if (!texture.LoadImage(byteArray))
        {
            Debug.LogError("SpriteLoader::Load image falied!");
        }

        return texture;
    }
    
    private void TextureToSprite
        (Texture2D texture, List<SpriteInfoXML> infos, int size, ref Dictionary<string, Sprite> container)
    {
        foreach (var info in infos)
        {
            Rect position = new Rect(info.x * size, info.y * size, size, size);
            Sprite sprite = Sprite.Create(texture, position, new Vector2(0.5f, 0.5f), size);
            sprite.name = info.name;
            container.Add(info.name, sprite);
        }
    }
    
}
