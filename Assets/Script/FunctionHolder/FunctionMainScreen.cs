using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionMainScreen : MonoBehaviour
{
    public GameObject creditImage;

    private void Start()
    {
        creditImage.SetActive(false);
    }

    public void launchSelectionLevel()
    {
        //Launch Selection Scene
        SceneManager.LoadScene("SelectionLevel");
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

}
