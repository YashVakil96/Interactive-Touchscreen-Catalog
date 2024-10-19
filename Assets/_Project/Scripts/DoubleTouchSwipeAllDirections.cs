using UnityEngine;

public class DoubleTouchSwipeAllDirections : MonoBehaviour
{
    private Vector2 initialTouchPosition1;
    private Vector2 initialTouchPosition2;

    private bool isTouching = false;

    // Minimum swipe distance to be considered a swipe
    public float minSwipeDistance = 50f;

    void Update()
    {
        // Check if there are two touches on the screen
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // If the touches have just begun
            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                // Record the initial positions of both touches
                initialTouchPosition1 = touch1.position;
                initialTouchPosition2 = touch2.position;
                isTouching = true;
            }
            // If touches are moving (swiping)
            else if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved && isTouching)
            {
                // Calculate the swipe distances for both touches
                Vector2 swipeDistance1 = touch1.position - initialTouchPosition1;
                Vector2 swipeDistance2 = touch2.position - initialTouchPosition2;

                // Check for horizontal swipe (right or left)
                if (Mathf.Abs(swipeDistance1.x) > minSwipeDistance && Mathf.Abs(swipeDistance2.x) > minSwipeDistance)
                {
                    if (swipeDistance1.x > 0 && swipeDistance2.x > 0)
                    {
                        // Swipe Right
                        Debug.Log("Double-touch swipe right detected!");
                        OnDoubleSwipeRight();
                    }
                    else if (swipeDistance1.x < 0 && swipeDistance2.x < 0)
                    {
                        // Swipe Left
                        Debug.Log("Double-touch swipe left detected!");
                        OnDoubleSwipeLeft();
                    }
                }

                // Check for vertical swipe (up or down)
                if (Mathf.Abs(swipeDistance1.y) > minSwipeDistance && Mathf.Abs(swipeDistance2.y) > minSwipeDistance)
                {
                    if (swipeDistance1.y > 0 && swipeDistance2.y > 0)
                    {
                        // Swipe Up
                        Debug.Log("Double-touch swipe up detected!");
                        OnDoubleSwipeUp();
                    }
                    else if (swipeDistance1.y < 0 && swipeDistance2.y < 0)
                    {
                        // Swipe Down
                        Debug.Log("Double-touch swipe down detected!");
                        OnDoubleSwipeDown();
                    }
                }
            }
            // Reset when the touches end
            else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
    }

    // Custom actions for each swipe direction
    void OnDoubleSwipeRight()
    {
        // Handle double-swipe right action here
        Debug.Log("Action triggered by double-touch swipe right.");
        UIManager.Instance.OpenPanel();
    }

    void OnDoubleSwipeLeft()
    {
        // Handle double-swipe left action here
        Debug.Log("Action triggered by double-touch swipe left.");
        UIManager.Instance.ClosePanel();
    }

    void OnDoubleSwipeUp()
    {
        // Handle double-swipe up action here
        Debug.Log("Action triggered by double-touch swipe up.");
    }

    void OnDoubleSwipeDown()
    {
        // Handle double-swipe down action here
        Debug.Log("Action triggered by double-touch swipe down.");
    }
}
