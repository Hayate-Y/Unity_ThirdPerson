using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//操作
//Space:攻撃　C:ガード
//
public class PlayerManager : MonoBehaviour
{
    Animator animator;
    bool Holding_Combat;
    public float movespeed = 2;
    float x, z;
    Rigidbody rb;
    bool RunAttackMove;
    bool ReRunAttackPosition;
    Quaternion targetRotation;
    public Collider WeaponCollider;
    public int MaxHP=100;
    int hp;
    public PlayerUIManager PlayerUIManager;
    public Collider GardCollider;
    public ParticleSystem ParticleSystem;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    Collider CapsuelCollider;
    bool IsSuperTime;//今無敵時間かどうか
    int SuperTimeCount = 0;
    public float HitStopTime = 0.2f;
    public ParticleSystem HitEffect;
    public Transform enemy1position;
    public Transform enemy2position;

    void Start()
    {
        animator=GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        HideColliderWeapon();
        HideGardCollider();
        hp = MaxHP;
        CapsuelCollider = GetComponent<Collider>();
        HitEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃アニメーション
        Combat_Action();
        //ガードアニメーション
        BlockAnimation();
        //移動
        Run();

        //無敵時間か
        SuperTime();

        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }


    }

    void Combat_Action()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //敵が近くなったらその方向を向く
            if (enemy1position != null)
            {
                float x1 = enemy1position.position.x;
                float z1 = enemy1position.position.z;
                float x2 = transform.position.x;
                float z2 = transform.position.z;
                Vector3 V = new Vector3(x1 - x2, 0, z1 - z2);

                if (V.magnitude < 2)
                {
                    targetRotation = Quaternion.LookRotation(V.normalized, Vector3.up);
                }
            }else if(enemy2position != null)
            {
                float x1 = enemy2position.position.x;
                float z1 = enemy2position.position.z;
                float x2 = transform.position.x;
                float z2 = transform.position.z;
                Vector3 V = new Vector3(x1 - x2, 0, z1 - z2);

                if (V.magnitude < 2)
                {
                    targetRotation = Quaternion.LookRotation((enemy2position.position - transform.position).normalized, Vector3.up);
                }
            } 
            

            

            if (Holding_Combat)
            {
                //攻撃アクション
                animator.SetTrigger("Holding_Combat");

            }
            else
            {
                //武器だし
                animator.SetTrigger("Holding_Combat");
                Holding_Combat = true;
                animator.SetBool("IsCombat", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Holding_Combat)
            {
                //武器をしまう
                animator.SetTrigger("Close_Combat");
                Holding_Combat= false;
                animator.SetBool("ISCombat", false);
            }
        }
    }

    void BlockAnimation()
    {
        if(Input.GetKey(KeyCode.C))
        {
            animator.SetBool("IsBlock", true);
        }
        else
        {
            animator.SetBool("IsBlock", false);
        }
    }

    void Run()
    {
        //入力受付
        x = Input.GetAxisRaw("Horizontal") * movespeed;
        z = Input.GetAxisRaw("Vertical") * movespeed;
        Quaternion horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        //移動方向
        //Vector3 direction = transform.position + new Vector3(x, 0, z) * movespeed;
        rb.velocity = horizontalRotation * new Vector3(x, rb.velocity.y, z);
        //yの移動を無視した速度look
        Vector3 look = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //アニメーションにスピードを渡してあげる
        animator.SetFloat("Speed", look.magnitude);

        //振り向きを滑らかにする
        var rotationspeed = 600 * Time.deltaTime;
        //速度が０より上だったらその方向を向かせる
        
        if (look.magnitude > 0.01f)
        {
            targetRotation = Quaternion.LookRotation(look, Vector3.up);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationspeed);
    }

    //ダメージ受けたら
    void Damage(int damage)
    {
        hp -= damage;
        if(hp<=0)
        {
            hp = 0;
            rb.velocity = Vector3.zero;
        }
        PlayerUIManager.UpdateHP(hp);
    }

    //当たり判定の処理
    private void OnTriggerEnter(Collider other)
    {
        if (hp <= 0)
        {
            return;
        }
        if (IsSuperTime)
        {
            return;
        }

        Damager damager = other.GetComponent<Damager>();
        if ((damager != null))
        {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
            IsSuperTime = true;
        }
        
        
    }

    void SuperTime()
    {
        if ((IsSuperTime))
        {
            if (SuperTimeCount <= 60)
            {
                SuperTimeCount++;
            }
            else
            {
                SuperTimeCount = 0;
                IsSuperTime = false;
            }
        }
    }

    //アイドル状態になったら攻撃をリセット
    //ダメージ処理も追加
    public void ResetAttack()
    {
        animator.ResetTrigger("Holding_Combat");
        animator.ResetTrigger("Hurt");
    }

    //武器の当たり判定出したり消したり
    //エフェクトも管理
    public void HideColliderWeapon()
    {
        WeaponCollider.enabled = false;
        ParticleSystem.transform.localScale = Vector3.zero;
    }
    public void ShowColliderWeapon()
    {
        WeaponCollider.enabled = true;
        ParticleSystem.transform.localScale = Vector3.one;
    }
    public void HideColliderOnly()
    {
        WeaponCollider.enabled = false;
    }

    //ガードの当たり判定
    public void HideGardCollider()
    {
        GardCollider.enabled=false;
    }
    public void ShowGardCollider()
    {
        GardCollider.enabled = true;
    }

    //剣のエフェクト出したり消したり
    public void HideParticle()
    {
        ParticleSystem.transform.localScale = Vector3.zero;
    }
    public void ShowParticle()
    {
        ParticleSystem.transform.localScale = Vector3.one;
    }

    //ヒットストップの実装
    public void OnAttackHit()
    {
        HitEffect.Play();
        var effc = DOTween.Sequence();
        effc.SetDelay(0.5f);
        effc.AppendCallback(() => HitEffect.Stop());
        animator.speed = 0f;
        var seq = DOTween.Sequence();
        seq.SetDelay(HitStopTime);
        seq.AppendCallback(() => animator.speed = 1f);
    }

}

