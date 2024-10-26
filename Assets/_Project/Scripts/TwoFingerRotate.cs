using UnityEngine;
using UnityEngine.EventSystems;

public class TwoFingerRotate : Singleton<TwoFingerRotate>, IPointerDownHandler
{
    public RectTransform imageRectTransform;

    private Vector2 prevTouchPos1, prevTouchPos2;
    private float initialAngle;
    public float rotateThreshold;
    public bool isRotating;

    void Update()
    {
        if (ImageManipulation.Instance.isZooming)
            return;

        // Ensure two fingers are touching the screen
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // If fingers have just touched the screen
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                prevTouchPos1 = touch1.position;
                prevTouchPos2 = touch2.position;

                // Calculate initial angle between the two touches
                initialAngle = Vector2.SignedAngle(prevTouchPos1 - prevTouchPos2, Vector2.right);
            }

            // If fingers are moving
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // isRotating = true;
                Vector2 touchPos1 = touch1.position;
                Vector2 touchPos2 = touch2.position;

                // Calculate the new angle between the two touches
                float currentAngle = Vector2.SignedAngle(touchPos1 - touchPos2, Vector2.right);

                if (currentAngle > rotateThreshold ||currentAngle < rotateThreshold)
                {
                    // Calculate the rotation delta and apply it to the image
                    float rotationDelta = currentAngle - initialAngle;
                    imageRectTransform.Rotate(0, 0, -rotationDelta);

                    // Update the initial angle for the next frame
                    initialAngle = currentAngle;
                }
            }

            if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                // isRotating = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Required to allow touch interactions with the UI element
    }
}