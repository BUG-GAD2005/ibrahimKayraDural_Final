using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSquare : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] SpriteRenderer SR_image;
    [SerializeField] SpriteRenderer SR_overlay;
    [SerializeField] LayerMask gridSquareLM;

    [Header("Values")]
    [SerializeField] float overlapRadius = .2f;

    bool _isPlacable;
    public bool IsPlacable => _isPlacable;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, overlapRadius, gridSquareLM);

        SR_overlay.color = new Color(1, 0, 0, .3f);

        if (collider != null)
        {
            if (collider.TryGetComponent(out GridSquare gs) && gs.IsOccupied == false)
            {
                SR_overlay.color = new Color(0, 1, 0, .3f);
            }
        }
    }

    public void SetImage(SO_Building data)
    {
        SR_image.sprite = data.BuildingSprite;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
