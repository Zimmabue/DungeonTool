using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Storage;

public class CatalogView : MonoBehaviour
{
    
    private enum BrushType
    {
        Ground,
        Item,
        Character,
        Weapon,
        Armour
    }

    private enum DrawType
    {
        Add,
        Remove
    }

    public Transform itemCatalogGridParent;
    public Transform characterCatalogGridParent;
    public Transform groundCatalogGridParent;
    public SpriteSlot spriteSlotPrefab;
    
    private World _world;
    private string _currentSelection = "";
    private BrushType brushType;
    private DrawType drawType;
    [SerializeField] private Camera _camera;

    // Use this for initialization
    void Start()
    {
        _world = World.Instance;
        _world.PrepareWorld();
        LoadCatalog();
    }

    public void DrawGround()
    {
        _currentSelection = "";
        drawType = DrawType.Add;
        brushType = BrushType.Ground;
    }

    public void DrawCharacter()
    {
        _currentSelection = "";
        drawType = DrawType.Add;
        brushType = BrushType.Character;
    }
    
    public void DrawWeapon()
    {
        _currentSelection = "";
        drawType = DrawType.Add;
        brushType = BrushType.Weapon;
    }

    public void DrawArmour()
    {
        _currentSelection = "";
        drawType = DrawType.Add;
        brushType = BrushType.Armour;
    }

    public void DrawItem()
    {
        _currentSelection = "";
        drawType = DrawType.Add;
        brushType = BrushType.Item;
    }

    public void SelectionModeOn()
    {
        drawType = DrawType.Add;
    }

    public void DeleteModeOn()
    {
        drawType = DrawType.Remove;
    }

    private void LoadCatalog()
    {
        
        var gCat = SpriteLoader.Instance.GetAllGround();
        foreach (var item in gCat)
        {
            var s = Instantiate(spriteSlotPrefab, groundCatalogGridParent);
            s.sprite = item;
            s.onClick = OnSlotClick;
        }
        
        var cCat = SpriteLoader.Instance.GetAllCharacters();
        foreach (var item in cCat)
        {
            var s = Instantiate(spriteSlotPrefab, characterCatalogGridParent);
            s.sprite = item;
            s.onClick = OnSlotClick;
        }
        
        var iCat = SpriteLoader.Instance.GetAllItems();
        foreach (var item in iCat)
        {
            var s = Instantiate(spriteSlotPrefab, itemCatalogGridParent);
            s.sprite = item;
            s.onClick = OnSlotClick;
        }
    }

    private void OnSlotClick(string name)
    {
        _currentSelection = name;
        Debug.Log(name);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentSelection == "")
                return;

            //prendi posizione da schermo
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            var position = _camera.ScreenToWorldPoint(mousePos);

            position.x = Mathf.Floor(position.x);
            position.y = Mathf.Floor(position.y);

            if (brushType == BrushType.Ground)
            {
                if (drawType == DrawType.Add)
                    _world.SetCellMap(_currentSelection, (int)position.x, (int)position.y);

                if (drawType == DrawType.Remove)
                    _world.SetCellMap("", (int)position.x, (int)position.y);
            }

            if(brushType == BrushType.Character)
            {
                if (drawType == DrawType.Add)
                    _world.CreateWorldEntity(_currentSelection, position, WorldEntityType.Character);

                if (drawType == DrawType.Remove)
                    _world.RemoveWorldEntity(_world.FindWorldEntity(position));
            }

            if (brushType == BrushType.Item)
            {
                if (drawType == DrawType.Add)
                    _world.CreateWorldEntity(_currentSelection, position, WorldEntityType.Container);

                if (drawType == DrawType.Remove)
                    _world.RemoveWorldEntity(_world.FindWorldEntity(position));
            }

            if (brushType == BrushType.Weapon)
            {
                if (drawType == DrawType.Add)
                    _world.CreateWorldEntity(_currentSelection, position, WorldEntityType.Weapon);

                if (drawType == DrawType.Remove)
                    _world.RemoveWorldEntity(_world.FindWorldEntity(position));
            }

            if (brushType == BrushType.Armour)
            {
                if (drawType == DrawType.Add)
                    _world.CreateWorldEntity(_currentSelection, position, WorldEntityType.Armour);

                if (drawType == DrawType.Remove)
                    _world.RemoveWorldEntity(_world.FindWorldEntity(position));
            }

        }
    }

    public void Save()
    {
        _world.SaveWorldMap();
    }

    public void Load()
    {
        _world.LoadWorldMap();
        _world.DrawGroundMap();
        _world.DrawWorldEntities();
    }
    
  /*  private void OnGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("ADD"))
            SelectionModeOn();

        if (GUILayout.Button("Remove"))
            DeleteModeOn();

        if (GUILayout.Button("Save"))
            _world.SaveWorldMap();

        if (GUILayout.Button("Load"))
        {
            _world.LoadWorldMap();
            _world.DrawGroundMap();
            _world.DrawWorldEntities();
        }

        if (GUILayout.Button("SpawnPoint"))
            ;

        GUILayout.EndVertical();
    }*/

}
