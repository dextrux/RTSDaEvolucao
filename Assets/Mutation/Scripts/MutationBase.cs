using UnityEngine;
public abstract class MutationBase : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private MutationBase[] _incompatibleMutations;
    [SerializeField] private MutationBase[] _requiredMutations;
    public MutationBase[] IncompatibleMutations { get { return _incompatibleMutations; } }
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
        for (int i = 0; i < _incompatibleMutations.Length; ++i)
        {
            if (targetPiece.AppliedMutations.Contains(_incompatibleMutations[i])) return false;
        }
        return true;
    }
}
