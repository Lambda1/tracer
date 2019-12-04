using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 処理変数
    private Vector3 m_movement;   // 移動量
    private bool m_on_off_switch; // 交互処理
    // ゲームオーバ時のカメラ
    public void KillCamera()
    {
        Shake();
    }

    // 揺らす処理
    void Shake()
    {
        if (!m_on_off_switch)
        {
            m_movement = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.1f, 0.1f), 0.0f);
        }
        else
        {
            m_movement = -m_movement;
        }
        transform.position += m_movement;
        m_on_off_switch = !m_on_off_switch;
    }
}
