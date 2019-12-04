using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool is_open; // ドア開場フラグ

    // Start is called before the first frame update
    void Start()
    {
        is_open = false;
    }

    public void Open()
    {
        is_open = true;
        this.GetComponent<Animation>().Play("Door_Open");
    }
}
