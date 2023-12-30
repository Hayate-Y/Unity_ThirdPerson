using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Slider HPSlider;

    public void Init(PlayerManager playerManager)
    {
        HPSlider.maxValue = playerManager.MaxHP;
        HPSlider.value = playerManager.MaxHP;
    }

    public void UpdateHP(int hp)
    {
        HPSlider.DOValue(hp, 0.5f);
    }
}
