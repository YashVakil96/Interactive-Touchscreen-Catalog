using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public float scale = 1.5f;
    public float duration=0.5f;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlanManager.Instance.OpenPlan);
    }

    public void AnimateOnClick()
    {
        transform.DOPunchScale(Vector3.one * scale, duration);
    }

    public void OnSelect(int value)
    {
        PlanManager.Instance.currentPlanSelected = value;
        UIManager.Instance.ClosePanel();
    }
     
}
