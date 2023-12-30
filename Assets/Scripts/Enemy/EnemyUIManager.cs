using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(EnemyManager enemyManager)
    {
        hpSlider.maxValue = enemyManager.MaxHP;
        hpSlider.value = enemyManager.MaxHP;
    }
    public void Init2(Enemy2Manager enemyManager)
    {
        hpSlider.maxValue = enemyManager.MaxHP;
        hpSlider.value = enemyManager.MaxHP;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
}
