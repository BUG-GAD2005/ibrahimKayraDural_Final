using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResourcePresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText, gemText;

    void Start()
    {
        GameManager.instance.event_ValueChanged_Gold += event_GoldValueChanged;
        GameManager.instance.event_ValueChanged_Gem += event_GemValueChanged;
    }

    void event_GemValueChanged(object sender, int e)
    {
        gemText.text = e.ToString();
    }
    void event_GoldValueChanged(object sender, int e)
    {
        goldText.text = e.ToString();
    }
}
