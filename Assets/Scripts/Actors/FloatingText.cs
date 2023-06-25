using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float decayTime = .8f;
    [SerializeField] TextMeshProUGUI goldTextMesh;
    [SerializeField] TextMeshProUGUI gemTextMesh;
    [SerializeField] GameObject mesh;

    float TargetTime_decay = float.MaxValue;
    bool isInstantiated = false;

    void Awake()
    {
        if (mesh != null) { mesh.SetActive(false); }
    }
    void Update()
    {
        if(isInstantiated)
        {
            Vector3 targetPos = transform.position;
            targetPos.y += speed * Time.deltaTime;
            transform.position = targetPos;

            if (TargetTime_decay <= Time.time)
            {
                Destroy(gameObject);
            }
        }
    }

    public void InstantiateText(int gold, int gem)
    {
        goldTextMesh.text = gold + " GOLD";
        gemTextMesh.text = gem + " GEM";
        if (mesh != null) { mesh.SetActive(true); }
        TargetTime_decay = Time.time + decayTime;

        isInstantiated = true;
    }
}
