using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVisualStatusMutation", menuName = "Mutation/Visual Status Mutation")]
public class MutationStatusVisual : MutationStatusNonVisual
{
    [SerializeField] private PieceParts _partToChange;
    [SerializeField] private Mesh _visual;
    public override void Mutate(Piece targetPiece)
    {
        base.Mutate(targetPiece);
        SetVisual(targetPiece);
    }
    private void SetVisual(Piece targetPiece)
    {
        //targetPiece.SetVisualPart(_partToChange, _visual);
    }
}
