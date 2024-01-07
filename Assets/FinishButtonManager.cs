using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButtonManager : MonoBehaviour
{
    public void OnFinishButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
}
