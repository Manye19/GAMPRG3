using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : BreakableObject
{
    public CropsScriptableObject cropsScriptableObject;
    [SerializeField] int currentGrowProgress;
    [SerializeField] int daysWithoutBeingWatered;

    Health health;

    DirtTileData _tile;

    protected override void Awake()
    {
        TimeManager.instance.onDayChangingEvent.AddListener(GrowProgress);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = cropsScriptableObject.cropSprites[0];
        currentGrowProgress = 0;
        daysWithoutBeingWatered = 0;

        //gets the current tile that it is on from the Dictionary in the Dirt script
        _tile = Dirt.instance.getTileData(Dirt.instance.dirtTilemap.WorldToCell(this.gameObject.transform.position));
    }

    protected override void OnEnable()
    {

        health = GetComponent<Health>();
        health.onDeathEvent.AddListener(SpawnDrops);
    }

    protected override void OnDisable()
    {
        health = GetComponent<Health>();
        health.onDeathEvent.RemoveListener(SpawnDrops);
    }

    public void GrowProgress()
    {
        if(currentGrowProgress < cropsScriptableObject.growTime)
        {
            //checks if tile is watered for it to grow
            if(_tile != null && _tile.IsWatered == true)
            {
                currentGrowProgress++;
                gameObject.GetComponent<SpriteRenderer>().sprite = cropsScriptableObject.cropSprites[currentGrowProgress]; 
                resetWateredTile();
                daysWithoutBeingWatered = 0;
            }
            else
            {
                daysWithoutBeingWatered++;
                Debug.Log(_tile.Name + " Cannot grow due to not being watered");
                if(daysWithoutBeingWatered >= SVConstants.CROP_DAYS_WITHOUT_WATER_LIMIT)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    //resets tile to unwatered when the day ends
    public void resetWateredTile()
    {
        Dirt.instance.dirtTilemap.SetTile(Dirt.instance.dirtTilemap.WorldToCell(this.gameObject.transform.position), Dirt.instance.dirtPlowed);
        _tile.IsWatered = false;
    }

    public override void OnHit(GameObject tool, Transform interactorPos)
    {
        //Harvest Plant when mature
        if(tool.GetComponent<Pickaxe>())
        {
            if(currentGrowProgress >= cropsScriptableObject.growTime)
            {
                //Drop harvest
                health.onDeathEvent.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
