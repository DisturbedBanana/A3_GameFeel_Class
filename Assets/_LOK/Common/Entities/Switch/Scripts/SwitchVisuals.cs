using System;
using UnityEngine;

namespace LOK.Core.Switch
{
    public class SwitchVisuals : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private SwitchEntity _switchEntity;

        [Header("Renderers")]
        [SerializeField] private SpriteRenderer _onSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _offSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _disabledSpriteRenderer = null;

        private void Awake()
        {
            _onSpriteRenderer.gameObject.SetActive(false);
            _offSpriteRenderer.gameObject.SetActive(true);
            _disabledSpriteRenderer.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _switchEntity.OnSwitchOn += _OnSwitchOn;
            _switchEntity.OnSwitchOff += _OnSwitchOff;
            _switchEntity.OnSwitchDisable += _OnSwitchDisable;
        }


        private void OnDisable()
        {
            _switchEntity.OnSwitchOn -= _OnSwitchOn;
            _switchEntity.OnSwitchOff -= _OnSwitchOff;
            _switchEntity.OnSwitchDisable -= _OnSwitchDisable;
        }

        private void _OnSwitchDisable(SwitchEntity obj)
        {
            _onSpriteRenderer.gameObject.SetActive(false);
            _offSpriteRenderer.gameObject.SetActive(false);
            _disabledSpriteRenderer.gameObject.SetActive(true);
        }

        private void _OnSwitchOff(SwitchEntity obj)
        {
            _onSpriteRenderer.gameObject.SetActive(false);
            _offSpriteRenderer.gameObject.SetActive(true);
            _disabledSpriteRenderer.gameObject.SetActive(false);
        }

        private void _OnSwitchOn(SwitchEntity switchEntity)
        {
            _onSpriteRenderer.gameObject.SetActive(true);
            _offSpriteRenderer.gameObject.SetActive(false);
            _disabledSpriteRenderer.gameObject.SetActive(false);
        }
    }
}