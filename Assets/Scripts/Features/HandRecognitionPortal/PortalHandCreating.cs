using System;
using System.Collections;
using System.Collections.Generic;
using RTHand;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.XR.ARFoundation;

public class PortalHandCreating : MonoBehaviour
{
    [SerializeField] private GameObject _portalVFXPrefab;
    [SerializeField] public RealtimeHandManager _handManager;
    [SerializeField] private VisualEffect _portalVFX;
    [SerializeField] private Button _button;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _button3;
    [SerializeField] private Button _button4;
    [SerializeField] private ARCameraBackground _cameraBackground;
    [SerializeField] private AROcclusionManager _occlusionManager;
   
    static readonly ExposedProperty PortalFullness = "PortalFullness";
    public int _circleProgress = 0;

    private void Start()
    {
        _handManager.HandUpdated += OnHandUpdated;
        _button.onClick.AddListener(Startcorutine);
        _button2.onClick.AddListener(ActivePortal);
        _button3.onClick.AddListener(OffOcclusion);
        _button4.onClick.AddListener(OffBack);
    }
    

    private void OnHandUpdated(RealtimeHand _realtimeHand)
    {
        if (_circleProgress < 4)
        {
            _portalVFXPrefab.SetActive(_realtimeHand.IsVisible);
            if (_realtimeHand.IsVisible && _portalVFX.GetFloat(PortalFullness) < 1)
            {
                var wristWorldPos = _realtimeHand.Joints[JointName.wrist].screenPos;
                var indexWorldPos = _realtimeHand.Joints[JointName.indexTip].screenPos;
                if (_circleProgress == 0)
                {
                    if (indexWorldPos.x > 0.8)
                    {
                        StartCoroutine(MoveToPosition(1));
                        _circleProgress++;
                    }
                }

                if (_circleProgress == 1)
                {
                    if (wristWorldPos.y < 0.2)
                    {
                        StartCoroutine(MoveToPosition(1));
                        _circleProgress++;
                    }
                }

                if (_circleProgress == 2)
                {
                    if (wristWorldPos.x < 0.2)
                    {
                        StartCoroutine(MoveToPosition(1));
                        _circleProgress++;
                    }
                }

                if (_circleProgress == 3)
                {
                    if (indexWorldPos.y > 0.8)
                    {
                        StartCoroutine(MoveToPosition(1));
                        _circleProgress++;
                    }
                }
            }
            
        }
    }
    
    public IEnumerator MoveToPosition(float timeToMove)
    {
        var currentPos = _portalVFX.GetFloat(PortalFullness);
        var nextPos = currentPos + 0.25f;
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / timeToMove;
            _portalVFX.SetFloat(PortalFullness, Mathf.Lerp(currentPos, nextPos, t));
            yield return null;
        }
    }

    void Startcorutine()
    {
        StartCoroutine(MoveToPosition(2));
        _circleProgress++;
    }

    void ActivePortal()
    {
        _portalVFXPrefab.SetActive(true);
    }

    void OffOcclusion()
    {
        if (_occlusionManager.enabled)
        {
            _occlusionManager.enabled = false;
        }
        else
        {
            _occlusionManager.enabled = true;
        }
       
    }

    void OffBack()
    {
        if (_cameraBackground)
        {
            _cameraBackground.enabled = false;
        }
        else
        {
            _cameraBackground.enabled = true;
        }
    }

}
