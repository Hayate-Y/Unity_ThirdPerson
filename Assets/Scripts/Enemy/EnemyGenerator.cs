using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
class EnemySpawnData
{
    public GameObject prefab;   // 敵プレハブ
    public int spawnNum;        // スポーンさせる数
}

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    List<EnemySpawnData> spawnData;

    [SerializeField, Tooltip("最小のX座標")]
    float spawnMinX;

    [SerializeField, Tooltip("最小のZ座標")]
    float spawnMinZ;

    [SerializeField, Tooltip("最大のX座標")]
    float spawnMaxX;

    [SerializeField, Tooltip("最大のZ座標")]
    float spawnMaxZ;

    [SerializeField]
    PlayerManager player;

    [SerializeField]
    GameObject HPBarPrefab;

    [SerializeField]
    Transform HPBarParent;


    //public int[] enemycount; //敵の数
    float time = 0;
    float interval = 5f;
    int WhichEnemy = 0;

    // Start is called before the first frame update
    void Start()
    {
        //enemycount = new int[spawnData.Count];
        int j = 0;

        foreach(var data in spawnData)
        {
            for(int i = 0; i < data.spawnNum; i++)
            {
                // 座標の生成
                float x = Random.Range(spawnMinX, spawnMaxX);
                float y = 1f;
                float z = Random.Range(spawnMinZ, spawnMaxZ);

                // 敵オブジェクトの生成、座標設定
                var obj = Instantiate(data.prefab);
                obj.transform.position = new Vector3(x, y, z);
                //enemycount[j]++;

                // 敵HPバーの生成
                var hpBarObj = Instantiate(HPBarPrefab, HPBarParent);
                var hpBar = hpBarObj.GetComponent<EnemyUIManager>();

                var enemy = obj.GetComponent<EnemyBase>();
                enemy.SetParam(player, hpBar);
            }
            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time > interval)
        {
            SpawnEnemy(WhichEnemy);
            WhichEnemy = (WhichEnemy + 1) % 2;

            //経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
        else
        {
            time += Time.deltaTime;
        }
         
    }

    public void SpawnEnemy(int i)
    {
        // 座標の生成
        float x = Random.Range(spawnMinX, spawnMaxX);
        float y = 1f;
        float z = Random.Range(spawnMinZ, spawnMaxZ);

        // 敵オブジェクトの生成、座標設定
        var obj = Instantiate(spawnData[i].prefab);
        obj.transform.position = new Vector3(x, y, z);

        // 敵HPバーの生成
        var hpBarObj = Instantiate(HPBarPrefab, HPBarParent);
        var hpBar = hpBarObj.GetComponent<EnemyUIManager>();

        var enemy = obj.GetComponent<EnemyBase>();
        enemy.SetParam(player, hpBar);
    }
}
