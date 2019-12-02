using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody = null;

    /* パラメータ */
    [SerializeField] private float m_speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(move_x, 0.0f, move_z);

        m_rigidbody.AddForce(movement * m_speed);
    }
}
