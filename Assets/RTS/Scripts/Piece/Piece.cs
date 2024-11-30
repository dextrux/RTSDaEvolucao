using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    #region Constantes e Configurações
    private const float _rayDistanceTile = 10f;
    private const float RestDuration = 3f; // Duração do descanso
    [SerializeField] private LayerMask TileLayerMask;
    BiomeReferences _biomeReferences;
    OwnerReference _ownerReference;
    TileTypeReferences _tileTypeReferences;
    Renderer _renderer;
    #endregion

    #region Atributos Gerais
    [SerializeField] private int _pontosMutagenicos;
    [SerializeField] private PieceDiet _diet;
    [SerializeField] private Owner _owner;
    [SerializeField] private List<Alerta> _alerta;
    #endregion

    #region Atributos de Status
    [SerializeField] private StatusBar _healthBar = new StatusBar(100, 100);
    [SerializeField] private StatusBar _energyBar = new StatusBar(100, 100);
    [SerializeField] private StatusBar _fertilityBar = new StatusBar(100, 0);
    [SerializeField] private StatusBar _strength = new StatusBar(100, 100);
    [SerializeField] private StatusBar _hungerBar = new StatusBar(100, 100);
    #endregion

    #region Atributos Ambientais
    [SerializeField] private EnviromentStatus _humidity;
    [SerializeField] private EnviromentStatus _temperature;
    #endregion

    #region Atributos de Comportamento
    [SerializeField] private bool _isResting;
    [SerializeField] private bool _isUnderDisastre;
    [SerializeField] private bool _isDoente;
    [SerializeField] private bool _isDuringAction;
    #endregion

    #region Mutação e Multiplicadores
    private int sickCounter;
    private List<MutationBase> _appliedMutations = new List<MutationBase>();
    private List<MutationBase> _incompatibleMutations = new List<MutationBase>();
    private float _huntMultiplier;
    private float _plantMultiplier;
    #endregion

    #region Propriedades
    public int PontosMutagenicos { get => _pontosMutagenicos; set => _pontosMutagenicos = value; }
    public StatusBar Energy => _energyBar;
    public StatusBar Health => _healthBar;
    public StatusBar Fertility => _fertilityBar;
    public StatusBar Hunger => _hungerBar;
    public PieceDiet Diet { get => _diet; set => _diet = value; }
    public Owner Owner { get => _owner; set => _owner = value; }
    public List<Alerta> Alerta => _alerta;
    public StatusBar Strength => _strength;
    public EnviromentStatus Humidity { get => _humidity; set => _humidity = value; }
    public EnviromentStatus Temperature { get => _temperature; set => _temperature = value; }
    public bool Resting { get => _isResting; set => _isResting = value; }
    public bool IsDoente { get => _isDoente; set => _isDoente = value; }
    public bool IsUnderDesastre { get => _isUnderDisastre; set => _isUnderDisastre = value; }
    public bool IsDuringAction { get => _isDuringAction; set => _isDuringAction = value; }
    public List<MutationBase> IncompatibleMutations => _incompatibleMutations;
    public List<MutationBase> AppliedMutations => _appliedMutations;
    #endregion

    #region Actions
    public IEnumerator Walk(Piece piece, GameObject targetTile, bool useEnergy)
    {
        if (!useEnergy && (piece.Resting || !LoseEnergyToAct(piece.gameObject, 1)))
            yield break;

        Tile currentTile = piece.PieceRaycastForTile();
        currentTile.Owner = Owner.None;

        Vector3 startPos = piece.transform.position;
        Vector3 targetPos = new Vector3(targetTile.transform.position.x, 5, targetTile.transform.position.z);

        float elapsed = 0;
        while (elapsed < 3f)
        {
            piece.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / 3f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        piece.transform.position = targetPos;
        piece.PieceRaycastForTile().Owner = piece.Owner;
        piece.IsDuringAction = false;
    }

    public void Eat(GameObject piece, GameObject tile)
    {
        Piece pieceScript = piece.GetComponent<Piece>();
        Tile targetTile = pieceScript.PieceRaycastForTile();

        if (pieceScript.Resting)
        {
            bool hasEnergy = LoseEnergyToAct(piece, 2);
            pieceScript.Energy.CurrentBarValue = hasEnergy ? pieceScript.Energy.CurrentBarValue : 0;
            EatRoutine(tile, pieceScript, hasEnergy);
        }
    }

    private void EatRoutine(GameObject tile, Piece pieceScript, bool walk)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        Totem totem = tileScript.Totem.GetComponent<Totem>();
        if (totem.TotemType != TotemType.Ponto_Mutagênico)
        {
            float multiplier = pieceScript.GetDietMultiplier(totem.TotemType);
            pieceScript.Hunger.CurrentBarValue += multiplier * totem.FoodQuantity;
            totem.DeactivateTotem();
        }
        else if(totem.TotemType == TotemType.Ponto_Mutagênico)
        {
            pieceScript.PontosMutagenicos++;
            totem.DeactivateTotem();
        }
        if (walk)
        {
            pieceScript.StartCoroutine(Walk(pieceScript, tile, true));
        }
        else
        {
            pieceScript.IsDuringAction = false;
        }
    }
    public void Rest(GameObject piece)
    {
        piece.GetComponent<Piece>().Resting = true;
    }

    public void Fight(GameObject attacker, GameObject targetTile)
    {
        Piece attackerScript = attacker.GetComponent<Piece>();
        if (attackerScript.Resting && LoseEnergyToAct(attacker, 1))
        {
            Piece opponent = targetTile.GetComponent<Tile>().TileRaycastForPiece();
            if (opponent != null)
            {
                attackerScript.Health.CurrentBarValue -= opponent.Strength.CurrentBarValue;
                opponent.Health.CurrentBarValue -= attackerScript.Strength.CurrentBarValue;
                VerifyFightOutcome(attackerScript, opponent, targetTile);
            }
        }
    }

    private void VerifyFightOutcome(Piece attacker, Piece defender, GameObject targetTile)
    {
        if (attacker.Health.CurrentBarValue == attacker.Health.MinBarValue)
            GameObject.Destroy(attacker.gameObject);
        GameObject.FindAnyObjectByType<RoundManager>().RemoverPieceEmLista(attacker.Owner, attacker.gameObject);
        if (defender.Health.CurrentBarValue == defender.Health.MinBarValue)
        {
            GameObject.Destroy(defender.gameObject);
            GameObject.FindAnyObjectByType<RoundManager>().RemoverPieceEmLista(defender.Owner, defender.gameObject);

            attacker.StartCoroutine(Walk(attacker,targetTile, true));
        }
        attacker.IsDuringAction = false;
    }

    public void Reproduce(GameObject father, GameObject motherTile, List<GameObject> tilesAvailable)
    {
        Piece fatherScript = father.GetComponent<Piece>();
        Piece motherScript = motherTile.GetComponent<Tile>().TileRaycastForPiece();
        GameObject selectedTile = tilesAvailable[UnityEngine.Random.Range(0, tilesAvailable.Count)];
        GameObject offspring = GameObject.Instantiate(father, selectedTile.transform.position + Vector3.up * 5, Quaternion.identity);
        Piece offspringScript = offspring.GetComponent<Piece>();
        Piece.InicializarPiece(offspring, offspringScript.PieceRaycastForTile().gameObject, fatherScript.Diet, fatherScript.Owner, 1);
        fatherScript.Fertility.CurrentBarValue = 0;
        motherScript.Fertility.CurrentBarValue = 0;
        fatherScript.IsDuringAction = false;
        Piece.SetParent(offspring, father.transform.parent.gameObject);
    }

    private static bool LoseEnergyToAct(GameObject piece, float actionFactor)
    {
        Piece pieceScript = piece.GetComponent<Piece>();

        //float energyLoss = actionFactor * (Convert.ToInt32(pieceScript.IsDoente) + 1) *
        //                   (Mathf.Abs(tile.Temperature.CurrentValue - pieceScript.Temperature.IdealValue) +
        //                    0.4f * Mathf.Abs(tile.Humidity.CurrentValue - pieceScript.Humidity.IdealValue));
        float energyLoss = 20;
        if (pieceScript.Energy.CurrentBarValue >= energyLoss)
        {
            pieceScript.Energy.CurrentBarValue -= energyLoss;
            return true;
        }
        return false;
    }
    #endregion

    #region Métodos de Inicalização
    public static void InicializarPiece(GameObject piece,GameObject tile, PieceDiet pieceDiet, Owner owner, int level)
    {
        Piece pieceScript = piece.GetComponent<Piece>();      
        pieceScript._biomeReferences = GameObject.FindFirstObjectByType<BiomeReferences>();
        pieceScript._ownerReference = GameObject.FindFirstObjectByType<OwnerReference>();
        pieceScript._tileTypeReferences = GameObject.FindFirstObjectByType<TileTypeReferences>();
        pieceScript._renderer = pieceScript.GetComponent<Renderer>();
        pieceScript.PontosMutagenicos = 0;
        pieceScript.Diet = pieceDiet;
        pieceScript.Owner =  owner;
        PieceLevelHandler.SetPieceAttributes(pieceScript, level);
        pieceScript._renderer.material = pieceScript._ownerReference.GetColor(pieceScript.Owner);
        pieceScript.Humidity = new EnviromentStatus(pieceScript.PieceRaycastForTile().Humidity);
        pieceScript.Temperature = new EnviromentStatus(pieceScript.PieceRaycastForTile().Temperature);
        pieceScript.SetDietMultipliers();
        pieceScript.PieceRaycastForTile().Owner = pieceScript.Owner;
        pieceScript.IsDuringAction = false;
        GameObject.FindFirstObjectByType<RoundManager>().AdicionarPieceEmLista(pieceScript.Owner, piece);
        
    }

    public static void SetParent(GameObject parent, GameObject son) { parent.transform.SetParent(son.transform); }
    #endregion

    #region Métodos de Raycast
    public Tile PieceRaycastForTile()
    {
        Vector3 rayOrigin = transform.position;
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _rayDistanceTile, TileLayerMask))
        {
            Debug.DrawRay(rayOrigin, Vector3.down * _rayDistanceTile, Color.green);
            return hit.collider.CompareTag("Tile") ? hit.collider.GetComponent<Tile>() : null;
        }
        Debug.DrawRay(rayOrigin, Vector3.down * _rayDistanceTile, Color.red);
        return null;
    }
    #endregion

    #region Métodos de Multiplicadores
    private void SetDietMultipliers()
    {
        (_huntMultiplier, _plantMultiplier) = _diet switch
        {
            PieceDiet.Herbivore => (0.4f, 1.4f),
            PieceDiet.Carnivore => (1.4f, 0.4f),
            _ => (1, 1)
        };
    }

    public float GetDietMultiplier(TotemType totemType)
    {
        return totemType switch
        {
            TotemType.P => _plantMultiplier,
            TotemType.M => _plantMultiplier,
            TotemType.G => _plantMultiplier,
            TotemType.Frutas => _huntMultiplier,
            TotemType.Grãos => _huntMultiplier,
            TotemType.Plantas => _huntMultiplier,
            TotemType.Ponto_Mutagênico => 1,
            _ => 1f
        };
    }
    #endregion

    #region Métodos de Mutação e Saúde
    public void AddMutation(MutationBase mutationToAdd)
    {
        _appliedMutations.Add(mutationToAdd);
        foreach (MutationBase incompatible in mutationToAdd.IncompatibleMutations)
        {
            _incompatibleMutations.Add(incompatible);
        }
        mutationToAdd.Mutate(gameObject.GetComponent<Piece>());
    }

    public void SetVisualPart(PieceParts newPart, Mesh newVisual)
    {

    }

    public void LoseLifeUnderDisastre()
    {
        if (IsUnderDesastre)
        {
            Tile tile = PieceRaycastForTile();
            if (tile == null) return;

            float healthLoss = Mathf.Abs(tile.Temperature.CurrentValue - Temperature.IdealValue) +
                               0.4f * Mathf.Abs(tile.Humidity.CurrentValue - Humidity.IdealValue);
            Energy.CurrentBarValue -= healthLoss;
        }
    }

    private void CheckForIllness()
    {
        if (Alerta.Count >= 3 )
        {
            if (UnityEngine.Random.Range(1, 24) <= Alerta.Count)
            {
                _isDoente = true;
            }           
        }
        else 
        {
            _isDoente = false;
        }
    }
    #endregion

    #region Métodos de turno

    public void EndTurnRoutine()
    {
        this._energyBar.CurrentBarValue += 20;
        this._fertilityBar.CurrentBarValue += 20;
        this._hungerBar.CurrentBarValue -= 20;
        this.CheckForIllness();
        this.Humidity.CurrentValue = this.PieceRaycastForTile().Humidity.CurrentValue;
        this.Temperature.CurrentValue = this.PieceRaycastForTile().Temperature.CurrentValue;
    }
    #endregion
}
