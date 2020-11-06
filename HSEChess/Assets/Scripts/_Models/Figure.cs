using System;
using System.Collections.Generic;
using UnityEngine;

public enum FigureType { PAWN, KNIGHT, BISHOP, ROOK, KING, QUEEN }
public enum FigureColor { WHITE, BLACK }

[Serializable]
public class Figure
{
    [SerializeField] protected bool chosen;
    [SerializeField] protected FigureType figureType;
    [SerializeField] protected FigureColor figureColor;
    [SerializeField] protected Vector2 position;
    [SerializeField] protected Tile tile;
    [SerializeField] protected PieceScript piece;

    public FigureColor GetColor()
    {
        return figureColor;
    }

    public Tile GetTile()
    {
        return tile;
    }

    private bool InDefaultPos()
    {
        return tile.GetY() == 1.5f && figureColor == FigureColor.WHITE || tile.GetY() == 6.5f && figureColor == FigureColor.BLACK;
    }

    public List<Tile> GetAvailableTiles()
    {
        List<Tile> availableTiles = new List<Tile>();
        List<Tile> verticalTiles = new List<Tile>();
        List<Tile> horizontalTiles = new List<Tile>();
        List<Tile> diagonalTiles = new List<Tile>();
        switch (figureType)
        {
            case FigureType.PAWN:
                List<Tile> tiles = new List<Tile>();
                if (InDefaultPos())
                {
                    tiles = TileManager.Instance.FrontTiles(tile, figureColor, 2);
                }
                else
                {
                    tiles = TileManager.Instance.FrontTiles(tile, figureColor);
                }
                if (figureColor == FigureColor.WHITE)
                    diagonalTiles = GetDiagonalTiles(TileManager.Instance.DiagonalTiles(tile), 1, 0, 1, 0, true);
                else
                    diagonalTiles = GetDiagonalTiles(TileManager.Instance.DiagonalTiles(tile), 0, 1, 0, 1, true);
                foreach (var t in tiles)
                {
                    if (t.occupied)
                        break;
                    availableTiles.Add(t);
                }
                availableTiles.AddRange(diagonalTiles);
                break;
            case FigureType.ROOK:
                verticalTiles = GetVerticalTiles(TileManager.Instance.VerticalTiles(tile, figureColor));
                horizontalTiles = GetHorizontalTiles(TileManager.Instance.HorizontalTiles(tile, figureColor));
                availableTiles.AddRange(verticalTiles);
                availableTiles.AddRange(horizontalTiles);
                break;
            case FigureType.QUEEN:
                verticalTiles = GetVerticalTiles(TileManager.Instance.VerticalTiles(tile, figureColor));
                horizontalTiles = GetHorizontalTiles(TileManager.Instance.HorizontalTiles(tile, figureColor));
                availableTiles.AddRange(verticalTiles);
                availableTiles.AddRange(horizontalTiles);
                diagonalTiles = GetDiagonalTiles(TileManager.Instance.DiagonalTiles(tile));
                availableTiles.AddRange(diagonalTiles);
                break;
            case FigureType.BISHOP:
                diagonalTiles = GetDiagonalTiles(TileManager.Instance.DiagonalTiles(tile));
                availableTiles.AddRange(diagonalTiles);
                break;
            case FigureType.KING:
                verticalTiles = GetVerticalTiles(TileManager.Instance.VerticalTiles(tile, figureColor), 1, 1);
                horizontalTiles = GetHorizontalTiles(TileManager.Instance.HorizontalTiles(tile, figureColor), 1, 1);
                availableTiles.AddRange(verticalTiles);
                availableTiles.AddRange(horizontalTiles);
                diagonalTiles = GetDiagonalTiles(TileManager.Instance.DiagonalTiles(tile), 1, 1, 1, 1);
                availableTiles.AddRange(diagonalTiles);
                break;
        }
        return availableTiles;
    }

    private List<Tile> GetVerticalTiles(List<Tile> _tiles, int _front = 8, int _back = 8)
    {
        List<Tile> tiles = new List<Tile>();
        int y = TileManager.Instance.TileIndexY(tile);
        if (figureColor == FigureColor.BLACK)
            _tiles.Sort((a, b) => b.GetY().CompareTo(a.GetY()));
        else
            _tiles.Sort((a, b) => a.GetY().CompareTo(b.GetY()));
        for (int i = figureColor == FigureColor.WHITE ? y : (8-y); i < 8; i++)
        {
            var t = _tiles[i];
            if (t != tile)
            {
                if (_front < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                        tiles.Add(t);
                    break;
                }
                else
                {
                    tiles.Add(t);
                    _front--;
                }
            }
        }
        if (figureColor == FigureColor.BLACK)
            _tiles.Sort((a, b) => b.GetY().CompareTo(a.GetY()));
        else
            _tiles.Sort((a, b) => a.GetY().CompareTo(b.GetY()));
        for (int i = y; i > -1; i--)
        {
            var t = _tiles[y-i];
            if (t != tile)
            {
                if (_back < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                    {
                        tiles.Add(t);
                    }
                    else
                        break;
                }
                else
                {
                    tiles.Add(t);
                    _back--;
                }
            }
        }
        return tiles;
    }
    private List<Tile> GetHorizontalTiles(List<Tile> _tiles, int _left = 8, int _right = 8)
    {
        List<Tile> tiles = new List<Tile>();
        int x = TileManager.Instance.TileIndexX(tile);
        for (int i = figureColor == FigureColor.WHITE ? x : (8 - x); i < 8; i++)
        {
            var t = _tiles[i];
            if (t != tile)
            {
                if (_left < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                        tiles.Add(t);
                    else
                        break;
                }
                else
                {
                    tiles.Add(t);
                    _left--;
                }
            }
        }
        for (int i = x; i > -1; i--)
        {
            var t = _tiles[i];
            if (t != tile)
            {
                if (_right < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                        tiles.Add(t);
                    else
                        break;
                }
                else
                {
                    tiles.Add(t);
                    _right--;
                }
            }
        }
        return tiles;
    }

    private List<Tile> GetDiagonalTiles(List<Tile> _tiles, int _left = 8, int _right = 8, int _front = 8, int _back = 8, bool _onlyOccupied = false)
    {
        List<Tile> tiles = new List<Tile>();
        foreach (var t in _tiles)
        {
            if (t.GetX() < tile.GetX() && t.GetY() > tile.GetY())
            {
                if (_left < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                    {
                        tiles.Add(t);
                    }
                    break;
                }
                else
                {
                    if (!_onlyOccupied)
                        tiles.Add(t);
                    _left--;
                }
            }
        }
        _tiles.Sort((a, b) => b.GetX().CompareTo(a.GetX()));
        foreach (var t in _tiles)
        {
            if (t.GetX() < tile.GetX() && t.GetY() < tile.GetY())
            {
                if (_right < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                    {
                        tiles.Add(t);
                    }
                    break;
                }
                else
                {
                    if (!_onlyOccupied)
                        tiles.Add(t);
                    _right--;
                }
            }
        }
        _tiles.Sort((a, b) => a.GetX().CompareTo(b.GetX()));
        foreach (var t in _tiles)
        {
            if (t.GetX() > tile.GetX() && t.GetY() > tile.GetY())
            {
                if (_front < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                    {
                        tiles.Add(t);
                    }
                    break;
                }
                else
                {
                    if (!_onlyOccupied)
                        tiles.Add(t);
                    _front--;
                }
            }
        }
        _tiles.Sort((a, b) => a.GetX().CompareTo(b.GetX()));
        foreach (var t in _tiles)
        {
            if (t.GetX() > tile.GetX() && t.GetY() < tile.GetY())
            {
                if (_back < 1)
                    break;
                if (t.occupied)
                {
                    if (GameController.Instance.FigureOnTile(t).GetColor() != figureColor)
                    {
                        tiles.Add(t);
                    }
                    break;
                }
                else
                {
                    if (!_onlyOccupied)
                        tiles.Add(t);
                    _back--;
                }
            }
        }
        return tiles;
    }

    public bool IsAvailableTile (Tile _tile)
    {
        return GetAvailableTiles().Contains(_tile);
    }

    public bool UnderAttack()
    {
        return GameController.Instance.UnderAttack(this);
    }

    public bool Move(Tile _tile)
    {
        if (GetAvailableTiles().Contains(_tile))
        {
            tile.occupied = false;
            _tile.occupied = true;
            tile = _tile;
            piece.Move(_tile);
            HighlightController.Instance.ClearHighlights();
            AudioController.Instance.PlaySound(figureColor == FigureColor.WHITE ? "white" : "black");
            return true;
        }
        return false;
    }

    public bool MoveAndEat(Tile _tile, Figure _figure)
    {
        if (GetAvailableTiles().Contains(_tile))
        {
            tile.occupied = false;
            tile = _tile;
            piece.MoveAndEat(_tile, _figure);
            AudioController.Instance.PlaySound(figureColor == FigureColor.WHITE ? "white" : "black");
            HighlightController.Instance.ClearHighlights();
            return true;
        }
        return false;
    }

    public bool IsChosen()
    {
        return chosen;
    }

    public void Choose()
    {
        chosen = true;
        HighlightController.Instance.HighlightTiles(GetAvailableTiles());
    }

    public FigureType GetFigureType()
    {
        return figureType;
    }

    public PieceScript GetPiece()
    {
        return piece;
    }

    public Player GetPlayer()
    {
        return GameController.Instance.players.Find(x => x.figures.Contains(this));
    }

    public Figure (FigureType _figureType, Tile _tile, FigureColor _figureColor)
    {
        figureType = _figureType;
        tile = _tile;
        tile.occupied = true;
        figureColor = _figureColor;
        position = _tile.GetPosition();
    }
    public void SetPiece (PieceScript _piece)
    {
        piece = _piece;
    }
}
