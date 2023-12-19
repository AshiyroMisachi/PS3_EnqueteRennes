using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionScoring : MonoBehaviour
{
    DataHolder dataHolder;

    //Text and Image
    public TextMeshProUGUI proofsText;
    public TextMeshProUGUI caseName;
    public Image star;

    public GameObject moreInfoButton, moreInfoImage;
    public TextMeshProUGUI moreInfoDescription;

    public GameObject blackImage;

    void Start()
    {
        //Find Dataholder
        dataHolder = FindObjectOfType<DataHolder>();

        //Setup
        caseName.text = dataHolder.levelName;
        proofsText.text = "Nombre de preuves trouvées: " + (dataHolder.proofsCount) + "/" + (dataHolder.proofsLevel.Length - 1);
        switch (dataHolder.mistake)
        {
            case 0:
                star.fillAmount = 1f;
                break;
            case 1:
                star.fillAmount = 0.8f;
                break;
            case 2:
                star.fillAmount = 0.6f;
                break;
            case 3:
                star.fillAmount = 0.4f;
                break;
            case 4:
                star.fillAmount = 0.2f;
                break;
            case 5:
                star.fillAmount = 0f;
                break;
            case 6:
                star.fillAmount = 0f;
                break;
        }

        if (star.fillAmount != 1f)
        {
            moreInfoButton.SetActive(false);
        }

        if (dataHolder.scoreArray[dataHolder.levelSelectedNumber] < star.fillAmount)
        {
            dataHolder.scoreArray[dataHolder.levelSelectedNumber] = star.fillAmount;
        }

        if (dataHolder.proofsCount > dataHolder.scoreProofArray[dataHolder.levelSelectedNumber])
        {
            dataHolder.scoreProofArray[dataHolder.levelSelectedNumber] = dataHolder.proofsCount;
        }

        moreInfoImage.SetActive(false);
        blackImage.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void GoBackSelectionLevel()
    {
        //Launch SelectionLevel
        StartCoroutine(LaunchScene("SelectionLevel"));
    }

    public void ShowMoreInfo()
    {
        if (moreInfoImage.activeInHierarchy)
        {
            moreInfoImage.SetActive(false);
        }
        else
        {
            moreInfoImage.SetActive(true);
            moreInfoDescription.text = dataHolder.levelMoreInfo;
        }
    }

    public void TakeScreenshot()
    {
        Debug.Log("Take Screenshot");
        ScreenCapture.CaptureScreenshot("/storage/emulated/0/DCIM/" + "RollandDeRennes_" + dataHolder.levelName + ".png");
    }

    private string GetAndroidExternalStoragePath()
    {
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
            jc.GetStatic<string>("DIRECTORY_DCIM"))
            .Call<string>("getAbsolutePath");
        return path;
    }

    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
