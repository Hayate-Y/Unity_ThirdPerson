using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCamera : MonoBehaviour
{
    //ƒJƒƒ‰‚ÌÀ•W‚ğ“ü‚ê‚é
    public Transform trans;
    private void Update()
    {
        transform.LookAt(trans);
    }
}
