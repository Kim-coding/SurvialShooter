using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    public static readonly string verticalInput = "Vertical";
    public static readonly string horizontaInput = "Horizontal";
    public static readonly string fireButtonName = "Fire1";

    public float vMove {  get; private set; }
    public float hMove { get; private set; }
    public bool fire {  get; private set; }


    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            fire = false;
            return;
        }
        vMove = Input.GetAxis(verticalInput);
        hMove = Input.GetAxis(horizontaInput);
        fire = Input.GetButton(fireButtonName);
    }
}
