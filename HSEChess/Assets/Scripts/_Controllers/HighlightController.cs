using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : Singleton<HighlightController>
{
    [SerializeField] private Highlight highlightPrefab;
    [SerializeField] private List<Highlight> allHighlights;
    [SerializeField] private List<Highlight> activeHighlights;

    public void ClearHighlights()
    {
        foreach (var h in activeHighlights)
            h.ChangeState(false);
        activeHighlights.Clear();
    }

    public void HighlightTiles (List<Tile> tiles)
    {
        ClearHighlights();
        foreach (var h in allHighlights)
        {
            if (tiles.Contains(h.thisTile))
            {
                h.ChangeState(true);
                activeHighlights.Add(h);
            }
        }
    }

    public void AddTile (Tile _tile)
    {
        var h = Instantiate(highlightPrefab, new Vector3(_tile.GetX(), 0.01f, _tile.GetY()), transform.rotation);
        h.SetTile(_tile);
        h.ChangeState(false);
        allHighlights.Add(h);
    }
}
