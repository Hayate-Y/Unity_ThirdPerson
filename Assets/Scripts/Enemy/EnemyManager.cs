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

        //�m�b�N�o�b�N
        //�C�ӂ̐��������ɉ�����
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

    //�_���[�W�󂯂���
    void Damage(int damage)
    {
        //���i�q�b�g���Ȃ��悤�ɃR���C�_�[������
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
        //�U�����󂯂���𗬃��[�h����퓬���[�h��
        animator.SetTrigger("IsAttacked");
        if (!animator.GetBool("Attacked"))
        {
            animator.SetBool("Attacked", true);
        }
        enemyUIManager.UpdateHP(HP);
    }

    //�_���[�W���󂯂�����s����֐�
    private void OnTriggerEnter(Collider other)
    {
        
        Damager damager=other.GetComponent<Damager>();
        if(damager!=null)
        {
            KnockBackCount = 0;
            Damage(damager.damage);
        }
    }

    //�G�̕���������
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

    //�_���[�W�󂯂Ă�Ƃ��͍U�������܂�������悤�Ɍ�낾���ɂ����ړ��ł��Ȃ��悤�ɂ���
    public void FixZ()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
    }
    public void NoFixedZ()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
