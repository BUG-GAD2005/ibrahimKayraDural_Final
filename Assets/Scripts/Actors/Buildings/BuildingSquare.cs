using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSquare : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] SpriteRenderer SR_background;
    [SerializeField] SpriteRenderer SR_image;
    [SerializeField] SpriteRenderer SR_overlay;
    [SerializeField] LayerMask gridSquareLM;

    [Header("Values")]
    [SerializeField] float overlapRadius = .2f;

    public bool IsPlacable => _isPlacable;
    bool _isPlacable = true;

    public GridSquare OccupiedGridSquare => _occupiedGridSquare;
    GridSquare _occupiedGridSquare;

    GridSquare gridSquareBelow;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, overlapRadius, gridSquareLM);

        SR_overlay.color = new Color(1, 0, 0, .3f);
        _isPlacable = false;

        if (collider != null)
        {
            if (collider.TryGetComponent(out GridSquare gs) && gs.IsOccupied == false)
            {
                SR_overlay.color = new Color(0, 1, 0, .3f);
                gridSquareBelow = gs;
                _isPlacable = true;
            }
        }
    }

    public void Place()
    {
        if (gridSquareBelow == null || IsPlacable == false) return;

        _isPlacable = false;

        gridSquareBelow.Occupy();
        _occupiedGridSquare = gridSquareBelow;
        //transform.position = gridSquareBelow.transform.position;

        SR_background.sortingOrder = 11;
        SR_image.sortingOrder = 12;
        SR_overlay.sortingOrder = 13;
        SR_overlay.enabled = false;
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
