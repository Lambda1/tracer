using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // データ定義 
    // キー入力
    enum KEY
    {
        UP    = KeyCode.W,
        DOWN  = KeyCode.S,
        RIGHT = KeyCode.D,
        LEFT  = KeyCode.A
    };
    struct KeyData
    {
        public KEY m_key { get; }
        public Vector3 m_vec { get; }
        public KeyData(KEY key, Vector3 vec) { m_key = key; m_vec = vec; }
    }

    // テーブル
    private List<KeyData> m_key_table = new List<KeyData>();

    // データ
    [SerializeField] private Rigidbody m_rigidbody = null;

    /* パラメータ */
    [SerializeField] private float m_speed = 1.0f;
    private Vector3 m_velocity;

    void Start()
    {
        m_velocity = Vector3.zero;

        InitKeyTable();
    }

    void Update()
    {
        MoveDirection(); // キー入力による移動
    }

    void FixedUpdate()
    {
        Move();
    }

    // Player制御
    // 移動処理
    void Move()
    {
    }

    // キー入力による移動ベクトルの算出
    void MoveDirection()
    {
        // ベクトル初期化
        m_velocity = Vector3.zero;
        // 入力処理
        foreach(KeyData data in m_key_table)
        {
            if (Input.GetKey((KeyCode)data.m_key))
            {
                m_velocity += data.m_vec;
            }
        }
        // ベクトル正規化 + 移動量の算出
    }

    // 初期化処理
    // キー入力テーブル
    void InitKeyTable()
    {
        m_key_table.Add(new KeyData(KEY.UP,   new Vector3( 0.0f, 0.0f, 1.0f)));
        m_key_table.Add(new KeyData(KEY.DOWN, new Vector3( 0.0f, 0.0f,-1.0f)));
        m_key_table.Add(new KeyData(KEY.RIGHT,new Vector3( 1.0f, 0.0f, 0.0f)));
        m_key_table.Add(new KeyData(KEY.LEFT, new Vector3(-1.0f, 0.0f, 0.0f)));
    }
}
