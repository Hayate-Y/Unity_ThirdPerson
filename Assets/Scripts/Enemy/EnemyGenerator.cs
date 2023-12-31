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

    // Start is called before the first frame update
    void Start()
    {
        foreach(var data in spawnData)
        {
            for(int i = 0; i < data.spawnNum; i++)
            {
                float x = Random.Range(spawnMinX, spawnMaxX);
                float y = 1f;
                float z = Random.Range(spawnMinZ, spawnMaxZ);

                var obj = Instantiate(data.prefab);
                obj.transform.position = new Vector3(x, y, z);

                var enemy = obj.GetComponent<EnemyBase>();
                enemy.SetParam(player);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
