using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PortalOnEnter : MonoBehaviour
{
    [SerializeField] private string[] _sceneToLoad;
    [SerializeField] private PortalHandCreating _portalHandCreating;
    [SerializeField] private VisualEffect _portalVfx;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            onNewSceneEnter();
        }
        
    }

    private void onNewSceneEnter()
    {
        print("ENTERED PORTAL");
        _portalHandCreating._circleProgress = 0;
        _portalVfx.SetFloat("PortalFullness", 0);
        
        if (SceneManager.GetActiveScene().name == _sceneToLoad[0])
        {
            SceneManager.LoadScene(_sceneToLoad[1]);
        }
        else if (SceneManager.GetActiveScene().name == _sceneToLoad[1])
        {
            SceneManager.LoadScene(_sceneToLoad[2]);
        }
       
    }
}
