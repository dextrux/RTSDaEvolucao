using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public void MoveToTile(GameObject newTile, GameObject oldTile, Jubileuson piece, TileType newTileType)
    {
        piece.gameObject.transform.position = new Vector3(newTile.transform.position.x, 1, newTile.transform.position.z);
        ChangeTileStatus(oldTile, TileType.Vazio);
        ChangeTileStatus(newTile, newTileType);
    }

    public void Eat(Jubileuson piece, FoodTotem totem, GameObject newTile, GameObject oldTile)
    {
        if (totem.GetCurrentTotemType() == CurrentTotemType.NonMeatFood)
        {
            piece.GetEnergyBar().SetNewBarValue(piece.GetEnergyBar().GetCurrentBarValue() + totem.GetNonMeatFood().GetRemainingVeggieValue());
        }
        else if (totem.GetCurrentTotemType() == CurrentTotemType.MeatFood)
        {
            piece.GetEnergyBar().SetNewBarValue(piece.GetEnergyBar().GetCurrentBarValue() + totem.GetMeatFood().GetRemainingMeatValue());
        }
        MoveToTile(newTile, oldTile, piece, TileType.Próprio);
    }

    public void Fight(Jubileuson invader, Jubileuson holder, GameObject holderTile, GameObject invaderTile)
    {
        if (invader.GetStrenghtBar().GetCurrentBarValue() >= holder.GetHealthBar().GetCurrentBarValue() && holder.GetStrenghtBar().GetCurrentBarValue() >= invader.GetHealthBar().GetCurrentBarValue())
        {
            Destroy(holder);
            Destroy(invader);
            ChangeTileStatus(holderTile, TileType.Vazio);
            ChangeTileStatus(invaderTile, TileType.Vazio);
        }
        else if (invader.GetStrenghtBar().GetCurrentBarValue() >= holder.GetHealthBar().GetCurrentBarValue())
        {
            Destroy(holder);
            invader.GetHealthBar().SetNewBarValue(holder.GetHealthBar().GetCurrentBarValue() - holder.GetStrenghtBar().GetCurrentBarValue());
            MoveToTile(holderTile, invaderTile, invader, invaderTile.GetComponent<Tile>().GetTileType());
        }
        else if (holder.GetStrenghtBar().GetCurrentBarValue() >= invader.GetHealthBar().GetCurrentBarValue())
        {
            Destroy(invader);
            holder.GetHealthBar().SetNewBarValue(holder.GetHealthBar().GetCurrentBarValue() - invader.GetStrenghtBar().GetCurrentBarValue());
            ChangeTileStatus(invaderTile, TileType.Vazio);
        }
        else
        {
            invader.GetHealthBar().SetNewBarValue(holder.GetHealthBar().GetCurrentBarValue() - holder.GetStrenghtBar().GetCurrentBarValue());
            holder.GetHealthBar().SetNewBarValue(holder.GetHealthBar().GetCurrentBarValue() - invader.GetStrenghtBar().GetCurrentBarValue());
        }
    }

    public void ChangeTileStatus(GameObject tile, TileType type)
    {
        tile.GetComponent<Tile>().SetTileType(type);
    }
    private GameObject selectedTile1;
    private GameObject selectedTile2;
    private Jubileuson currentPiece;

    public void SelectTile(GameObject tile, Jubileuson piece)
    {
        if (selectedTile1 == null)
        {
            selectedTile1 = tile;
            currentPiece = piece; // Store the current piece for action
        }
        else if (selectedTile2 == null)
        {
            selectedTile2 = tile;
            ExecuteAction();
        }
    }

    private void ExecuteAction()
    {
        TileType tile1Type = selectedTile1.GetComponent<Tile>().GetTileType();
        TileType tile2Type = selectedTile2.GetComponent<Tile>().GetTileType();

        if (tile1Type == TileType.Próprio && tile2Type == TileType.Vazio)
        {
            MoveToTile(selectedTile2, selectedTile1, currentPiece, tile2Type);
        }
        else if (tile1Type == TileType.Vazio && tile2Type == TileType.Próprio)
        {
            MoveToTile(selectedTile1, selectedTile2, currentPiece, tile1Type);
        }
        else if (tile1Type == TileType.Próprio && tile2Type == TileType.Comida) // Assuming TileType.Alimento is a food tile type
        {
            Eat(currentPiece, selectedTile2.GetComponent<FoodTotem>(), selectedTile1, selectedTile2);
        }
        else if (tile1Type == TileType.Próprio && tile2Type == TileType.Inimigo) // Assuming TileType.Oponente is an enemy tile type
        {
            Fight(currentPiece, selectedTile2.GetComponent<Jubileuson>(), selectedTile1, selectedTile2);
        }

        // Reset selected tiles
        selectedTile1 = null;
        selectedTile2 = null;
    }

    // ... existing methods (MoveToTile, Eat, Fight, ChangeTileStatus)
}
