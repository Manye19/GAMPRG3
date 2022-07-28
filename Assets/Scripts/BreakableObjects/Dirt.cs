using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dirt : BreakableObject
{
    public Tilemap dirtTilemap;

    public TileBase plowedField;
    // Start is called before the first frame update
    void Start()
    {
        objName = "Dirt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit(GameObject tool, Transform interactor)
    {
        if (tool.GetComponent<Hoe>())
        {
            Debug.Log("hits!");
            dirtTilemap.SetTile(dirtTilemap.WorldToCell(interactor.position), plowedField);
        }
    }
}
