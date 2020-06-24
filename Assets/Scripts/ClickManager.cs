using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private PlayerController _playerController = null;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }

    private void Click()
    {
        Vector2 mouseClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseClickedPosition, Vector2.zero);

        if (hit.collider == null)
        {
            Debug.Log("No colliders hit from mouse click");
            return;
        }

        if (hit.collider.CompareTag("AreaToMove"))
        {
            _playerController.GoTo(mouseClickedPosition);
            return;
        }
        
        Debug.Log("Hit Collider: " + hit.transform.tag);
        _handleClickByTag(hit);

        _playerController.GoTo(hit.transform.position);

    }

    private void _handleClickByTag(RaycastHit2D hit)
    {
        _playerController.SetClickableObjectAsCallback(hit.transform.gameObject.GetComponent<ClickableObject>());
    }
}
