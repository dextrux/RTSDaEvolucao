using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
[CreateAssetMenu(fileName = "New Mutation", menuName = "Mutation/NewMutation")]
public class MutationBase : ScriptableObject
{
    [SerializeField] protected int _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected bool _isSumMutation;
    [SerializeField] private bool _isDietMutation;
    [SerializeField] private PieceDiet _dietAdjust;
    [SerializeField] private bool _isVisual;
    [SerializeField] private int _meshLocation;
    [SerializeField] private Mesh _visual;
    [SerializeField] private Status[] _pieceStatusToChange;
    [SerializeField] private int[] _pieceValueToChange;

    public virtual void SetMutation(Piece targetPiece)
    {
        if (_isDietMutation) DietAdjust(targetPiece);
        if (_visual) AdjustVisual(targetPiece.GetComponent<MeshFilter>());
        Mutate(targetPiece);
    }
    private void Mutate(Piece targetPiece)
    {
        for (int i = 0; i < _pieceStatusToChange.Length; i++)
        {
            switch (_pieceStatusToChange[i])
            {
                case Status.Health:
                    if (_isSumMutation) targetPiece.Health.StatusAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Health.StatusAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Energy:
                    if (_isSumMutation) targetPiece.Energy.StatusAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Energy.StatusAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Fertility:
                    if (_isSumMutation) targetPiece.Fertility.StatusAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Fertility.StatusAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Strenght:
                    if (_isSumMutation) targetPiece.Strength.StatusAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Strength.StatusAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Hunger:
                    if (_isSumMutation) targetPiece.Hunger.StatusAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Hunger.StatusAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Humidity:
                    if (_isSumMutation) targetPiece.Humidity.IdealHumidityAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Humidity.IdealHumidityAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
                case Status.Temperature:
                    if (_isSumMutation) targetPiece.Temperature.IdealTemperatureAdjustSum(_pieceValueToChange[i]);
                    else targetPiece.Temperature.IdealTemperatureAdjustMultiplyPercent(_pieceValueToChange[i]);
                    break;
            }
        }
    }
    protected virtual int StatusBarAdjust(Piece targetPiece)
    {
        return 1;
    }
    private void DietAdjust(Piece targetPiece)
    {
        targetPiece.Diet = _dietAdjust;
    }
    protected void AdjustVisual(MeshFilter target)
    {
        target.mesh = _visual;
    }
}
