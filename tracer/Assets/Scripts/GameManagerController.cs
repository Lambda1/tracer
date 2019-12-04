using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    // ゲームのWAVE
    enum WAVE
    {
        FIRST,
        MIDDLE,
        LAST
    };

    // キャラクタデータ
    [SerializeField] private PlayerController m_player = null;
    [SerializeField] private Enemy m_enemy  = null;
    [SerializeField] private List<GateController> m_gate = null;
    [SerializeField] private DoorController m_door = null;

    // ゲーム段階
    private WAVE m_wave;

    // ゲーム定数
    private const int KEY_ITEM_NUM = 5;          // キーアイテムの数
    private const float ENEMY_MAX_SPEED = 10.0f; // 敵の最高速度

    // Start is called before the first frame update
    void Start()
    {
        m_wave = WAVE.FIRST;
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_wave)
        {
            case WAVE.FIRST:
                FirstWave(); break;
            case WAVE.MIDDLE:
                MiddleWave(); break;
            case WAVE.LAST:
                LastWave(); break;
            default:
                break;
        }
    }

    // ゲーム開始時のWAVE
    void FirstWave()
    {
        // 初めのキーを取得したら, WAVEを移行
        if (m_player.GetItemCount > 0)
        {
            m_wave = WAVE.MIDDLE;
            // 門を開く
            foreach(GateController gate in m_gate) { gate.Open(); }
            // 敵のチェイス開始
            m_enemy.StartChase();
        }
    }

    // ゲーム中間のWAVE
    void MiddleWave()
    {
        // 全てのキーを取得したら, 最終WAVEに移行
        if (m_player.GetItemCount >= KEY_ITEM_NUM)
        {
            m_wave = WAVE.LAST;
            m_door.Open(); // ゴールを開ける
            m_enemy.SetEnemySpeed = ENEMY_MAX_SPEED;
        }
    }

    // ゲーム最終のWAVE
    void LastWave()
    {

    }
}
