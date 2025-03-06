using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeManager : MonoBehaviour
{
    public LevelPool levelPool;
    public Transform Player;
    private List<GameObject> activeLevels = new List<GameObject>();
    public GameObject startLevel;
    private float nextSpawnY = 38.4f;
    private int maxLevelsOnScreen = 3;

    private void Start()
    {
        activeLevels.Add(startLevel);
        for (int i = 0; i < maxLevelsOnScreen - 1 ; i++)
        {
            SpawnNextLevel();
        }
    }
    private void Update()
    {
        if (Player.position.y > activeLevels[1].transform.position.y)
        {
            SpawnNextLevel();
            RemoveOldestLevel();
        }
    }
    void SpawnNextLevel()
    {
        GameObject newLevel = levelPool.GetRandomLevel();
        newLevel.transform.position = new Vector2(0,nextSpawnY);
        nextSpawnY += 38.4f;
        activeLevels.Add(newLevel);
    }
    void RemoveOldestLevel()
    {
        if(activeLevels.Contains(startLevel))
        {
            activeLevels.Remove(startLevel);
            startLevel.SetActive(false);
        }
        else if (activeLevels.Count > maxLevelsOnScreen)
        {
            GameObject oldLevel = activeLevels[0];
            levelPool.ReturnLevel(oldLevel);
            activeLevels.RemoveAt(0);
        }
    }
}
