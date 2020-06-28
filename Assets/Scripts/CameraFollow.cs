
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float speed;

    public Transform objectToFollow;

    public bool isFollowing = true;
    public bool isSmooth = false;
    
    //For Camera Bounds
    public SpriteRenderer spriteBg;

    public void Start()
    {
        FollowPlayer();
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = objectToFollow.position;
            //transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speed);
        
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.Log("No Camera tagged Main Camera");
                return;
            }
            float camVertExtent =  cam.orthographicSize;
            float camHorzExtent =  cam.aspect * camVertExtent;
        
            float leftBound   = spriteBg.bounds.min.x + camHorzExtent;
            float rightBound  = spriteBg.bounds.max.x - camHorzExtent;
            float bottomBound = spriteBg.bounds.min.y + camVertExtent;
            float topBound    = spriteBg.bounds.max.y - camVertExtent;

            var position = objectToFollow.transform.position;
            float camX = Mathf.Clamp(position.x, leftBound, rightBound);
            float camY = Mathf.Clamp(position.y, bottomBound, topBound);
            
            Vector3 newPos = new Vector3(camX, camY, transform.position.z);
            if (isSmooth)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
            }
            else
            {
                transform.position = newPos;
            }
        }
    }

    public void SetSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        spriteBg = spriteRenderer;
    }

    public void SetObjectToFollow(Transform target)
    {
        objectToFollow = target;
    }

    public void FollowPlayer()
    {
        objectToFollow = player;
    }
}
