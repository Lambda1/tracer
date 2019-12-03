using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody = null;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(1.0f , Vector3.up);
    }
}
