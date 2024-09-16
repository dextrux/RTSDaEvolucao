using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileHuntStatus
{
    TileHuntStatusVazio,         // 0% ou quase nenhuma TileHuntStatus
    TileHuntStatusEscassa,         // 1% - 25% TileHuntStatus dispon�vel
    TileHuntStatusModerada,        // 26% - 50% TileHuntStatus dispon�vel
    TileHuntStatusAbundante,       // 51% - 75% TileHuntStatus dispon�vel
    TileHuntStatusGeneralizada    // 76% - 100% TileHuntStatus dispon�vel
}

