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
    // ゲーム状態
    enum STATE
    {
        GAME,       // ゲーム中
        GAME_OVER,  // ゲームオーバ
        GAME_CLEAR, // ゲームクリア
    };

    // キャラクタデータ
    [SerializeField] private PlayerController m_player = null;
    [SerializeField] private Enemy m_enemy  = null;
    [SerializeField] private List<GateController> m_gate = null;
    [SerializeField] private DoorController m_door = null;

    // BGMデータ
    private AudioSource m_audio_source = null;
    [SerializeField] private List<AudioClip> m_audio_clip = null;

    // ゲーム段階
    private WAVE  m_wave;
    private STATE m_state;

    // ゲーム定数
    private const int KEY_ITEM_NUM = 5;                   // キーアイテムの数
    private const float ENEMY_MAX_SPEED = 10.0f;          // 敵の最高速度
    private const float GAME_OVER_DISPLAY_FRAME = 3.0f;   // ゲームオーバ画面表示時間
    // ゲームオーバ空間座標
    private Vector3 GAME_OVER_WORLD_PLAYER = new Vector3(-60, 0, -18);
    private Vector3 GAME_OVER_WORLD_ENEMY  = new Vector3(-60, 0, -20);

    // Start is called before the first frame update
    void Start()
    {
        // BGM処理
        m_audio_source = GetComponent<AudioSource>();
        m_audio_source.clip = m_audio_clip[0];
        m_audio_source.Play();

        // ゲーム処理
        m_wave = WAVE.FIRST;
        m_state = STATE.GAME;
    }

    // Update is called once per frame
    void Update()
    {
        m_state = GameState();
        switch (m_state)
        {
            case STATE.GAME:
                Game(); break;
            case STATE.GAME_OVER:
                GameOverEnd();
                break;
            case STATE.GAME_CLEAR:
                break;
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
            // ゴールを開ける
            m_door.Open();
            // 敵の移動速度をMAXにする
            m_enemy.SetEnemySpeed = ENEMY_MAX_SPEED;
            // BGM切り替え
            m_audio_source.Stop();
            m_audio_source.clip = m_audio_clip[1];
            m_audio_source.Play();
        }
    }

    // ゲーム最終のWAVE
    void LastWave()
    {

    }

    // ゲーム存続判定
    STATE GameState()
    {
        if (m_state == STATE.GAME && m_player.IsDeath)
        {
           STATE game_state = STATE.GAME_OVER;
            return game_state;
        }
        return m_state;
    }

    // ゲーム中処理
    void Game()
    {
        switch (m_wave)
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

    // ゲームオーバ処理
    IEnumerator GameOver()
    {
        float elapsed_frame = 0;
        while (elapsed_frame < GAME_OVER_DISPLAY_FRAME)
        {
            // 敵とプレイヤをゲームオーバ空間へ空間転移
            // NOTE: positionは瞬間移動ではないため, 毎フレーム計算させる.
            m_player.transform.position = GAME_OVER_WORLD_PLAYER;
            m_enemy.transform.position = GAME_OVER_WORLD_ENEMY;

            // 捕縛処理
            m_enemy.CatchPlayer(m_player.transform.position);
            m_player.CaughtEnemy(m_enemy.transform.position);

            elapsed_frame += Time.deltaTime;
            yield return null;
        }
    }
    // ゲームオーバ時の終了処理
    void GameOverEnd()
    {
        // 状態遷移
        m_state = STATE.GAME_CLEAR;
        // BGM切り替え
        m_audio_source.Stop();
        m_audio_source.PlayOneShot(m_audio_clip[2]);
        // ゲームオーバ処理
        StartCoroutine("GameOver");
    }
}
