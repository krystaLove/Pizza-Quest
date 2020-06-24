
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool reachedTarget = true;
    public float nearDistance = 1.25f;
    public Vector2 target = Vector2.zero;

    private ClickableObject _goingClickableObject = null;

    void Update()
    {

        if (Vector2.Distance(transform.position, target) <= nearDistance)
        {
            reachedTarget = true;
            if(_goingClickableObject != null)
                _goingClickableObject.Action();
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    public void GoTo(Vector2 pos)
    {
        reachedTarget = false;
        target = pos;
    }

    public void SetClickableObjectAsCallback(ClickableObject objectTo)
    {
        _goingClickableObject = objectTo;
    }

    public void SetPosition(Vector2 pos)
    {
        reachedTarget = false;
        transform.position = pos;
        target = pos;
        _goingClickableObject = null;
    }
    

}
