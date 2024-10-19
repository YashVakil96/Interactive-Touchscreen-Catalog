using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageManipulation : Singleton<ImageManipulation>
{
    public RectTransform imageRectTransform;
    private Vector2 prevTouchPos1, prevTouchPos2;
    private bool isRotating = false;
    public float rotateSentivitiy = 1;
    public TMP_InputField sensi;

    // Zoom Variables
    public float minScale = 0.5f; // Minimum scale limit
    public float maxScale = 2.5f; // Maximum scale limit

    public float anglePC;
    [SerializeReference]
    public bool spin;

    // Update is called once per frame
    void Update()
    {
// #if UNITY_EDITOR
//         float angle = anglePC * rotateSentivitiy * Time.deltaTime;
// #else
//                 float angle = Vector2.SignedAngle(prevVector, currVector) * rotateSentivitiy * Time.deltaTime;
// #endif
//
//         Debug.Log(anglePC);
//         imageRectTransform.transform.Rotate(Vector3.back,rotateSentivitiy * Time.deltaTime);
//         // imageRectTransform.Rotate(0, 0, angle);

        // Handle pinch to zoom (two finger touch)

        if (Input.touchCount == 2)
        {
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

                // Update the previous positions
                prevTouchPos1 = touchPos1;
                prevTouchPos2 = touchPos2;
            }

            // Handle rotation with two-finger twist gesture
            if (!isRotating)
            {
                isRotating = true;
            }

            if (isRotating)
            {
                Vector2 prevVector = prevTouchPos2 - prevTouchPos1;
                Vector2 currVector = touch2.position - touch1.position;

#if UNITY_EDITOR
                float angle = anglePC * rotateSentivitiy * Time.deltaTime;
#else
                float angle = Vector2.SignedAngle(prevVector, currVector) * rotateSentivitiy * Time.deltaTime;
#endif

                Debug.Log(anglePC);
                if (spin)
                {
                    imageRectTransform.Rotate(0, 0, angle);
                }
                else
                {
                    imageRectTransform.transform.Rotate(Vector3.back, rotateSentivitiy * Time.deltaTime);
                }

                // Update previous touch positions for the next frame
                prevTouchPos1 = touch1.position;
                prevTouchPos2 = touch2.position;
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

    public void ChangeSpin()
    {
        spin = !spin;
    }
    public void OnSensiUpdate()
    {
        rotateSentivitiy = float.Parse(sensi.text);
    }
}