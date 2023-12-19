using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public DataHolder dataHolder;
    public AudioSource sound;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        dataHolder = FindObjectOfType<DataHolder>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SelectionLevel")
        {
            Destroy(gameObject);
        }

        sound.volume = dataHolder.volume;
    }
}
