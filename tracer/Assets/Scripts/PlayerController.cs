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

    // パラメータ
    [SerializeField] private float m_speed = 1.0f;             // 移動速度(方向)
    [SerializeField] private float m_mouse_sensitivity = 1.0f; // マウス感度
    private int m_get_item_number;                             // 取得アイテム数

    // 処理変数
    private Vector3 m_player_first_position; // 初期座標
    private Vector3 m_move_direction;        // 移動方向ベクトル
    private Quaternion m_angle_direction;    // 回転クォータニオン

    void Start()
    {
        m_player_first_position = transform.position;
        m_move_direction = Vector3.zero;
        m_get_item_number = 0;
        InitKeyTable();
    }

    void Update()
    {
       m_move_direction = MoveDirection();    // キーによる移動
       m_angle_direction = RotateDirection(); // マウスによる回転
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        RotateBody();
    }

    // Player制御
    // 移動処理
    void Move()
    {
        //transform.Translate(m_move_direction * Time.deltaTime * m_speed);
        //transform.position += m_move_direction * Time.deltaTime * m_speed;
        m_rigidbody.AddRelativeForce(m_move_direction * Time.deltaTime * m_speed); // ローカル座標をもとに移動
    }
    // 回転処理
    void RotateBody()
    {
        transform.rotation *= m_angle_direction;
    }

    // キー入力による移動ベクトルの算出
    Vector3 MoveDirection()
    {
        // ベクトル初期化
        Vector3 direction = Vector3.zero;
        // 入力処理
        foreach(KeyData data in m_key_table)
        {
            if (Input.GetKey((KeyCode)data.m_key))
            {
                direction += data.m_vec;
            }
        }
        return direction;
    }
    // マウス入力による回転処理
    Quaternion RotateDirection()
    {
        Quaternion quaternion = m_angle_direction;
        if (Input.GetMouseButton(0))
        {
            Quaternion to_quart1 = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * m_mouse_sensitivity, Vector3.up);
            quaternion = Quaternion.Slerp(transform.rotation, to_quart1 , 1.0f);
        }
        return quaternion;
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

    void OnTriggerEnter(Collider other)
    {
        // アイテム取得処理
        if(other.CompareTag("KeyItem"))
        {
            m_get_item_number++;
            other.GetComponent<KeyItem>().Got();
        }
    }
}
