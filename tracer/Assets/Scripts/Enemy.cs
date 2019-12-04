using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // パラメータ
    [SerializeField] private float m_chase_speed = 1.0f; // 追跡速度

    // 処理変数
    private NavMeshAgent m_ai_agent;  // AI処理
    private bool is_chase;            // 追跡開始
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        m_ai_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_chase)
        {
            Chase();
        }
    }

    // 追跡開始準備
    public void PrepareChase()
    {

    }

    // 追跡開始
    public void StartChase()
    {
        // 警告ライトを灯火
        this.transform.Find("SearchLight").GetComponent<Light>().enabled = true;
        // 追跡開始
        is_chase = true;
    }

    private void Chase()
    {
        m_ai
    }
}
