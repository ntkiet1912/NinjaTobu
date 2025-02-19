using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPool : MonoBehaviour
{
    [Header("Settings")]
    public List<GameObject> levelPrefabs;
    private Queue<GameObject> levelPool = new Queue<GameObject>();
    private List<GameObject> activeLevels = new List<GameObject>();
    private int lastLevelIndex = -1;
    private void Start()
    {
        List<GameObject> shuffledLevels = new List<GameObject>(levelPrefabs);
        ShuffleList(shuffledLevels);

        foreach (var prefab in shuffledLevels)
        {
            GameObject level = Instantiate(prefab);
            level.SetActive(false);
            levelPool.Enqueue(level);
        }
    }
    public GameObject GetRandomLevel()
    {
        if (levelPool.Count == 0)
        {
            return Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Count)]);
        }

        GameObject level = levelPool.Peek(); 

        while (activeLevels.Contains(level) && levelPool.Count > 1)
        {
            levelPool.Dequeue(); 
            levelPool.Enqueue(level); 
            level = levelPool.Peek(); 
        }

        levelPool.Dequeue(); 
        level.SetActive(true);
        activeLevels.Add(level);
        return level;
    }

    public void ReturnLevel(GameObject level)
    {
        level.SetActive(false);
        activeLevels.Remove(level);
        levelPool.Enqueue(level);
    }
    private void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            GameObject temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
