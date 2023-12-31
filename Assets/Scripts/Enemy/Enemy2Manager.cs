using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy2Manager : EnemyBase
{
    public float movespeed;
    Vector3 OldVelocity=Vector3.zero;
    public ParticleSystem RollEffect;
    public Collider Enemy2Collider;
    public GameObject FireBoll;
    float distance;
    bool ChangeAttack;//�U����؂�ւ���邩�ǂ���
    bool IsLook = true;
    public ParticleSystem ChageEffect;
    public ParticleSystem BulletEffect;
    int Interval = 60;
    int IntervalCount;


    //�U�����[�h
    int AttackMode = 0;
    //�U�����[�h0
    bool Move=true;//�ړ��ł��邩�ǂ���
    //�U�����[�h1
    

    // Start is called before the first frame update
    void Start()
    {
        ChageEffect.Stop();
        BulletEffect.Stop();
        RollEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < 20)
        {
            switch (AttackMode)
            {
                case 0:
                    RollAttackUpdate();
                    IntervalCount++;
                    break;
                case 1:
                    FireAttack();
                    break;
            }

            if (distance > 5 && AttackMode == 0 && IntervalCount > Interval)
            {
                AttackMode = 1;
                FireAttackStart();
                ChangeAttack = false;
            }
            if (AttackMode == 1 && ChangeAttack)
            {
                AttackMode = 0;
                IntervalCount = 0;
            }
        }
        else
        {
            distance = (float)System.Math.Sqrt((target.position.x - transform.position.x) * (target.position.x - transform.position.x) + (target.position.z - transform.position.z) * (target.position.z - transform.position.z));
            animator.SetFloat("Distance", distance);
        }
        
    }

    //�U���p�^�[���P-----------------------------------------------------------------------
    void RollAttackUpdate()
    {
        if (HP > 0 && Move)
        {
            LookAtTarget();
            distance = (float)System.Math.Sqrt((target.position.x - transform.position.x) * (target.position.x - transform.position.x) + (target.position.z - transform.position.z) * (target.position.z - transform.position.z));
            animator.SetFloat("Distance", distance);
            if (distance < 2)
            {

            }
            else
            {
                float x = transform.forward.x * movespeed;
                float z = transform.forward.z * movespeed;
                Vector3 Velocity = new Vector3(x, 0, z);
                transform.position = new Vector3(transform.position.x + x, 1, transform.position.z + z);//Vector3.Lerp(OldVelocity, Velocity, 0.2f);
                OldVelocity = Velocity;
            }
        }
    }

    //�U���p�^�[���Q---------------------------------------------------------------------------------------------------
    void FireAttack()
    {
        if (HP > 0)
        {
            if (IsLook)
            {
                LookAtTarget();
            }
            distance = (float)System.Math.Sqrt((target.position.x - transform.position.x) * (target.position.x - transform.position.x) + (target.position.z - transform.position.z) * (target.position.z - transform.position.z));
        }
    }

   

    void FireAttackStart()
    {
        animator.SetTrigger("Fire");
        AttackMode = 1;
    }
    

    //�G�̕���������
    public void LookAtTarget()
    {
        Vector3 retarget=new Vector3 (target.position.x, transform.position.y, target.position.z);
        transform.LookAt(retarget);
    }

    //�_���[�W�󂯂���
    void Damage(int damage)
    {
        playerManager.OnAttackHit();

        

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            animator.SetTrigger("Die");
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            Destroy(gameObject, 5f);
        }
        
        enemyUIManager.UpdateHP(HP);
    }

    //�_���[�W���󂯂�����s����֐�
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            Damage(damager.damage);
            animator.SetTrigger("Hurt");
        }
    }

    //�A�C�h���ɂȂ�����g���K�[�����Z�b�g����
    public void IdleStart()
    {
        animator.ResetTrigger("Hurt");
    }

    
    //�R���W�����o�������������
    //�U�����͈ړ����Ȃ�
    public void HideCollider()
    {
        Enemy2Collider.enabled = false;
        Move = true;
    }
    public void ShowCollider()
    {
        Enemy2Collider.enabled = true;
        Move = false;
    }
    public void ShowFire()
    {
        Vector3 pos=new Vector3(target.position.x , target.position.y+1 , target.position.z);
        var dir = (pos - transform.position).normalized;
        GameObject Bullet = Instantiate(FireBoll, transform.position+dir*2, transform.rotation);
        Bullet.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Bullet.GetComponent<Rigidbody>().AddForce(dir * 20f, ForceMode.Impulse);

    }

    //�U����؂�ւ���邩�ǂ���
    public void CanChangeAttack()
    {
        ChangeAttack = true;
    }
    public void NoChangeAttack()
    {
        ChangeAttack = false;
    }

    //�G�̕����������ǂ���
    public void YesLook()
    {
        IsLook = true;
    }
    public void NoLook()
    {
        IsLook = false;
    }

    //�t�@�C�A�{�[���̃`���[�W
    public void ChargeStart()
    {
        ChageEffect.Play();
        BulletEffect.Play();
    }
    public void ChargeFinish()
    {
        ChageEffect.Stop();
        BulletEffect.Stop();
    }

    //��]���̃G�t�F�N�g
    public void ShowRollEffect()
    {
        RollEffect.Play();
    }
    public void HideRollEffect()
    {
        RollEffect.Stop();
    }
}
