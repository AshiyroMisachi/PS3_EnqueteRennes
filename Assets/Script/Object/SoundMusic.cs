using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMusicMainTheme : MonoBehaviour
{
    // Start is called before the first frame update
    public DataHolder dataHolder;
    public AudioSource sound;
    public string sceneDestroy;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        dataHolder = FindObjectOfType<DataHolder>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == sceneDestroy)
        {
            Destroy(gameObject);
        }

        sound.volume = dataHolder.volume;
    }
}
