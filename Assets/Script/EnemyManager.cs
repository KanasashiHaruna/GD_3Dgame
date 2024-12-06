using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyPrefab;
    public List<Vector3> spawnPosition;
    private List<EnemyScript> enemies=new List<EnemyScript>();

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemySpawn()
    {
        for(int i = 0; i<enemyPrefab.Count; i++)
        {
            if (i < spawnPosition.Count)
            {
                GameObject enemy = Instantiate(enemyPrefab[i], spawnPosition[i],Quaternion.identity);
                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

                if (enemyScript != null)
                {
                    enemies.Add(enemyScript);
                    Debug.Log("ìGÇ™ê∂ê¨Ç≥ÇÍÇ‹ÇµÇΩ: " + enemyScript.name);
                }
            }
        }
    }


    public List<EnemyScript> GetEnemies()
    {
        return enemies;
    }
}
