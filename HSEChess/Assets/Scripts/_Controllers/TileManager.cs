using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private List<Tile> tiles = new List<Tile>();
    [SerializeField] private const float tileOffset = 0.5f;
    [SerializeField] private const float tileSize = 1f;

    void Start()
    {
        StartCoroutine(InitializeTiles());
    }

    //(0, 0) - первая клетка, (7, 7) - последняя
    public Tile GetTile (Vector2 _pos)
    {
        _pos = new Vector2(_pos.x + tileOffset, _pos.y + tileOffset);
        Tile tile = tiles.Find(x => x.GetPosition() == _pos);
        return tile;
    }

    //получаем ближайший к клику мыши тайл
    public Tile GetClosestTile (Vector2 _pos)
    {
        Tile closestTile = null;
        foreach (var t in tiles)
        {
            if (t.ContainsPos (_pos))
            {
                closestTile = t;
            }
        }
        return closestTile;
    }

    //получаем amount тайлов только впереди тайла. Нужно для пешек
    public List<Tile> FrontTiles(Tile _tile, FigureColor _color, int _amount = 1)
    {
        int amount = 1;
        List<Tile> tilesInLine = new List<Tile>();
        Vector2 tilePos = _tile.GetPosition();
        if (_color == FigureColor.WHITE)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].GetX() == tilePos.x && PawnCondition(tiles[i], tilePos, amount))
                {
                    tilesInLine.Add(tiles[i]);
                    amount++;
                }
                if (amount > _amount)
                    break;
            }
        }
        else
        {
            for (int i = tiles.Count-1; i > -1; i--)
            {
                if (tiles[i].GetX() == tilePos.x && PawnCondition(tiles[i], tilePos, amount))
                {
                    tilesInLine.Add(tiles[i]);
                    amount++;
                }
                if (amount > _amount)
                    break;
            }
        }
        return tilesInLine;
    }

    private bool PawnCondition(Tile _tile, Vector2 _tilePos, int _amount)
    {
        return (GameController.Instance.GetCurrentPlayer().color == FigureColor.WHITE && _tile.GetY() > _tilePos.y
            || GameController.Instance.GetCurrentPlayer().color == FigureColor.BLACK && _tile.GetY() < _tilePos.y) && Mathf.Abs(_tilePos.y - _tile.GetY()) == _amount;
    }

    //получаем amount тайлов слева и справа от тайла
    public List<Tile> HorizontalTiles(Tile _tile, FigureColor _color, int _amount = 100)
    {
        List<Tile> tilesInLine = new List<Tile>();
        Vector2 tilePos = _tile.GetPosition();
        if (_color == FigureColor.WHITE)
        {
            for (int i = 0; i < tiles.Count(); i++)
            {
                if (tiles[i].GetY() == tilePos.y)
                {
                    tilesInLine.Add(tiles[i]);
                    _amount--;
                }
                if (_amount < 1)
                    break;
            }
        }
        else
        {
            for (int i = tiles.Count()-1; i > -1; i--)
            {
                if (tiles[i].GetY() == tilePos.y)
                {
                    tilesInLine.Add(tiles[i]);
                    _amount--;
                }
                if (_amount < 1)
                    break;
            }
        }
        return tilesInLine;
    }

    public int TileIndexX(Tile _tile)
    {
        return Mathf.FloorToInt(_tile.GetX() - 0.5f);
    }
    public int TileIndexY (Tile _tile)
    {
        return Mathf.FloorToInt(_tile.GetY()-0.5f);
    }

    //получаем amount тайлов спереди и сзади от тайла
    public List<Tile> VerticalTiles (Tile _tile, FigureColor color, int amount = 100)
    {
        List<Tile> tilesInLine = new List<Tile>();
        Vector2 tilePos = _tile.GetPosition();
        if (color == FigureColor.WHITE)
        {
            for (int i = 0; i < tiles.Count(); i++)
            {
                if (tiles[i].GetX() == tilePos.x)
                {
                    tilesInLine.Add(tiles[i]);
                    amount--;
                }
                if (amount < 1)
                    break;
            }
        }
        else
        {
            for (int i = tiles.Count() - 1; i > -1; i--)
            {
                if (tiles[i].GetX() == tilePos.x)
                {
                    tilesInLine.Add(tiles[i]);
                    amount--;
                }
                if (amount < 1)
                    break;
            }
        }
        return tilesInLine;
    }

    //получаем amount тайлов по диагонали к тайлу
    public List<Tile> DiagonalTiles(Tile _tile, int _amount = 100)
    {
        List<Tile> tilesInLine = new List<Tile>();
        Vector2 tilePos = _tile.GetPosition();
        foreach (var tile in tiles)
        {
            if (Mathf.Abs(tile.GetX() - tilePos.x) == Mathf.Abs(tile.GetY() - tilePos.y))
            {
                tilesInLine.Add(tile);
                _amount--;
            }
            if (_amount < 1)
                break;
        }
        return tilesInLine;
    }

    IEnumerator InitializeTiles()
    {
        float tileOffsetX = tileOffset;
        float tileOffsetY = tileOffset;
        Tile newTile = null;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                newTile = new Tile(new Vector2(tileOffsetX, tileOffsetY));
                tiles.Add(newTile);
                HighlightController.Instance.AddTile(newTile);
                tileOffsetX += tileSize;
            }
            tileOffsetX = tileOffset;
            tileOffsetY += tileSize;
            yield return new WaitForEndOfFrame();
        }
        FigureSpawner.Instance.SpawnFigures();
    }
}
