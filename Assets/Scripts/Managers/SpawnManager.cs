using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int currentRoomID;
    private List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnPoints;

    private IEnumerator runningCoroutine;

    private void OnEnable()
    {
        TimeManager.onHourChangedEvent.AddListener(Spawn);
        TimeManager.instance.onDayChangingEvent.AddListener(DestroyAll);
        PlayerManager.onUpdateCurrentRoomIDEvent.AddListener(OnRoomChange);
    }
    private void OnDisable()
    {
        TimeManager.onHourChangedEvent.RemoveListener(Spawn);
        TimeManager.instance.onDayChangingEvent.RemoveListener(DestroyAll);
        PlayerManager.onUpdateCurrentRoomIDEvent.RemoveListener(OnRoomChange);
    }

    private void Spawn(int p_hour)
    { 
        if (p_hour == SVConstants.NIGHT_TIME && currentRoomID != 0)
        {
            //Debug.Log("Spawning at " + p_hour);
            runningCoroutine = SpawnCoroutine();
            StartCoroutine(runningCoroutine);
        }
        else
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                runningCoroutine = null;
            }
        }
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(GetRand(3f, 6f));
        //Debug.Log("Spawned!");
        GameObject newEnemy = Instantiate(enemyPrefabs[(int)GetRand(0, enemyPrefabs.Count)], spawnPoints[(int)GetRand(0, spawnPoints.Count)]);
        enemyList.Add(newEnemy);
    }

    private void DestroyAll()
    {
        if (enemyList.Count > 0)
        {
            foreach (GameObject gameObjects in enemyList)
            {
                Destroy(gameObjects);
            }
            enemyList.Clear();
        }
    }

    private void OnRoomChange(int p_index)
    {
        currentRoomID = p_index;
        if (currentRoomID == 0)
        {
            DestroyAll();
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                runningCoroutine = null;
            }
        }       
    }

public float GetRand(float p_min, float p_max)
    {
        float rand = Random.Range(p_min, p_max);
        return rand;
    }
}
