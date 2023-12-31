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

    // HP��\�����鋗��
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
        // HP�o�[�����W�ɍ��킹�Ĉړ�������
        if(enemyUIManager != null)
        {
            // �������߂��ꍇ
            if(Vector3.Distance(transform.position, target.position) < HPDispDistance)
            {
                enemyUIManager.gameObject.SetActive(true);

                // �I�u�W�F�N�g�̃��[���h���W����X�N���[�����W
                Vector3 offsetHPbar = new Vector3(
                    HPDispPosition.localPosition.x * HPDispPosition.lossyScale.x,
                    HPDispPosition.localPosition.y * HPDispPosition.lossyScale.y,
                    HPDispPosition.localPosition.z * HPDispPosition.lossyScale.z
                    );
                var screenPosition = Camera.main.WorldToScreenPoint(transform.position + offsetHPbar);

                // �X�N���[�����W��UI���W�ɕϊ�
                Vector2 uiPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    enemyUIManager.transform.parent.GetComponent<RectTransform>(),
                    screenPosition,
                    null,
                    out uiPosition);

                HPbar.localPosition = uiPosition;
            }
            // �����������ꍇ
            else
            {
                enemyUIManager.gameObject.SetActive(false);
            }
        }
    }
}
