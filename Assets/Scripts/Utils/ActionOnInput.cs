using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionOnInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;

    [SerializeField] private UnityEvent OnInput;
    
    
    private void Start()
    {
        _input.action.performed += Action;
    }

    private void OnDestroy()
    {
        _input.action.performed -= Action;
    }

    void Action(InputAction.CallbackContext context) => OnInput?.Invoke();
}
