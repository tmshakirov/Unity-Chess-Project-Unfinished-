using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [Header("Players")]
    public List<Player> players;
    [SerializeField] private int playerID;
    

    private Vector3 clickPos;
    [SerializeField] private Tile clickedTile;
    [SerializeField] private Figure chosenFigure;

    [Header("UI")]
    [SerializeField] private Image turnImage;
    [SerializeField] private List<Sprite> playerImages;

    private RaycastHit hit;
    private Ray ray;

    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            clickPos = hit.point;
            clickedTile = TileManager.Instance.GetClosestTile(new Vector2(clickPos.x, clickPos.z));
            if (clickedTile != null)
            {
                if (!FigureChosen())
                {
                    if (clickedTile.occupied)
                    {
                        //если на тайле есть фигура игрока, чей сейчас ход
                        if (FigureOnTile(clickedTile) != null && FigureOnTile(clickedTile).GetColor() == GetCurrentPlayer().color)
                        {
                            chosenFigure = FigureOnTile(clickedTile);
                            chosenFigure.Choose();
                        }
                    }
                }
                else
                {
                    if (clickedTile.occupied)
                    {
                        //если на тайле есть фигура
                        if (FigureOnTile(clickedTile) != null)
                        {
                            //если фигура на тайле принадлежит игроку, чей сейчас ход, мы ее перевыбираем, иначе едим
                            if (FigureOnTile(clickedTile).GetColor() == GetCurrentPlayer().color)
                            {
                                chosenFigure = FigureOnTile(clickedTile);
                                chosenFigure.Choose();
                            }
                            else
                            {                                
                                EatFigure(FigureOnTile(clickedTile));
                            }
                        }
                    }
                    else
                    {
                        if (!KingUnderAttack() || chosenFigure.GetFigureType() == FigureType.KING)
                        {
                            if (chosenFigure.Move(clickedTile))
                                NextTurn();
                        }
                    }
                }
            }
        }
    }

    private bool KingUnderAttack()
    {
        return GetCurrentPlayer().figures.Find(x => (x.GetFigureType() == FigureType.KING) && (x.UnderAttack())) != null;
    }

    public bool UnderAttack(Figure _figure)
    {
        foreach (var p in players)
        {
            if (p != GetCurrentPlayer())
            {
                foreach (var f in p.figures)
                {
                    if (f.GetAvailableTiles().Contains(_figure.GetTile()))
                        return true;
                }
            }
        }
        return false;
    }

    private void EatFigure (Figure _figure)
    {
        if (_figure.GetFigureType() != FigureType.KING) //короля съесть нельзя
        {
            if (chosenFigure.MoveAndEat(clickedTile, _figure))
            {
                _figure.GetPlayer().RemoveFigure(_figure);
                NextTurn();
            }
        }
    }

    private bool FigureChosen()
    {
        return chosenFigure != null && chosenFigure.GetPiece() != null;
    }

    public Figure FigureOnTile(Tile _tile)
    {
        foreach (var p in players)
        {
            if (p.figures.Find(x => x.GetTile() == _tile) != null)
                return p.figures.Find(x => x.GetTile() == _tile);
        }
        return null;
    }

    public void NextTurn()
    {
        clickedTile = null;
        chosenFigure = null;
        players[playerID].isActive = false;
        playerID++;
        if (playerID > players.Count - 1)
            playerID = 0;
        players[playerID].isActive = true;
        turnImage.sprite = playerImages[playerID];
    }

    public Player GetCurrentPlayer()
    {
        return players[playerID];
    }
}
