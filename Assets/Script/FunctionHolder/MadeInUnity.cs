using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MadeInUnity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waiting());
    }


 public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainScreen");
    }
}
