using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<EnemyManager> wavs;
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private bool isWave=false;
    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWave && EnemyAllDead(wavs[currentWaveIndex]))
        {
            {
                Debug.Log("ウェーブ " + (currentWaveIndex + 1) + " が完了しました！");
                currentWaveIndex++;
                if (currentWaveIndex < wavs.Count)
                {
                    StartWave();
                }
                else
                {
                    Debug.Log("すべてのウェーブが終わりましたよー");
                    isWave = false;
                }
            }
        }
    }

    void StartWave()
    {
        if(currentWaveIndex< wavs.Count)
        {
            isWave = true;
            Debug.Log("ウェーブ " + (currentWaveIndex + 1) + " が開始されました！");
        }
    }

    bool EnemyAllDead(EnemyManager wave)
    {
        foreach(EnemyScript enemy in wave.GetEnemies())
        {
            if(enemy !=null)
            {
                return false;
                
            }
        }
        return true;
    }
}
