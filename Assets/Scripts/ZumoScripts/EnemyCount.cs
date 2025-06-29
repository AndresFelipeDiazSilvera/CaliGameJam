using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public List<GameObject> enemiesActives = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int EnemysEnable()
    {
        int count = 0;
        foreach (GameObject prefab in enemiesActives)
        {
            if (prefab.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}
