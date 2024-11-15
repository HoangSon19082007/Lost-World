using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Stat : MonoBehaviour, IPointerClickHandler
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI percentText;
    [SerializeField] private Image iconImage;

    [Header("Propeties")]
    [SerializeField] private float fillDuration = 1f;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color increaseColor = Color.green;
    [SerializeField] private Color decreaseColor = Color.red;
    [SerializeField] private Ease effect;
    private float currentFillAmount;

    private void Start()
    {
        InceaseProperty(50);
    }

    public void InceaseProperty(float percent)
    {
        ChangeFillAmount(percent, true);
    }

    public void DeceaseProperty(float percent)
    {
        ChangeFillAmount(percent, false);
    }

    public void ChangeFillAmount(float percent, bool increase)
    {
        float targetFillAmount;
        if (increase)
        {
            targetFillAmount = iconImage.fillAmount + (percent / 100);
            iconImage.color = increaseColor;
        }
        else
        {
            targetFillAmount = iconImage.fillAmount - (percent / 100);
            iconImage.color = decreaseColor;
        }
        targetFillAmount = Mathf.Clamp01(targetFillAmount);

        iconImage.DOFillAmount(targetFillAmount, fillDuration)
            .SetEase(effect)
            .OnComplete(() =>
            {
                iconImage.color = normalColor;
            });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int random = Random.Range(0, 2);
        if (random == 0)
            InceaseProperty(10);
        else
            DeceaseProperty(10);
    }
}
