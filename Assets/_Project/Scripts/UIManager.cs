using System.Linq.Expressions;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuPanel;
    public float duration = 1;

    public bool isPanelOpen;
    
    [Button("Open Panel")]
    public void OpenPanel()
    {
        if (isPanelOpen) return;
        
        //float valFloat = 0f;
        // DOTween.To(() => valFloat, x => valFloat = x, 0f, 1f);
        mainMenuPanel.SetActive(true);
        mainMenuPanel.transform.localScale = new Vector3(0, 0, 0);
        var alpha = mainMenuPanel.GetComponent<CanvasGroup>().alpha;
        mainMenuPanel.transform.DOScale(1, duration);
        DOTween.To(() => mainMenuPanel.GetComponent<CanvasGroup>().alpha, x => mainMenuPanel.GetComponent<CanvasGroup>().alpha = x, 1, duration);
        isPanelOpen = true;
    }

    [Button("Close Panel")]
    public void ClosePanel()
    {
        var alpha = mainMenuPanel.GetComponent<CanvasGroup>().alpha;
        mainMenuPanel.transform.DOScale(0, duration);
        DOTween.To(() => mainMenuPanel.GetComponent<CanvasGroup>().alpha, x => mainMenuPanel.GetComponent<CanvasGroup>().alpha = x, 0, duration).OnComplete(
            () =>
            {
                mainMenuPanel.SetActive(false);
            });
        isPanelOpen = false;
    }
}
