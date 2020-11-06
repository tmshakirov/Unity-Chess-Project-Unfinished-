using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceScript : MonoBehaviour
{
    [SerializeField] private FigureColor figureColor;
    [SerializeField] private List<Material> colors;
    private MeshRenderer MR;

    public void SetColor (FigureColor _figureColor)
    {
        figureColor = _figureColor;
        MR = GetComponent<MeshRenderer>();
        MR.sharedMaterial = colors[(int)figureColor];
    }

    public void Move (Tile _tile)
    {
        transform.DOMove(new Vector3(_tile.GetX(), 0, _tile.GetY()), 0.5f);
    }
    public void MoveAndEat (Tile _tile, Figure _figure)
    {
        transform.DOMove(new Vector3(_tile.GetX(), 0, _tile.GetY()), 0.5f).OnComplete (() =>
        {
            Destroy(_figure.GetPiece().gameObject);
            AudioController.Instance.PlaySound("beat");
        });
    }
}
