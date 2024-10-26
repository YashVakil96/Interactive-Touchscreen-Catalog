using UnityEngine;

public class ZoomRotatePan : Singleton<ZoomRotatePan>
{
    public RectTransform imageRectTransform;

    private Vector2 prevTouchPos1, prevTouchPos2;
    private bool isZooming = false;
    private bool isRotating = false;
    private float minScale = 0.5f;
    private float maxScale = 2.5f;
    private float rotationThreshold = 5f; // Rotation threshold to prevent conflicts
    private float initialAngle;


    void Update()
    {
        // Two-finger touch
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                prevTouchPos1 = touch1.position;
                prevTouchPos2 = touch2.position;
                initialAngle = Vector2.SignedAngle(prevTouchPos1 - prevTouchPos2, Vector2.right);
                isZooming = false;
                isRotating = false;
            }

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 touchPos1 = touch1.position;
                Vector2 touchPos2 = touch2.position;

                float prevDistance = Vector2.Distance(prevTouchPos1, prevTouchPos2);
                float currentDistance = Vector2.Distance(touchPos1, touchPos2);
                float distanceDelta = currentDistance - prevDistance;

                float prevAngle = Vector2.SignedAngle(prevTouchPos1 - prevTouchPos2, Vector2.right);
                float currentAngle = Vector2.SignedAngle(touchPos1 - touchPos2, Vector2.right);
                float angleDelta = currentAngle - prevAngle;

                // Decide whether to zoom or rotate based on the touch input
                if (Mathf.Abs(angleDelta) > rotationThreshold && !isZooming)
                {
                    // Perform Rotation
                    isRotating = true;
                    imageRectTransform.Rotate(0, 0, angleDelta);
                }
                else if (!isRotating)
                {
                    // Perform Zoom
                    isZooming = true;
                    Vector3 scale = imageRectTransform.localScale;
                    scale += Vector3.one * (distanceDelta * 0.01f);
                    scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
                    scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
                    imageRectTransform.localScale = scale;
                }

                // Update previous touch positions for next frame
                prevTouchPos1 = touchPos1;
                prevTouchPos2 = touchPos2;
            }
        }
    }
}
