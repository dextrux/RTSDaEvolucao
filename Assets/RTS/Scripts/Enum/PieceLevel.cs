using System.Collections.Generic;
using UnityEngine;
public struct LevelAttributes
{
    public int Health;
    public int Energy;
    public int Strength;

    public LevelAttributes(int health, int energy, int strength)
    {
        Health = health;
        Energy = energy;
        Strength = strength;
    }
}

public static class PieceLevelHandler
{
    // Dicionário de atributos de nível
    private static readonly Dictionary<int, LevelAttributes> levelAttributes = new Dictionary<int, LevelAttributes>
    {
        { 1, new LevelAttributes(100, 100, 20) },
        { 2, new LevelAttributes(300, 200, 20) },
        { 3, new LevelAttributes(900, 400, 30) }
    };

    public static void SetPieceAttributes(Piece piece, int level)
    {
        if (levelAttributes.TryGetValue(level, out var attributes))
        {
            piece.Health.MaxBarValue = attributes.Health;
            piece.Energy.MaxBarValue = attributes.Energy;
            piece.Strength.CurrentBarValue = attributes.Strength;
        }
        else
        {
            Debug.LogWarning("Nível da peça inválido.");
        }

        piece.Hunger.CurrentBarValue = 100;
        piece.Fertility.CurrentBarValue = 0;
    }
}

