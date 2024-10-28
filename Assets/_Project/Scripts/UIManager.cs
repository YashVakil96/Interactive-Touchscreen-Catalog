using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Animator mainMenuPanel;
    public Animator menuCap;
    
    public float duration = 1;
    public UIContentCarousel Carousel;

    public bool isPanelOpen;
    public bool isMenuOpen;
    public RectTransform t;

    [Header("Home Button")] public GameObject homeButton;
    public Vector2 showHome;
    public Vector2 hideHome;

    [Header("Settings Menu")]
    public GameObject settingsMenu;
    public Slider moveSlider;
    public Slider ScaleSlider;
    public TMP_Text moveText;
    public TMP_Text scaleText;


    [Button("Open Panel")]
    public void OpenPanel()
    {
        if (isPanelOpen) return;

        isPanelOpen = true;
        mainMenuPanel.Play("OpenPanel");
        AnimateHomeButton();
    }

    [Button("Close Panel")]
    public void ClosePanel()
    {
        isPanelOpen = false;
        mainMenuPanel.Play("ClosePanel");
        AnimateHomeButton();
    }

    public void OpenMenuCap()
    {
        menuCap.Play("Open");
        isMenuOpen = true;
    }

    public void CloseMenuCap()
    {
        menuCap.Play("Close");
        isMenuOpen = false;
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

    public void OpenSetting()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        settingsMenu.SetActive(false);
    }
    public void OnMoveSensitiveChange()
    {
        ImageManipulation.Instance.moveSensitivity = moveSlider.value;
        moveText.text = moveSlider.value.ToString();
    }
    
    public void OnZoomSensitiveChange()
    {
        ImageManipulation.Instance.zoomSensitivity = ScaleSlider.value;
        scaleText.text = ScaleSlider.value.ToString();
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}