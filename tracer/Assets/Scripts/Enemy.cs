using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 定数
    enum ANIMATION : int
    {
        IDLE = 0,
        WALK = 1
    };
    // 処理変数
    private NavMeshAgent m_ai_agent;  // AI処理
    private bool is_chase;            // 追跡開始
    private Animator m_anim;          // アニメーション
    public GameObject target;         // ターゲット

    public float SetEnemySpeed { set { m_ai_agent.speed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        m_ai_agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
        m_anim.SetInteger("State", (int)ANIMATION.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        // 向きの変更
        transform.rotation = Quaternion.LookRotation(transform.forward);
        if (is_chase)
        {
            Chase();
        }
    }

    // 追跡開始
    public void StartChase()
    {
        // アニメーションを変更
        m_anim.SetInteger("State", (int)ANIMATION.WALK);
        // 警告ライトを灯火
        this.transform.Find("SearchLight").GetComponent<Light>().enabled = true;
        // 追跡開始
        is_chase = true;
    }
    // プレイヤ捕縛処理
    public void CatchPlayer(Vector3 player_position)
    {
        // アニメーションを変更
        m_anim.SetInteger("State", (int)ANIMATION.IDLE);
        // チェイス終了
        is_chase = false;
        m_ai_agent.enabled = false;
        transform.rotation = Quaternion.LookRotation(player_position - transform.position);
    }

    // 追跡中
    private void Chase()
    {
        m_ai_agent.destination = target.transform.position;
    }
}
