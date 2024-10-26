using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuPanel;
    public float duration = 1;
    public UIContentCarousel Carousel;

    public bool isPanelOpen;
    public RectTransform t;

    public Vector2 openPanel;
    public Vector2 closePanel;


    [Header("Home Button")] public GameObject homeButton;
    public Vector2 showHome;
    public Vector2 hideHome;


    [Button("Open Panel")]
    public void OpenPanel()
    {
        if (isPanelOpen) return;

        isPanelOpen = true;
        mainMenuPanel.SetActive(true);
        // mainMenuPanel.transform.DOMove(new Vector3(-1349.97217f,0,0), 1);
        mainMenuPanel.GetComponent<RectTransform>().DOAnchorPos(openPanel, 1);
        AnimateHomeButton();
    }

    [Button("Close Panel")]
    public void ClosePanel()
    {
        isPanelOpen = false;
        // mainMenuPanel.transform.DOMove(new Vector3(-2533,0,0), 1).OnComplete(() =>
        // {
        //     mainMenuPanel.SetActive(false);    
        // });

        mainMenuPanel.GetComponent<RectTransform>().DOAnchorPos(closePanel, 1);
        AnimateHomeButton();
    }

    public void HomeButton()
    {
        ClosePanel();
        homeButton.GetComponent<RectTransform>().DOAnchorPos(hideHome, 1);
        PlanManager.Instance.currentPlan.planObject.SetActive(false);
        PlanManager.Instance.otherPlan.planObject.SetActive(false);
    }

    public void AnimateHomeButton()
    {
        if (isPanelOpen)
        {
            //hide
            homeButton.GetComponent<RectTransform>().DOAnchorPos(hideHome, 1);
        }
        else
        {
            //show
            homeButton.GetComponent<RectTransform>().DOAnchorPos(showHome, 1);
        }
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}