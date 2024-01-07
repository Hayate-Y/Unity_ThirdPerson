using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUIManager : MonoBehaviour
{
    public EnemyDestroyCount EnemyDestroyCount;
    public TMP_Text text; 

    public void Init()
    {
        int count = EnemyDestroyCount.DestroyCount;
        text.text = "ì|ÇµÇΩêîÅF" + count;
    }
}
