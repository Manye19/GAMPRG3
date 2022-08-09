using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dirt : BreakableObject
{
    public static Dirt instance;
    public Tilemap dirtTilemap;

    public TileBase dirtPlowed;
    public TileBase dirtWatered;

    public Dictionary<Vector3, DirtTileData> tiles;
    private DirtTileData _tile;

    protected override void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GetDirtTiles();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        objName = "Dirt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDirtTiles()
    {
        Debug.Log("Tiles Initialized");
        tiles = new Dictionary<Vector3, DirtTileData>();

        foreach(Vector3Int pos in dirtTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, 0);

            if(!dirtTilemap.HasTile(localPlace))
            {
                continue;
            }

            DirtTileData tile = new DirtTileData
            {
                LocalPlace = localPlace,
                WorldLocation = dirtTilemap.CellToWorld(localPlace),
                TileBase = dirtTilemap.GetTile(localPlace),
                TilemapMember = dirtTilemap,

                Name = localPlace.x + ", " + localPlace.y,
                IsPlowed = false,
                IsWatered = false,
                IsOccupied = false
            };

            tiles.Add(tile.WorldLocation, tile);
        }
    }

    public DirtTileData getTileData(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out _tile))
        {
            return _tile;
        }
        else
        {
            Debug.Log("Cannot Find Tile");
        }
        return null;
    }

    public override void OnHit(GameObject tool, Transform interactor)
    {
        Vector3Int dirtTilePos = dirtTilemap.WorldToCell(interactor.position);
        
        //plows dirt
        if (tool.GetComponent<Hoe>())
        {
            if (tiles.TryGetValue(dirtTilePos, out _tile))
            {
                if (_tile.IsPlowed == false)
                {
                    _tile.IsPlowed = true;
                    Debug.Log("Tile" + dirtTilePos + " plowed"); 
                    dirtTilemap.SetTile(dirtTilePos, dirtPlowed);
                }
            }
        }

        //waters plowed dirt
        if(tool.GetComponent<WateringCan>())
        {
            if (tiles.TryGetValue(dirtTilePos, out _tile))
            {
                if (_tile.IsWatered == false && _tile.IsPlowed == true)
                {
                    _tile.IsWatered = true;
                    Debug.Log("Tile" + dirtTilePos + " watered");
                    dirtTilemap.SetTile(dirtTilemap.WorldToCell(interactor.position), dirtWatered);
                }
            }
        }
        
        //plants seed if dirt is plowed
        if(tool.GetComponent<ParsnipSeed>())
        {
            if (tiles.TryGetValue(dirtTilePos, out _tile))
            {
                if (_tile.IsOccupied == false && _tile.IsPlowed == true)
                {
                    _tile.IsOccupied = true;
                    Vector3 plantingLocation = dirtTilePos + new Vector3(0.5f, 0.5f, 0);
                    Instantiate(tool.GetComponent<ParsnipSeed>().parsnipPlant, plantingLocation, Quaternion.identity);
                }
            }
        }
    }
}
