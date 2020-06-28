using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCamera : MonoBehaviour
{
    private CameraFollow _cameraFollow;
    private float beforeDialogCameraSize = 5f;
    
    void Start()
    {
        _cameraFollow = gameObject.GetComponent<CameraFollow>();

        DialogueManager.Instance.OnDialogStart += (sender, args) => 
            StartDialogCamera(DialogueManager.Instance.cameraPosition, DialogueManager.Instance.cameraSize);
        DialogueManager.Instance.OnDialogFinish += (sender, args) => EndDialogCamera();
    }

    void StartDialogCamera(Transform target, float camSize)
    {
        beforeDialogCameraSize = Camera.main.orthographicSize;
        if (target == null) return;
        StartCoroutine(_changeCameraSize(camSize, 0.1f));
        //Camera.main.orthographicSize = camSize;
        _cameraFollow.isSmooth = true;
        _cameraFollow.SetObjectToFollow(target);
    }

    void EndDialogCamera()
    {
        //StartCoroutine(_changeCameraSize(beforeDialogCameraSize, 0.05f));
        _cameraFollow.isSmooth = false;
        _cameraFollow.FollowPlayer();
        Camera.main.orthographicSize = beforeDialogCameraSize;
    }

    IEnumerator _changeCameraSize(float camSize, float speed)
    {
        if (Camera.main.orthographicSize < camSize)
        {
            while (Camera.main.orthographicSize < camSize)
            {
                Camera.main.orthographicSize += speed;
                yield return null;
            }
        }
        else
        {
            while (Camera.main.orthographicSize > camSize)
            {
                Camera.main.orthographicSize -= speed;
                yield return null;
            }
        }
    }
    
}
