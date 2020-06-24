using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float speed;
    public SpriteRenderer spriteBg;
    public Camera cam;

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = player.position;
        //transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speed);

        float camVertExtent = cam.orthographicSize;
        float camHorzExtent = cam.aspect * camVertExtent;
        
        float leftBound   = spriteBg.bounds.min.x + camHorzExtent;
        float rightBound  = spriteBg.bounds.max.x - camHorzExtent;
        float bottomBound = spriteBg.bounds.min.y + camVertExtent;
        float topBound    = spriteBg.bounds.max.y - camVertExtent;

        var position = player.transform.position;
        float camX = Mathf.Clamp(position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(position.y, bottomBound, topBound);

        transform.position = new Vector3(camX, camY, transform.position.z);
    }

    public void SetSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        spriteBg = spriteRenderer;
    }
}
