using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public PlayerController playerController;
    public GameObject mainCamera;

    public Animator levelFadeAnimator;
    public IEnumerator ChangeLevel(GameObject from, GameObject to, GameObject spawnPoint, SpriteRenderer nextBg)
    {
        levelFadeAnimator.SetBool("Start", true);
        yield return new WaitForSeconds(levelFadeAnimator.GetCurrentAnimatorStateInfo(0).length 
                                        + levelFadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        
        levelFadeAnimator.SetBool("Start", false);
        from.SetActive(false);
        to.SetActive(true);
        
        var position = spawnPoint.transform.position;
        playerController.SetPosition(position);
        mainCamera.GetComponent<CameraFollow>().SetSpriteRenderer(nextBg);
        mainCamera.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, mainCamera.transform.position.z);
        
    }

}
