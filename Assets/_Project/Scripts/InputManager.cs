using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Create a pointer event data
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                // Raycast to check for UI clicks
                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    Debug.Log("Double-clicked on UI element: " + result.gameObject.name);
                    UIManager.Instance.OpenPanel();

                    // Add your double-click handling logic here
                }
            }

            lastClickTime = Time.time;
        }
    }
}
