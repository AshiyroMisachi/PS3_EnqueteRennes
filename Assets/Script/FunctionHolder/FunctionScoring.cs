using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        //Find Dataholder
        dataHolder = FindObjectOfType<DataHolder>();

        //Setup
        caseName.text = dataHolder.levelName;
        proofsText.text = "Nombre de preuves trouvées: " + (dataHolder.proofsCount) + "/" + (dataHolder.proofsLevel.Length-1);
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

        if (dataHolder.scoreArray[dataHolder.levelSelectedNumber] < star.fillAmount)
        {
            dataHolder.scoreArray[dataHolder.levelSelectedNumber] = star.fillAmount;
        }

        if (dataHolder.scoreProofArray[dataHolder.levelSelectedNumber] < dataHolder.scoreProofArray[dataHolder.levelSelectedNumber])
        {
            dataHolder.scoreProofArray[dataHolder.levelSelectedNumber] = dataHolder.proofsCount;
        }
    }

    public void goBackSelectionLevel()
    {
        //Launch SelectionLevel
        SceneManager.LoadScene("SelectionLevel");
    }
}
