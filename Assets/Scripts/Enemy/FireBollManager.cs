using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBollManager : MonoBehaviour
{
    int DestroyCount =360;//‰ó‚ê‚é‚Ü‚Å‚ÌŽžŠÔ
    int now;

    private void Start()
    {
    }

    private void Update()
    {
        if (now <= DestroyCount)
        {
            now++;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
