using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class PortalSpawnRule : MonoBehaviour
{
    private static readonly ExposedProperty PortalFullness = "PortalFullness";
    [SerializeField] private PortalHandCreating _portalHandCreating;
    [SerializeField] private VisualEffect _portalVfx;

    private void Update()
    {
        if (_portalHandCreating._circleProgress >= 4)
        {
            var cameraPos = Camera.main.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, cameraPos - Vector3.up, 0.5f * Time.deltaTime);
        }
       
    }

    private void OnEnable()
    {
        var cameraPos = Camera.main.transform.position;
        
        if (Camera.main != null && _portalHandCreating._circleProgress < 4) 
        {
            transform.position = new Vector3(cameraPos.x, cameraPos.y - 1, cameraPos.z + 3);
        }
       
    }
    
    private void OnDisable()
    {
        if (_portalHandCreating._circleProgress < 4)
        {
            _portalHandCreating._circleProgress = 0;
            _portalVfx.SetFloat("PortalFullness", 0);
        }
        _portalVfx.GetFloat(PortalFullness);
        
    }
}
