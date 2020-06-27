using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    [SerializeField] private List<CursorAnimation> cursorAnimationList;

    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;

    public bool alwaysArrow = true;
    

    public enum CursorType
    {
        Arrow,
        Grab,
        Talk,
        Move,
        Arrive,
        Interact
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DialogueManager.Instance.OnDialogStart += (sender, args) => BlockCursor();
        DialogueManager.Instance.OnDialogFinish += (sender, args) => UnlockCursor();
        SetActiveCursorType(CursorType.Arrow);
    }
    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        if (alwaysArrow) return;
        
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }
    
    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach(CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if (cursorAnimation.cursorType == cursorType)
            {
                return cursorAnimation;
            }
        }
        // Couldn't find this CursorType!
        return null;
    }
    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }

    public void BlockCursor()
    {
        alwaysArrow = true;
        SetActiveCursorAnimation(GetCursorAnimation(CursorType.Arrow));
    }

    public void UnlockCursor()
    {
        alwaysArrow = false;
    }
    
    
    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}