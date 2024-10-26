using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageManipulation : Singleton<ImageManipulation>
{
    public RectTransform imageRectTransform;
    private Vector2 prevTouchPos1, prevTouchPos2;
    public bool isZooming;
    public float zoomThreshold;

    public TMP_InputField sensi;

    // Zoom Variables
    public float minScale = 0.5f; // Minimum scale limit
    public float maxScale = 2.5f; // Maximum scale limit

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            if (TwoFingerRotate.Instance.isRotating)
                return;
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
        
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                prevTouchPos1 = touch1.position;
                prevTouchPos2 = touch2.position;
            }
        
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 touchPos1 = touch1.position;
                Vector2 touchPos2 = touch2.position;
        
                // Distance between current touches
                float currentDistance = Vector2.Distance(touchPos1, touchPos2);
                float prevDistance = Vector2.Distance(prevTouchPos1, prevTouchPos2);
        
                // Zoom Factor
                if (currentDistance - prevDistance > zoomThreshold ||currentDistance - prevDistance < zoomThreshold)
                {
                    // isZooming = true;
                    float zoomFactor = (currentDistance - prevDistance) * 0.01f;
                    // Apply zoom to the image scale
                    Vector3 currentScale = imageRectTransform.localScale;
                    currentScale += Vector3.one * zoomFactor;
        
                    // Clamp the scale within the limits
                    currentScale = new Vector3(
                        Mathf.Clamp(currentScale.x, minScale, maxScale),
                        Mathf.Clamp(currentScale.y, minScale, maxScale),
                        currentScale.z
                    );
        
                    imageRectTransform.localScale = currentScale;
                }
        
        
                // Update the previous positions
                prevTouchPos1 = touchPos1;
                prevTouchPos2 = touchPos2;
            }
        
            if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                // isZooming = false;
            }
        }
        else if (Input.touchCount == 1)
        {
            // Handle pan (dragging)
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;

                imageRectTransform.anchoredPosition += deltaPosition;
            }
        }
    }
}