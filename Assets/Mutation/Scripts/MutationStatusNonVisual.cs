using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewVisualStatusMutation", menuName = "Mutation/Non Visual Status Mutation")]
public class MutationStatusNonVisual : MutationBase
{
    [SerializeField] private Status[] _pieceStatusToChange;
    [SerializeField] private int[] _pieceValueToChange;
    public override void Mutate(Piece targetPiece)
    {
        ChangeStatus(targetPiece);
        targetPiece.AddMutation(this);
    }
    private void ChangeStatus(Piece targetPiece)
    {
        for (int i = 0; i < _pieceStatusToChange.Length; i++)
        {
            switch (_pieceStatusToChange[i])
            {
                case Status.Health:
                    targetPiece.Health.StatusAdjustSum(_pieceValueToChange[i]);
                    break;
                case Status.Energy:
                    targetPiece.Energy.StatusAdjustSum(_pieceValueToChange[i]);
                    break;
                case Status.Fertility:
                    targetPiece.Fertility.StatusAdjustSum(_pieceValueToChange[i]);
                    break;
                case Status.Strenght:
                    targetPiece.Strength.StatusAdjustSum(_pieceValueToChange[i]);
                    break;
                case Status.Hunger:
                    targetPiece.Hunger.StatusAdjustSum(_pieceValueToChange[i]);
                    break;
                case Status.Humidity:
                    targetPiece.Humidity.IncreaseByValue(_pieceValueToChange[i]);
                    break;
                case Status.Temperature:
                    targetPiece.Temperature.IncreaseByValue(_pieceValueToChange[i]);
                    break;
            }
        }
    }
}
