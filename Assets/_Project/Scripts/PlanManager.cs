using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlanManager : Singleton<PlanManager>
{
    public List<Sprite> plansList;
    public int currentPlanSelected;
    public List<PlanObjects> ObjectsList;
    public PlanObjects currentPlan = new PlanObjects();
    public PlanObjects otherPlan = new PlanObjects();
    public GameObject planCloseButton;


    public void OpenPlan()
    {
        foreach (var plan in ObjectsList)
        {
            if (!plan.inUse)
            {
                planCloseButton.SetActive(true);

                currentPlan = plan;
                plan.imageRenderer.sprite = plansList[currentPlanSelected];
                ImageManipulation.Instance.imageRectTransform = currentPlan.planObject.GetComponent<RectTransform>();
                TwoFingerRotate.Instance.imageRectTransform = currentPlan.planObject.GetComponent<RectTransform>();
                // ZoomRotatePan.Instance.imageRectTransform= currentPlan.planObject.GetComponent<RectTransform>();
            }
            else
            {
                otherPlan = plan;
            }
        }

        currentPlan.inUse = true;
        currentPlan.InAnimation();
        try
        {
            otherPlan.OutAnimation();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        otherPlan.inUse = false;
        UIManager.Instance.Carousel.autoMove = false;
    }

    public void ClosePlan()
    {
        currentPlan.inUse = false;
        currentPlan.OutAnimation();
        planCloseButton.SetActive(false);
        UIManager.Instance.Carousel.autoMove = true;
    }
}

[Serializable]
public class PlanObjects
{
    public bool inUse;
    public GameObject planObject;
    public Image imageRenderer;

    [Button("IN Animation")]
    public void InAnimation()
    {
        planObject.SetActive(true);
        // planObject.transform.DOMoveZ(planObject.transform.position.z+2, 1);
        planObject.transform.DOLocalMove(new Vector3(0, 0, 46), 1);
        planObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    [Button("OUT Animation")]
    public void OutAnimation()
    {
        planObject.transform.localPosition = new Vector3(0, 0, -1000);
        planObject.SetActive(false);
        // planObject.transform.DOLocalMove(new Vector3(0,0,1404), 1).OnComplete(() =>
        // {
        //     planObject.transform.localPosition = new Vector3(0, 0, -1000);
        //     planObject.SetActive(false);
        // });
    }
}