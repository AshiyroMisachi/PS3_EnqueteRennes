using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundFeedBack : MonoBehaviour
{
    // Start is called before the first frame update
    public DataHolder dataHolder;
    public AudioSource sound;

    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = dataHolder.son;
    }
}
