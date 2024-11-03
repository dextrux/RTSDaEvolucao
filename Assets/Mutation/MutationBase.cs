using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
public abstract class MutationBase : ScriptableObject
{
    protected int _id;
    protected string _name;
    protected string _description;
    protected float _temperatureAdjust;
    protected float _humidityAdjust;
    protected float _healtBarAdjust;
    protected float _fertilityAdjust;
    protected float _strenghtAdjust;
    [SerializeField] private bool _IsDietMutation;
    [SerializeField] private PieceDiet _dietAdjust;
    [SerializeField] private bool _isVisual;
    [SerializeField] private int _meshLocation;
    [SerializeField] private Mesh _visual;

    public virtual void SetMutation(Piece targetPiece)
    {
        if (_IsDietMutation) DietAdjust(targetPiece);
        if (_visual) AdjustVisual(targetPiece.GetComponent<MeshFilter>());
    }
    protected abstract int StatusBarAdjust(Piece targetPiece);
    protected void DietAdjust(Piece targetPiece)
    {
        targetPiece.Diet = _dietAdjust;
    }
    protected void AdjustVisual(MeshFilter target)
    {
        target.mesh = _visual;
    }
}
