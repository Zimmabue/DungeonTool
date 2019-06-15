using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class WorldServer : NetworkBehaviour
{

    public static WorldServer Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GroundNetwork groundNetworkPrefab;
    public WorldEntityNetwork entityNetworkPrefab;

    private List<WorldEntityNetwork> playerNetwork;
    private List<WorldEntityNetwork> _allWorldEntityNetwork;
    private World _world;

    // Use this for initialization
    void Start()
    {
        if (!isServer)
            return;

        _allWorldEntityNetwork = new List<WorldEntityNetwork>();
        _world = World.Instance;
        _world.LoadWorldMap();
        SpawnGroundMap();
        SpawnWorldEntities();
    }
    
    public void Save()
    {
        List<WorldEntity> we = new List<WorldEntity>();
        _allWorldEntityNetwork.ForEach(x => we.Add(x.worldEntity));
        _world.SetWorldEntities(we);
        _world.SaveWorldMap();
    }

    public WorldEntityNetwork GetWorldEntityNetwork(string entityName)
    {
        return playerNetwork.Find(x => x.worldEntity.name == entityName);
    }

    public void SpawnWorldEntity(WorldEntity entity)
    {
        _world.AddWorldEntity(entity);
        WorldEntityNetwork we = Instantiate(entityNetworkPrefab);
        we.worldEntity = entity;
        we.transform.position = new Vector3(entity.position.x + 0.5f, entity.position.y + 0.5f, -0.1f);

        _allWorldEntityNetwork.Add(we);
        NetworkServer.Spawn(we.gameObject);
    }

    private void SpawnGroundMap()
    {
        var ground = _world.GetGroundMap();
        for (int y = 0; y < ground.GetLength(1); y++)
        {
            for (int x = 0; x < ground.GetLength(0); x++)
            {
                GroundNetwork gn = Instantiate(groundNetworkPrefab);
                gn.spriteName = ground[x, y];
                gn.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                
                NetworkServer.Spawn(gn.gameObject);
            }
        }
    }
    
    private void SpawnWorldEntities()
    {
        var entities = _world.GetWorldEntities();
        foreach (var e in entities)
        {
            WorldEntityNetwork we = Instantiate(entityNetworkPrefab);
            we.worldEntity = e;
            we.transform.position = new Vector3(e.position.x + 0.5f, e.position.y + 0.5f, -0.1f);

            _allWorldEntityNetwork.Add(we);
            NetworkServer.Spawn(we.gameObject);
        }
    }

    private void SpawnPlayers()
    {
        playerNetwork = new List<WorldEntityNetwork>();

        foreach (var player in _world.GetPlayers())
        {
            WorldEntityNetwork netPlayer = Instantiate(entityNetworkPrefab);
            netPlayer.worldEntity = player;
            netPlayer.transform.position = new Vector3(player.position.x + 0.5f, player.position.y + 0.5f, -0.1f);

            playerNetwork.Add(netPlayer);
            NetworkServer.Spawn(netPlayer.gameObject);
        }
    }

    private void SpawnPlayersOld()
    {
        playerNetwork = new List<WorldEntityNetwork>();

        var pl1 = WorldEntityFabric.Character("zimmabue", "char01");
        var pl2 = WorldEntityFabric.Character("uriel", "char02");

        WorldEntityNetwork we1 = Instantiate(entityNetworkPrefab);
        we1.worldEntity = pl1;
        we1.transform.position = new Vector3(pl1.position.x + 0.5f, pl1.position.y + 0.5f, -0.1f);

        playerNetwork.Add(we1);

        NetworkServer.Spawn(we1.gameObject);

        WorldEntityNetwork we2 = Instantiate(entityNetworkPrefab);
        we2.worldEntity = pl2;
        we2.transform.position = new Vector3(pl2.position.x + 0.5f, pl2.position.y + 0.5f, -0.1f);

        playerNetwork.Add(we2);

        NetworkServer.Spawn(we2.gameObject);
    }

}
