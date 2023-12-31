using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Transform target;
    protected int HP;

    [SerializeField]
    public int MaxHP = 100;

    [SerializeField]
    protected Collider EnemyCollider;

    [SerializeField]
    Transform HPDispPosition;

    protected PlayerManager playerManager;
    protected Rigidbody rb;
    protected Animator animator;
    protected EnemyUIManager enemyUIManager;
    protected RectTransform HPbar;

    // HPを表示する距離
    static float HPDispDistance = 20;

    public void SetParam(PlayerManager player, EnemyUIManager eui)
    {
        enemyUIManager = eui;
        target = player.transform;
        playerManager = player;
        enemyUIManager.Init(this);
        HPbar = enemyUIManager.GetComponent<RectTransform>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        HP = MaxHP;
        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        if(enemyUIManager != null)
        {
            Destroy(enemyUIManager.gameObject);
        }
    }

    protected void UpdateHPBar()
    {
        // HPバーを座標に合わせて移動させる
        if(enemyUIManager != null)
        {
            // 距離が近い場合
            if(Vector3.Distance(transform.position, target.position) < HPDispDistance)
            {
                enemyUIManager.gameObject.SetActive(true);

                // オブジェクトのワールド座標からスクリーン座標
                Vector3 offsetHPbar = new Vector3(
                    HPDispPosition.localPosition.x * HPDispPosition.lossyScale.x,
                    HPDispPosition.localPosition.y * HPDispPosition.lossyScale.y,
                    HPDispPosition.localPosition.z * HPDispPosition.lossyScale.z
                    );
                var screenPosition = Camera.main.WorldToScreenPoint(transform.position + offsetHPbar);

                // スクリーン座標をUI座標に変換
                Vector2 uiPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    enemyUIManager.transform.parent.GetComponent<RectTransform>(),
                    screenPosition,
                    null,
                    out uiPosition);

                HPbar.localPosition = uiPosition;
            }
            // 距離が遠い場合
            else
            {
                enemyUIManager.gameObject.SetActive(false);
            }
        }
    }
}
