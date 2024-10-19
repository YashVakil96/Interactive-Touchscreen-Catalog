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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            // Raycast to check for UI clicks
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Create a pointer event data

                foreach (RaycastResult result in results)
                {
                    Debug.Log("Double-clicked on UI element: " + result.gameObject.name);
                    UIManager.Instance.OpenPanel();

                    // Add your double-click handling logic here
                }
            }
            else
            {
                foreach (RaycastResult result in results)
                {
                    // Debug.Log("Single Click on UI element: " + result.gameObject.name);

                    // Add your single-click handling logic here
                }
            }

            lastClickTime = Time.time;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check what we hit
                Debug.Log("Hit: " + hit.collider.name);

                // Example: If the object has a specific tag
                if (hit.collider.CompareTag("Interactable"))
                {
                    // Perform actions on the hit object
                    Debug.Log("Interacted with: " + hit.collider.name);
                }
            }
        }
    }
}