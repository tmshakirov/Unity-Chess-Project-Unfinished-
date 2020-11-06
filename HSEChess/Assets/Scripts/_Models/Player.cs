using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Player
{
    public bool isActive;
    public FigureColor color;
    public List<Figure> figures = new List<Figure>();

    public void RemoveFigure (Figure _figure)
    {
        figures.Remove(_figure);
    }
}
