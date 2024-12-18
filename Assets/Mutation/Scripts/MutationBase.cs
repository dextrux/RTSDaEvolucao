using System;
using UnityEngine;
public abstract class MutationBase : ScriptableObject, IComparable<MutationBase>
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _cost;
    [SerializeField] private string _description;
    [SerializeField] private MutationBase[] _incompatibleMutations;
    [SerializeField] private MutationBase[] _requiredMutations;
    public MutationBase[] IncompatibleMutations { get { return _incompatibleMutations; } }
    public int Id { get { return _id; } }
    public int Cost { get { return _cost; } }
    public Sprite Icon { get { return _icon; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public virtual void Mutate(Piece targetPiece)
    {

    }
    public bool IsMutationUnlocked(Piece targetPiece)
    {
        if (targetPiece.AppliedMutations.Contains(this)) return true;
        else return false;
    }
    public bool IsMutationUnlockable(Piece targetPiece)
    {
        if (targetPiece.AppliedMutations.Contains(this)) return false;
        for (int i = 0; i < _incompatibleMutations.Length; ++i)
        {
            if (targetPiece.AppliedMutations.Contains(_incompatibleMutations[i])) return false;
        }
        for (int i = 0; i < _requiredMutations.Length; i++)
        {
            
                if (!(targetPiece.AppliedMutations.Contains(_requiredMutations[i])))  return false;
        }
        return true;
    }
    public int CompareTo(MutationBase other)
    {
        return Id.CompareTo(other.Id);
    }
}
