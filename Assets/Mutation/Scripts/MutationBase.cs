using System;
using UnityEngine;
public abstract class MutationBase : ScriptableObject, IComparable<MutationBase>
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;
    [SerializeField] private MutationBase[] _incompatibleMutations;
    [SerializeField] private MutationBase[] _requiredMutations;
    public MutationBase[] IncompatibleMutations { get { return _incompatibleMutations; } }
    public virtual void Mutate(Piece targetPiece)
    {

    }
    public int CompareTo(MutationBase comparable)
    {
        if (comparable._id > _id)
        {
            return -1;
        } else if (comparable._id == _id)
        {
            return 0;
        } else
        {
            return 1;
        }
    }
    public bool IsMutationUnlocked(Piece targetPiece)
    {
        if (targetPiece.AppliedMutations.Pesquisar(this)) return true;
        else return false;
    }
    public bool IsMutationUnlockable(Piece targetPiece)
    {
        for (int i = 0; i < _incompatibleMutations.Length; ++i)
        {
            if (targetPiece.AppliedMutations.Pesquisar(_incompatibleMutations[i])) return false;
        }
        return true;
    }
}
