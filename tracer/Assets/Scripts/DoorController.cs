using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // 処理変数
    [SerializeField] AudioSource m_audio_source = null;
    private int delay_frame_counter; // 遅延フレームカウンタ

    // パラメータ
    [SerializeField] private int DELAY_AUDIO_FRAME = 1; // SE遅延フレーム数

    // Start is called before the first frame update
    void Start()
    {
        m_audio_source = GetComponent<AudioSource>();
        delay_frame_counter = 0;
    }

    // ゴールを開く
    public void Open()
    {
        StartCoroutine("PlaySE");
        this.GetComponent<Animation>().Play("Door_Open");
    }

    IEnumerator PlaySE()
    {
        // ドア開錠SEを遅延させる
        // NOTE: キーアイテム取得SEを重なるため
        while(true)
        {
            delay_frame_counter++;
            if (delay_frame_counter > DELAY_AUDIO_FRAME) { break; }
            yield return null;
        }
        m_audio_source.PlayOneShot(m_audio_source.clip);
    }
}
