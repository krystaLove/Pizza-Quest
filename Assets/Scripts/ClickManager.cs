
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }
    private PlayerController _playerController = null;

    public bool canMove = true;

    private void Awake()
    {
        Instance = this;
    }

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
        if (!canMove)
            return;
        
        Vector2 mouseClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseClickedPosition, Vector2.zero);

        if (IsPointerOverUIObject())
        {
            Debug.Log("UI");
            return;
        }
            

        if (hit.collider == null)
        {
            Debug.Log("No colliders hit from mouse click");
            return;
        }

        if (!hit.collider.CompareTag("InventoryUI"))
        {
            GameManager.Instance.UiInventory.CloseInventory();
        }

        if (!hit.collider.CompareTag("Person") && !hit.collider.CompareTag("Interactable"))
        {
            GameManager.Instance.ResetSelectedItem();
        }

        if (hit.collider.CompareTag("AreaToMove"))
        {
            _playerController.GoTo(mouseClickedPosition);
            _playerController.SetClickableObjectAsCallback(null);
            return;
        }
        
        Debug.Log("Hit Collider: " + hit.transform.tag);
        _handleClickByTag(hit);

    }

    private void _handleClickByTag(RaycastHit2D hit)
    {
        ClickableObject clickObj = hit.transform.gameObject.GetComponent<ClickableObject>();
        if (clickObj != null)
        {
            _playerController.GoTo(clickObj.positionToStep.position);
            _playerController.SetClickableObjectAsCallback(clickObj);
        }
       
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
