using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EasterEggManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float _displayTime = 0.2f;
    [SerializeField] private List<InputAction> _secretCombo;

    [Header("Images de l'easter egg (SpriteRenderer)")]
    [SerializeField] private List<GameObject> _imageObjects;
    
    [Header("Sound Effect")]
    [SerializeField] private AudioSource _easterEggSound;

    private int _comboIndex;

    private void OnEnable()
    {
        foreach (var action in _secretCombo)
            action.Enable();
    }

    private void OnDisable()
    {
        foreach (var action in _secretCombo)
            action.Disable();
    }

    private void Update()
    {
        if (_secretCombo == null || _secretCombo.Count == 0) return;

        if (_secretCombo[_comboIndex].triggered)
        {
            _comboIndex++;
            if (_comboIndex >= _secretCombo.Count)
            {
                _comboIndex = 0;
                TriggerEasterEgg();
            }
        }
        else if (AnyOtherActionTriggered())
        {
            _comboIndex = 0;
        }
    }

    private bool AnyOtherActionTriggered()
    {
        foreach (var action in _secretCombo)
        {
            if (action.triggered) return false;
        }
        return true;
    }

    private void TriggerEasterEgg()
    {
        if (_imageObjects == null || _imageObjects.Count == 0) return;

        int randomIndex = Random.Range(0, _imageObjects.Count);
        StartCoroutine(ShowImageCoroutine(_imageObjects[randomIndex]));
    }

    private IEnumerator ShowImageCoroutine(GameObject imageObject)
    {
        imageObject.SetActive(true);
        _easterEggSound.Play();
        yield return new WaitForSeconds(_displayTime);
        imageObject.SetActive(false);
    }
}
