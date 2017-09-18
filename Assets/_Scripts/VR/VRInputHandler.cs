using System;
using UnityEngine;

public class VRInputHandler : MonoBehaviour
{
    public enum SwipeDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public event Action<SwipeDirection> OnSwipe;                
    public event Action OnClick;                                
    public event Action OnDown;                                 
    public event Action OnUp;                                   
    public event Action OnDoubleClick;                          
    public event Action OnCancel;

    [SerializeField] AirVRCameraRig airVRCameraRig;
    [SerializeField] float doubleClickTime = 0.3f;    
    [SerializeField] float swipeWidth = 0.3f;

    Vector2 touchDownPosition;                        
    Vector2 touchUpPosition;                          
    float lastMouseUpTime;                            
    float lastHorizontalValue;                        
    float lastVerticalValue;         
    
    public float DoubleClickTime { get { return doubleClickTime; } }

    private void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        SwipeDirection swipe = SwipeDirection.NONE;

        if (AirVRInput.GetDown(airVRCameraRig, AirVRInput.Touchpad.Button.Touch) || Input.GetMouseButtonDown(0))
        {
            touchDownPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (OnDown != null)
                OnDown();
        }
        
        if (AirVRInput.GetUp(airVRCameraRig, AirVRInput.Touchpad.Button.Touch) || Input.GetMouseButtonUp(0))
        {
            touchUpPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            swipe = DetectSwipe();

            if (OnSwipe != null)
                OnSwipe(swipe);

            if (OnUp != null)
                OnUp();

            if(Time.time - lastMouseUpTime < doubleClickTime)
            {
                if(OnDoubleClick != null)
                {
                    OnDoubleClick();
                }
            }

            else
            {
                if (OnClick != null)
                {
                    OnClick();
                }
            }

            lastMouseUpTime = Time.time;
        }

        if(AirVRInput.GetDown(airVRCameraRig, AirVRInput.Touchpad.Button.Back) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnCancel != null)
                OnCancel();
        }
    }

    SwipeDirection DetectSwipe()
    {
        Vector2 swipeData = (touchUpPosition - touchDownPosition).normalized;

        bool swipeIsVertical = Mathf.Abs(swipeData.x) < swipeWidth;
        bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < swipeWidth;

        if (swipeData.y > 0f && swipeIsVertical)
            return SwipeDirection.UP;

        if (swipeData.y < 0f && swipeIsVertical)
            return SwipeDirection.DOWN;

        if (swipeData.x > 0f && swipeIsHorizontal)
            return SwipeDirection.RIGHT;

        if (swipeData.x < 0f && swipeIsHorizontal)
            return SwipeDirection.LEFT;

        return SwipeDirection.NONE;
    }

    void OnDestroy()
    {
        OnSwipe = null;
        OnClick = null;
        OnDoubleClick = null;
        OnDown = null;
        OnUp = null;
    }
}