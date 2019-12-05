using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    private float m_fps_counter;
    private float MAX_TIME = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_fps_counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_fps_counter += Time.deltaTime;
        if(m_fps_counter > MAX_TIME)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
