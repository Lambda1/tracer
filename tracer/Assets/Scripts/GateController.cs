using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // パラメータ
    [SerializeField] private float HEIGHT_LENGTH = 3.0f; // 門の移動量
    // 処理変数
    private bool is_open; // 門の開閉

    // Start is called before the first frame update
    void Start()
    {
        is_open = false;
    }

    // 門を開く
    public void Open()
    {
        is_open = true;
        this.transform.position += new Vector3(0.0f, HEIGHT_LENGTH, 0.0f);
    }
    // 門を閉じる
    public void Close()
    {
        is_open = false;
        this.transform.position += new Vector3(0.0f, -HEIGHT_LENGTH, 0.0f);
    }
}