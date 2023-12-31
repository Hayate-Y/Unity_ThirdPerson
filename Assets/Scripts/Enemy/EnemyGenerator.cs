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


    //public int[] enemycount; //�G�̐�
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
                // ���W�̐���
                float x = Random.Range(spawnMinX, spawnMaxX);
                float y = 1f;
                float z = Random.Range(spawnMinZ, spawnMaxZ);

                // �G�I�u�W�F�N�g�̐����A���W�ݒ�
                var obj = Instantiate(data.prefab);
                obj.transform.position = new Vector3(x, y, z);
                //enemycount[j]++;

                // �GHP�o�[�̐���
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

            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
        }
        else
        {
            time += Time.deltaTime;
        }
         
    }

    public void SpawnEnemy(int i)
    {
        // ���W�̐���
        float x = Random.Range(spawnMinX, spawnMaxX);
        float y = 1f;
        float z = Random.Range(spawnMinZ, spawnMaxZ);

        // �G�I�u�W�F�N�g�̐����A���W�ݒ�
        var obj = Instantiate(spawnData[i].prefab);
        obj.transform.position = new Vector3(x, y, z);

        // �GHP�o�[�̐���
        var hpBarObj = Instantiate(HPBarPrefab, HPBarParent);
        var hpBar = hpBarObj.GetComponent<EnemyUIManager>();

        var enemy = obj.GetComponent<EnemyBase>();
        enemy.SetParam(player, hpBar);
    }
}
