using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButon : MonoBehaviour
{
    private bool is_click = false;

    public bool IsClick { get { return is_click; } }

    void Start()
    {
        is_click = false;
    }

    public void OnClick()
    {
        is_click = true;
    }
}
