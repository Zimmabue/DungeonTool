using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Storage;

public class World : MonoBehaviour
{
    /*
     
        due layer
        1-ground
            matrice di stringhe
            ovvero i nomi delle sprite da applicare come terreno
        2-worldEntity
            lista di world entity
        
        come trasformare una sprite in worldEntity?
        cosa abbiamo?
        una lista di sprite ottenuta dallo SpriteLoader
        quindi: lista = SpriteLoader.Get
        questa lista di sprite verrà visualizzata nella UI in una griglia
        tramite il drag and drop la singola sprite può essere posizionata nella griglia del mondo
        cosa avviene quando la sprite viene rilasciata?
        deve creare un GameObject con uno SpiteRenderer che renderizza la sprite stessa

        inoltre deve aggiungere un nuovo WoldEntity nella lista/dizionario
        se dizionario<nome, WorldEntity> la chiave deve essere settata con un contatore intero

        Questo GameObject deve avere le seguenti proprietà:
        -possibilità di riposizionare tramite drag and drop
        -modificare le proprità del WorldEntity

        quindi potrebbe avere due Componenti: una per lo spostamento e una per le modifiche di WorldEntity
        ma lo spostamento agisce comunque sul WorldEntity in quanto ha l'attributo position
        quindi
        si potrebbe usare una Componente che fa entrambe le cose
        come?
        OnMouseDrag() ??? riposiziona il transform e aggiorna la posizione di worldEntity (pero al rilascio)
        cliccando sul GameObject si aggiorna la UI relativa alla WorldEntity selezionata e mostra i dati
        relativi alla wolrdEntity corrente
        
        ///////////////////////////
        ///world
        
        che funzionalità deve avere la classe world?
        conserave le informazioni relative alla mappa e oggetti
        deve avere funzioni di
        -aggiungi world entity
        -rimuovi we
        -cerca we

        inoltre deve gestire la mappa statica
        quindi modifica della matrice
        come deve essere instanziata la matrice?
        a vuoto? -> griglia di spite null -> sfondo con griglia

        deve essere NetworkBehaviour? forse no
        il GameServer ottiene la mappa dai file e i client la richiedono
        avendo la mappa in locale la devono disegnare, come?
        ai client/server serve la classe World o deve rimanere solo nella creazione mappa?

        questo perche se i client chiedono al server la mappa
        la faranno disegnare al GridManager
        mentre le informazioni relative alle worldEntity in-game si trovano nel GameServer

        World puo diventare il GridManager?
        si

        quindi aggiungere funzioni di Save and Load(? non lo fa il GameServer in-game?)
        ?si puo creare il DataLoader e il Save and Load di World non fa altro che chiamare il DataLoader?
        
        ////////////////
        ///cells

        come avviene la modifica delle celle?
        cos è la classe cella?
        due tipi:
        -ground
        -worldEntity

        ///tipo ground
        GameObject con Component e BoxCollider
        il selezionare le celle viene gestito da altro(World) che ottiene il Component
        e ne modifica la sprite --- Errato
        non puo modificarla semplicemente perche World contiene solo nomi di sprite
        
        Solo spriteRenderer? probabile di no
        Quindi non deve avere un Component specifico

        ///tipo worldEntity
        GameObject con Component che contiene riferimento di WorldEntity
        chiama il metodo ModifyGraphics(string name)

        //////////////////
        ///WorldDrawer

        modalità:
        -selezione
        -sposta
        -cancella

        Usa lo SpriteLoader e World
        Chiede allo SpriteLoader le immagini dei cataloghi
        Le disegna nei tre GridLayout
        Ogni cella del GridLayout quindi è un GameObject con uno SpriteRenderer e un Component
        
        Dilemma:
        Drag and Drop?
        Selezione e disegna? si

        Tiene conto della selezione corrente

        /////////////////////
        Ricapitolando il procedimento

        SpriteLoader -> carica i cataloghi di sprite
        
        WorldDrawer -> carica tramite lo SpriteLoader le sprite
        e le posiziona nelle tre griglie (di selezione)
        
        Utente -> clicca sull'immagine e la seleziona
        dopo di chè la puo usare per disegnare cliccando nel mondo

        World -> disegna e mantiene in memoria

    */

    public static World Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public SpriteRenderer groundPrefab;
    public EntityContainer entityContainerPrefab;
    
    private readonly static int SIZE = 100;     //world size

    private string[,] _ground;                  //ground layer
    private List<WorldEntity> _worldEntities;   //character/item layer
    private List<WorldEntity> _players;         //players

    private SpriteRenderer[,] _groundSpriteRenderer;
    private List<EntityContainer> _entityContainers;

    private void Start()
    {
        _ground = new string[SIZE, SIZE];
        
        _worldEntities = new List<WorldEntity>();
        _players = new List<WorldEntity>();
        _entityContainers = new List<EntityContainer>();
    }
    
    public void PrepareWorld()
    {
        InstantiateGroundMatrix();
    }

    private void InstantiateGroundMatrix()
    {
        _groundSpriteRenderer = new SpriteRenderer[SIZE, SIZE];
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                var cell = Instantiate(groundPrefab);
                cell.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                _groundSpriteRenderer[x, y] = cell;
            }
        }
        
    }

    public void DrawGroundMap()
    {

        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                _groundSpriteRenderer[x, y].sprite = SpriteLoader.Instance.GetSprite(_ground[x, y]);
            }
        }

    }

    public void DrawWorldEntities()
    {
        foreach (var we in _worldEntities)
        {
            EntityContainer ec = Instantiate(entityContainerPrefab);
            ec.worldEntity = we;
            ec.transform.position = new Vector3(we.position.x + 0.5f, we.position.y + 0.5f, -1);
            ec.GetComponent<SpriteRenderer>().sprite = SpriteLoader.Instance.GetSprite(we.spriteName);
            _entityContainers.Add(ec);
        }
    }

    public void SetCellMap(string name, int x, int y)
    {
        if (x < 0 || y < 0 || x >= SIZE || y >= SIZE)
            return;

        _ground[x, y] = name;
        _groundSpriteRenderer[x, y].sprite = SpriteLoader.Instance.GetSprite(name);
    }

    public void SetCellMap(string[,] map)
    {
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                SetCellMap(map[x, y], x, y);
            }
        }
    }
    
    public string[,] GetGroundMap()
    {
        return _ground;
    }
    
    public void CreateWorldEntity(string spriteName, Vector2 position, WorldEntityType worldEntityType)
    {
        if (position.x < 0 || position.y < 0 || position.x >= SIZE || position.y >= SIZE)
            return;

        if (FindWorldEntity(position) != null)
            return;

        WorldEntity we = WorldEntityFabric.Get(spriteName, spriteName, worldEntityType);
        we.position = position;
        _worldEntities.Add(we);
        
        EntityContainer ec = Instantiate(entityContainerPrefab);
        ec.worldEntity = we;
        ec.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, -1);
        _entityContainers.Add(ec);
    }

    public void AddWorldEntity(WorldEntity worldEntity)
    {
        _worldEntities.Add(worldEntity);
    }

    public void SetWorldEntities(List<WorldEntity> worldEntities)
    {
        _worldEntities = worldEntities;
    }

    public void SetWorldEntitiesAndDraw(List<WorldEntity> worldEntities)
    {
        _entityContainers.ForEach(x => Destroy(x.gameObject));
        _entityContainers.Clear();
        _worldEntities = worldEntities;
        DrawWorldEntities();
    }

    public List<WorldEntity> GetWorldEntities()
    {
        return _worldEntities;
    }

    public WorldEntity FindWorldEntity(string name)
    {
        return _worldEntities.Find(e => e.name == name);
    }

    public WorldEntity FindWorldEntity(Vector2 position)
    {
        return _worldEntities.Find(e => e.position == position);
    }

    public WorldEntity FindPlayer(Vector2 position)
    {
        return _players.Find(e => e.position == position);
    }
    
    public bool RemoveWorldEntity(WorldEntity worldEntity)
    {
        if (worldEntity == null)
            return false;

        var ec = _entityContainers.Find(x => x.worldEntity == worldEntity);
        _entityContainers.Remove(ec);
        Destroy(ec.gameObject);
        return _worldEntities.Remove(worldEntity);
    }

    public void RemovePlayer(WorldEntity player)
    {
        _players.Remove(player);
    }

    public List<WorldEntity> GetPlayers()
    {
        return _players;
    }

    public void SaveWorldMap()
    {
        var data = StorageManager.GetDataAccess();
        data.Save(Application.persistentDataPath + "/map01.dat", _ground);
        
        WESerializable[] wes = new WESerializable[_worldEntities.Count];
        for (int i = 0; i < _worldEntities.Count; i++)
        {
            wes[i] = new WESerializable
            {
                name = _worldEntities[i].name == "" ? _worldEntities[i].spriteName : _worldEntities[i].name,
                spriteName = _worldEntities[i].spriteName,
                type = _worldEntities[i].type,
                character = _worldEntities[i].character,
                inventory = _worldEntities[i].inventory,
                equipment = _worldEntities[i].equipment,
                weapon = _worldEntities[i].weapon,
                armour = _worldEntities[i].armour,
                description = _worldEntities[i].description,
                position = new float[2] { _worldEntities[i].position.x, _worldEntities[i].position.y }
            };
        }

        data.Save(Application.persistentDataPath + "/ent01.dat", wes);
        
    }

    public void LoadWorldMap()
    {
        var data = StorageManager.GetDataAccess();
        if (File.Exists(Application.persistentDataPath + "/map01.dat"))
        {
            _ground = data.Load<string[,]>(Application.persistentDataPath + "/map01.dat");
        }

        if(File.Exists(Application.persistentDataPath + "/ent01.dat"))
        {
            var wes = data.Load<WESerializable[]>(Application.persistentDataPath + "/ent01.dat");
            for (int i = 0; i < wes.Length; i++)
            {
                _worldEntities.Add
                    (
                    new WorldEntity
                    {
                        name = wes[i].name,
                        spriteName = wes[i].spriteName,
                        type = wes[i].type,
                        character = wes[i].character,
                        equipment = wes[i].equipment,
                        inventory = wes[i].inventory,
                        weapon = wes[i].weapon,
                        armour = wes[i].armour,
                        description = wes[i].description,
                        position = new Vector2(wes[i].position[0], wes[i].position[1])
                    }
                    );
            }
        }
        
    }

}