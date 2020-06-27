
using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool reachedTarget = true;
    public float nearDistance = 1.25f;
    public Vector2 target;

    private ClickableObject _goingClickableObject = null;
    private SpriteRenderer _spriteRenderer;

    public bool isMoving;
    public bool isFlipped;

    public Animator heroAnimator;

    void Update()
    {

        if (Vector2.Distance(transform.position, target) <= nearDistance)
        {
            OnPlayerReachedTarget();
        }

        Move();
    }

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        target = gameObject.transform.position;
    }

    private void Move()
    {
        if (transform.position.x < target.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            if(Math.Abs(transform.position.x - target.x) > 0.05f)
                _spriteRenderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }
    
    public void OnPlayerReachedTarget()
    {
        //Debug.Log("OnPlayerReached");
        isMoving = false;
        heroAnimator.SetBool("Moving", isMoving);
        
        reachedTarget = true;
        if (_goingClickableObject != null)
        {
            _goingClickableObject.Action();
            _goingClickableObject = null;
        }
    }

    public void GoTo(Vector2 pos)
    {
        isMoving = true;
        heroAnimator.SetBool("Moving", isMoving);
        
        reachedTarget = false;
        target = pos;
    }

    public void SetClickableObjectAsCallback(ClickableObject objectTo)
    {
        _goingClickableObject = objectTo;
    }

    public void SetPosition(Vector2 pos)
    {
        isMoving = false;
        heroAnimator.SetBool("Moving", isMoving);
        
        reachedTarget = false;
        transform.position = pos;
        target = pos;
        _goingClickableObject = null;
    }

    
    

}
