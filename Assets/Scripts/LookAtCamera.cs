using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCamera : MonoBehaviour
{
    //カメラの座標を入れる
    public Transform trans;
    private void Update()
    {
        transform.LookAt(trans);
    }
}
