using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeTest : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI resultText;
    

    public void OnCLick()
    {
        int randomNumber = Random.Range(0, 100);
        mainText.text = randomNumber.ToString();
        resultText.text = randomNumber.ToString();
        
    }
}
