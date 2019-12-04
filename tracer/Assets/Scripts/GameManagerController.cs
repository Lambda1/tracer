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
    [SerializeField] private GameObject m_enemy  = null;
    [SerializeField] private GateController m_gate = null;
    [SerializeField] private DoorController m_door = null;

    // ゲーム段階
    private WAVE m_wave;

    // ゲーム定数
    private const int KEY_ITEM_NUM = 5; // キーアイテムの数

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
                break;
            default:
                break;
        }
    }

    void FirstWave()
    {
        // 初めのキーを取得したら, WAVEを移行
        if (m_player.GetItemCount > 0)
        {
            m_wave = WAVE.MIDDLE;
            m_gate.Open(); // 門を解放
        }
    }

    void MiddleWave()
    {
        // 全てのキーを取得したら, 最終WAVEに移行
        if (m_player.GetItemCount >= KEY_ITEM_NUM)
        {
            m_wave = WAVE.LAST;
            m_door.Open();
        }
    }
}
