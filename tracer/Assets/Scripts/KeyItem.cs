using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody = null;
    [SerializeField] private AudioSource m_audio_source = null;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 回転させて, アイテムの存在を知覚させる.
        transform.rotation *= Quaternion.AngleAxis(1.0f , Vector3.up);
    }

    void OnDisable()
    {
        Destroy(this);
    }

    public void Got()
    {
        // オブジェクトの消滅処理
        this.transform.GetChild(0).GetComponent<Light>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        // SEを鳴らす
        StartCoroutine("PlayGetItemSounds");
    }
    
    private IEnumerator PlayGetItemSounds()
    {
        m_audio_source.PlayOneShot(m_audio_source.clip);
        // SE終了時にオブジェクトを破棄
        while (true)
        {
            if(!m_audio_source.isPlaying)
            {
                this.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
}
