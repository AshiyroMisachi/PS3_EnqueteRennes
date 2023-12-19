using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionMainScreen : MonoBehaviour
{
    public DataHolder dataHolder;
    public GameObject creditImage;
    public GameObject blackImage;
    public AudioSource clickButtonFeedback;

    private void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();

        if (dataHolder.lastScene == "")
        {
            blackImage.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        else
        {
            blackImage.GetComponent<Animator>().SetTrigger("Clear");
        }

        creditImage.SetActive(false);
    }
    public void launchSelectionLevel()
    {
        //Launch Selection Scene
        dataHolder.UpdateLastScene();
        StartCoroutine(LaunchScene("SelectionLevel"));
    }

    public void launchSettings()
    {
        //Launch Settings Scene
        dataHolder.UpdateLastScene();
        SceneManager.LoadScene("Option");
    }

    public void showCredits()
    {
        //Show Credit image, if already visible hide
        if (creditImage.activeSelf)
        {
            creditImage.SetActive(false);
        }
        else
        {
            creditImage.SetActive(true);
            clickButtonFeedback.Play();
        }
    }
    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
