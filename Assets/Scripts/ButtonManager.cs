using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public TMP_Text text;

    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnPointerEnter()
    {
        text.color = Color.red;
    }

    public void PointerExit()
    {
        text.color = Color.black;
    }
}
