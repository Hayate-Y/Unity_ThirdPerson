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
    protected EnemyUIManager enemyUIManager;

    [SerializeField]
    protected Collider EnemyCollider;

    protected PlayerManager playerManager;
    protected Rigidbody rb;
    protected Animator animator;

    public void SetParam(PlayerManager player)
    {
        target = player.transform;
        playerManager = player;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        HP = MaxHP;
        enemyUIManager.Init(this);
        rb = GetComponent<Rigidbody>();
    }
}
