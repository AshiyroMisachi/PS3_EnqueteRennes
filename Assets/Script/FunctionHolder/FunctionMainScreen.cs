using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionMainScreen : MonoBehaviour
{
    public GameObject creditImage;
    public GameObject blackImage;

    private void Start()
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeOut");

        creditImage.SetActive(false);
    }
    public void launchSelectionLevel()
    {
        //Launch Selection Scene
        StartCoroutine(LaunchScene("SelectionLevel"));
    }

    public void launchSettings()
    {
        //Launch Settings Scene
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
        }
    }
    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(()=>blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
