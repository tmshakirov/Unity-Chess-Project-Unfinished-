using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;

public class FigureSpawner : SerializedSingleton<FigureSpawner>
{
    [SerializeField] private Dictionary<FigureType, PieceScript> figureModels;
    [SerializeField] private GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnFigures ()
    {
        //спавн белых
        SpawnFigure(FigureType.ROOK, TileManager.Instance.GetTile(new Vector2(0, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.KNIGHT, TileManager.Instance.GetTile(new Vector2(1, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.BISHOP, TileManager.Instance.GetTile(new Vector2(2, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.QUEEN, TileManager.Instance.GetTile(new Vector2(3, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.KING, TileManager.Instance.GetTile(new Vector2(4, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.BISHOP, TileManager.Instance.GetTile(new Vector2(5, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.KNIGHT, TileManager.Instance.GetTile(new Vector2(6, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.ROOK, TileManager.Instance.GetTile(new Vector2(7, 0)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(0, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(1, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(2, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(3, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(4, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(5, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(6, 1)), FigureColor.WHITE);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(7, 1)), FigureColor.WHITE);
        //спавн черных
        SpawnFigure(FigureType.ROOK, TileManager.Instance.GetTile(new Vector2(0, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.KNIGHT, TileManager.Instance.GetTile(new Vector2(1, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.BISHOP, TileManager.Instance.GetTile(new Vector2(2, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.QUEEN, TileManager.Instance.GetTile(new Vector2(3, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.KING, TileManager.Instance.GetTile(new Vector2(4, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.BISHOP, TileManager.Instance.GetTile(new Vector2(5, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.KNIGHT, TileManager.Instance.GetTile(new Vector2(6, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.ROOK, TileManager.Instance.GetTile(new Vector2(7, 7)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(0, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(1, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(2, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(3, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(4, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(5, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(6, 6)), FigureColor.BLACK);
        SpawnFigure(FigureType.PAWN, TileManager.Instance.GetTile(new Vector2(7, 6)), FigureColor.BLACK);
    }

    private void SpawnFigure(FigureType _figure, Tile _tile, FigureColor _color)
    {
        Figure figure = new Figure(_figure, _tile, _color);
        PieceScript _piece = null;
        if (figureModels.TryGetValue(_figure, out _piece))
        {
            var piece = Instantiate(_piece, new Vector3(_tile.GetX(), 0, _tile.GetY()), transform.rotation);
            piece.SetColor(_color);
            figure.SetPiece(piece);
            piece.transform.SetParent(board.transform);
            var _thisColorPlayer = GameController.Instance.players.Find(x => x.color == _color);
            if (_thisColorPlayer != null)
                _thisColorPlayer.figures.Add(figure);
        }
    }
}
