using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TilePanelInfoDealer : MonoBehaviour
{
    public GameObject targetTile;
    TMP_Text _TileBiomeStatusTxt;
    TMP_Text _TileHuntStatusTxt;
    TMP_Text _TileFruitsStatusTxt;
    public GameObject TileBiomeStatus;
    public GameObject TileHuntStatus;
    public GameObject TileFruitsStatus;

    private void Start()
    {
        _TileBiomeStatusTxt = TileBiomeStatus.GetComponent<TMP_Text>();
        _TileHuntStatusTxt = TileHuntStatus.GetComponent<TMP_Text>();
        _TileFruitsStatusTxt = TileFruitsStatus.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _TileBiomeStatusTxt.text = targetTile.GetComponent<Tile>().GetTileBiomeStatusString();
        _TileHuntStatusTxt.text = targetTile.GetComponent<Tile>().GetTileHuntStatusString();
        _TileFruitsStatusTxt.text = targetTile.GetComponent <Tile>().GetTileFruitsStatusString();
    }
}
