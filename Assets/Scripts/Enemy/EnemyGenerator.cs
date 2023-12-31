using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class EnemySpawnData
{
    public GameObject prefab;   // �G�v���n�u
    public int spawnNum;        // �X�|�[�������鐔
}

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    List<EnemySpawnData> spawnData;

    [SerializeField, Tooltip("�ŏ���X���W")]
    float spawnMinX;

    [SerializeField, Tooltip("�ŏ���Z���W")]
    float spawnMinZ;

    [SerializeField, Tooltip("�ő��X���W")]
    float spawnMaxX;

    [SerializeField, Tooltip("�ő��Z���W")]
    float spawnMaxZ;

    [SerializeField]
    PlayerManager player;

    [SerializeField]
    GameObject HPBarPrefab;

    [SerializeField]
    Transform HPBarParent;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var data in spawnData)
        {
            for(int i = 0; i < data.spawnNum; i++)
            {
                // ���W�̐���
                float x = Random.Range(spawnMinX, spawnMaxX);
                float y = 1f;
                float z = Random.Range(spawnMinZ, spawnMaxZ);

                // �G�I�u�W�F�N�g�̐����A���W�ݒ�
                var obj = Instantiate(data.prefab);
                obj.transform.position = new Vector3(x, y, z);

                // �GHP�o�[�̐���
                var hpBarObj = Instantiate(HPBarPrefab, HPBarParent);
                var hpBar = hpBarObj.GetComponent<EnemyUIManager>();

                var enemy = obj.GetComponent<EnemyBase>();
                enemy.SetParam(player, hpBar);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
