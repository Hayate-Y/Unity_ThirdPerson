using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(EnemyBase enemy)
    {
        hpSlider.maxValue = enemy.MaxHP;
        hpSlider.value = enemy.MaxHP;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
}
