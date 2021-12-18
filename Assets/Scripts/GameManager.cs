using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Gravity = -9.81f * 2; //default -9.81f

    void Start()
    {
        Physics.gravity = new Vector3(0, Gravity, 0);
    }
}
