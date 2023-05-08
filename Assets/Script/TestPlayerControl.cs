using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerControl : MonoBehaviour
{
    //[SerializeField] private GameObject instantiatedObj;

    

    private InputActionsControl inputAction;
    private InputAction swithLamp;
    [SerializeField] private HallSceneController controller;

    Vector3 spawnPosition;

    void Awake()
    {
        inputAction = new InputActionsControl();
    }

    void OnEnable()
    {
        swithLamp = inputAction.Player.SwitchLamp;
        inputAction.Player.SwitchLamp.performed += ctx => DoSwitchLamp();
        inputAction.Enable();
    }

    void OnDisable()
    {
        inputAction.Disable();
    }

    void DoSwitchLamp()
    {
        controller.SwitchLamp();
    }

    void DoSwitchScreen()
    {
        controller.PopUpDancer();
        controller.SpotlightSwitch();
    }

}
