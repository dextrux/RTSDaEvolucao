using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDietMutation", menuName = "Mutation/Diet Mutation")]
public class MutationDiet : MutationBase
{
    [SerializeField] private PieceDiet _newDiet;
    public override void Mutate(Piece targetPiece)
    {
        SetMutation(targetPiece);
    }
    public void SetMutation(Piece targetPiece)
    {
        targetPiece.ChangeDiet(_newDiet);
    }
}
