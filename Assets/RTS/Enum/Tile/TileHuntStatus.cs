using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileHuntStatus
{
    TileHuntStatusVazio,         // 0% ou quase nenhuma TileHuntStatus
    TileHuntStatusEscassa,         // 1% - 25% TileHuntStatus disponível
    TileHuntStatusModerada,        // 26% - 50% TileHuntStatus disponível
    TileHuntStatusAbundante,       // 51% - 75% TileHuntStatus disponível
    TileHuntStatusGeneralizada    // 76% - 100% TileHuntStatus disponível
}

