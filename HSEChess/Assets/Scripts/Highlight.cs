using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Tile thisTile;
    [SerializeField] private MeshRenderer MR;

    private void Awake()
    {
        MR = GetComponent<MeshRenderer>();
    }

    public void SetTile (Tile _tile)
    {
        thisTile = _tile;
    }

    public void ChangeState (bool _activate)
    {
        MR.enabled = _activate;
    }
}
