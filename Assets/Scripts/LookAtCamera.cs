using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCamera : MonoBehaviour
{
    //�J�����̍��W������
    public Transform trans;
    private void Update()
    {
        transform.LookAt(trans);
    }
}
