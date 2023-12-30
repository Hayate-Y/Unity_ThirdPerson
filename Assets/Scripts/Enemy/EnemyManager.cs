using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    int HP;
    public int MaxHP = 100;
    public EnemyUIManager enemyUIManager;
    int KnockBackTotal = 10;
    int KnockBackCount=11;
    public Collider EnemyCollider;
    public PlayerManager playerManager;
    int Interval = 120;
    int IntervalCount;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Distance", 10);
        HP = MaxHP;
        enemyUIManager.Init(this);
        IntervalCount = 0;
        rb= GetComponent<Rigidbody>();
        //EnemyCollider= GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance",agent.remainingDistance);
        LookAtTarget();

        //ノックバック
        //任意の数だけ後ろに下がる
        if(KnockBackCount <= KnockBackTotal)
        {
            var TargetDir = (target.position - transform.position).normalized;
            this.transform.position += new Vector3(-TargetDir.x * 0.05f, 0, -TargetDir.z * 0.05f);
            KnockBackCount++;
        }

        if(IntervalCount>=Interval)
        {
            IntervalCount = Interval;
            animator.SetBool("Interval", false);
        }
        else
        {
            animator.SetBool("Interval", true);
            IntervalCount++;
        }
    }

    //ダメージ受けたら
    void Damage(int damage)
    {
        //多段ヒットしないようにコライダーを消す
        playerManager.OnAttackHit();
        playerManager.HideColliderOnly();
        playerManager.HideGardCollider();

        IntervalCount = 0;


        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            animator.SetTrigger("Die");
            Destroy(gameObject, 5f);

        }
        //攻撃を受けたら交流モードから戦闘モードへ
        animator.SetTrigger("IsAttacked");
        if (!animator.GetBool("Attacked"))
        {
            animator.SetBool("Attacked", true);
        }
        enemyUIManager.UpdateHP(HP);
    }

    //ダメージを受けたら実行する関数
    private void OnTriggerEnter(Collider other)
    {
        
        Damager damager=other.GetComponent<Damager>();
        if(damager!=null)
        {
            KnockBackCount = 0;
            Damage(damager.damage);
        }
    }

    //敵の方向を向く
    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void AttackStart()
    {
        EnemyCollider.enabled = true;
        IntervalCount = 0;
    }

    public void AttackEnd()
    {
        EnemyCollider.enabled = false;
    }

    //ダメージ受けてるときは攻撃がうまく当たるように後ろだけにしか移動できないようにする
    public void FixZ()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
    }
    public void NoFixedZ()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
