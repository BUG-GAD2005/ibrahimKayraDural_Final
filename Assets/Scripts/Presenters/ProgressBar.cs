using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Slider slider;

    Vector3 targetPosition = Vector3.zero;

    public void Initiate(Vector3 scale, Vector3 position)
    {
        canvas.worldCamera = Camera.main;

        targetPosition = position;
        transform.localScale = scale;

        Invoke("SetPos", .2f);
    }
    void SetPos()
    {
        transform.position = targetPosition;
    }

    public void SetValue(float valueZeroToOne)
    {
        if (slider == null) return;

        slider.value = valueZeroToOne;
    }
}
