using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class Piece : MonoBehaviour
{
    #region Analytics & Cladograma
    public GameObject manager;
    private Analytics analytics;

    private Cladograma clado;

    private void Update()
    {
        if (this.Health.CurrentBarValue <= 0)
        {
            GameObject.FindAnyObjectByType<RoundManager>().PieceMorreu(this.Owner, this.gameObject);
            GameObject.Destroy(this.gameObject);           
            analytics.SetPecasMortas();
        }
    }
    private void Start()
    {
        manager = GameObject.Find("Manager");
        analytics = manager.GetComponent<Analytics>();
        clado = manager.GetComponent<Cladograma>();
    }
    #endregion

    #region Constantes e Configura��es
    private const float _rayDistanceTile = 10f;
    private const float RestDuration = 3f; // Dura��o do descanso
    [SerializeField] private LayerMask TileLayerMask;
    BiomeReferences _biomeReferences;
    OwnerReference _ownerReference;
    TileTypeReferences _tileTypeReferences;
    [SerializeField] private Renderer _renderer;
    public GameObject indicador;
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
    private int _isDoenteTime = 0;
    #endregion

    #region Muta��o e Multiplicadores
    private int sickCounter;
    [SerializeField] private Transform _olhoCustomizavel;
    [SerializeField] private Transform _caudaCustomizavel;
    [SerializeField] private Transform _bocaCustomizavel;
    [SerializeField] private Transform[] _patasCustomizavel;
    [SerializeField] private GameObject _olhoCustomizavelAtivos;
    [SerializeField] private GameObject _caudaCustomizavelAtivos;
    [SerializeField] private GameObject _bocaCustomizavelAtivos;
    [SerializeField] private GameObject[] _maosCustomizavelAtivos;
    [SerializeField] private List<MutationBase> _appliedMutations = new List<MutationBase>();
    [SerializeField] private List<MutationBase> _incompatibleMutations = new List<MutationBase>();
    private float _huntMultiplier;
    private float _plantMultiplier;
    private float _healthMultiplier;
    private float _energyMultiplier;
    private float _fertilityMultiplier;
    private float _hungerMultiplier;
    private float _strenghtMultiplier;
    private float _humidityMultiplier;
    private float _temperatureMultiplier;
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
    public bool IsDoente { get => _isDoente; set => _isDoente = value; }
    public bool IsUnderDesastre { get => _isUnderDisastre; set => _isUnderDisastre = value; }
    public bool IsDuringAction { get => _isDuringAction; set => _isDuringAction = value; }
    public List<MutationBase> IncompatibleMutations { get => _incompatibleMutations; set => _incompatibleMutations = value; }
    public List<MutationBase> AppliedMutations { get => _appliedMutations; set => _appliedMutations = value; }
    public float HuntMultiplier { get => _huntMultiplier; set => _huntMultiplier = value; }
    public float PlantMultiplier { get => _plantMultiplier; set => _plantMultiplier = value; }
    public float HealthMultiplier { get => _healthMultiplier; set => _healthMultiplier = value; }
    public float EnergyMultiplier { get => _energyMultiplier; set => _energyMultiplier = value; }
    public float FertilityMultiplier { get => _fertilityMultiplier; set => _fertilityMultiplier = value; }
    public float HungerMultiplier { get => _hungerMultiplier; set => _hungerMultiplier = value; }
    public float StrengthMultiplier { get => _strenghtMultiplier; set => _strenghtMultiplier = value; }
    public float HumidityMultiplier { get => _humidityMultiplier; set => _humidityMultiplier = value; }
    public float TemperatureMultiplier { get => _temperatureMultiplier; set => _temperatureMultiplier = value; }
    #endregion

    #region Actions
    public IEnumerator Walk(Piece piece, GameObject targetTile, bool useEnergy, Tile LastTile)
    {
        LastTile.RetornarTilesParaMaterialOriginal();
        Debug.Log("Entrei em walk");
        if (!useEnergy && (!LoseEnergyToAct(piece.gameObject, 1)))
        {
            yield break;
        }


        Tile currentTile = piece.PieceRaycastForTile();
        currentTile.Owner = Owner.None;

        Vector3 startPos = piece.transform.position;
        Vector3 targetPos = new Vector3(targetTile.transform.position.x, 5, targetTile.transform.position.z);

        float elapsed = 0;
        while (elapsed < 1f)
        {
            piece.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        piece.transform.position = targetPos;
        piece.PieceRaycastForTile().Owner = piece.Owner;
        piece.IsDuringAction = false;

        #region Analytics Mudou Quantas Vezes de Bioma
        analytics.SetNumeroMudouBioma(currentTile);
        #endregion
    }

    public void Eat(GameObject piece, GameObject tile, Tile LastTile)
    {
        LastTile.RetornarTilesParaMaterialOriginal();
        Piece pieceScript = piece.GetComponent<Piece>();
        Tile targetTile = pieceScript.PieceRaycastForTile();
        Debug.Log("Comendo");
        bool hasEnergy = LoseEnergyToAct(piece, 2);
        pieceScript.Energy.CurrentBarValue = hasEnergy ? pieceScript.Energy.CurrentBarValue : 0;
        EatRoutine(tile, pieceScript, hasEnergy, LastTile);
    }

    private void EatRoutine(GameObject tile, Piece pieceScript, bool walk, Tile LastTile)
    {
        LastTile.RetornarTilesParaMaterialOriginal();
        Tile tileScript = tile.GetComponent<Tile>();
        Totem totem = tileScript.Totem.GetComponent<Totem>();
        if (totem.TotemType != TotemType.Ponto_Mutagenico)
        {
            float multiplier = pieceScript.GetDietMultiplier(totem.TotemType);
            pieceScript.Hunger.CurrentBarValue += multiplier * totem.FoodQuantity;
            totem.DeactivateTotem();
        }
        else if (totem.TotemType == TotemType.Ponto_Mutagenico)
        {
            if (walk == true)
            {
                GameObject.FindAnyObjectByType<RoundManager>().AddMutationPoint(pieceScript.Owner);
                GameObject.FindAnyObjectByType<InGameUi>().UpdateMutationPointText();
                totem.DeactivateTotem();
            }


        }

        if (walk)
        {
            pieceScript.StartCoroutine(Walk(pieceScript, tile, true, LastTile));
        }
        else
        {
            pieceScript.IsDuringAction = false;
        }
    }

    public void Fight(GameObject attacker, GameObject targetTile, Tile LastTile)
    {
        LastTile.RetornarTilesParaMaterialOriginal();
        Piece attackerScript = attacker.GetComponent<Piece>();
        if (LoseEnergyToAct(attacker, 1))
        {
            Piece opponent = targetTile.GetComponent<Tile>().TileRaycastForPiece();
            if (opponent != null)
            {
                attackerScript.Health.CurrentBarValue -= opponent.Strength.CurrentBarValue;
                opponent.Health.CurrentBarValue -= attackerScript.Strength.CurrentBarValue;
                VerifyFightOutcome(attackerScript, opponent, targetTile);

            }
        }
        else
        {
            attackerScript.IsDuringAction = false;
        }
    }

    private void VerifyFightOutcome(Piece attacker, Piece defender, GameObject targetTile)
    {
        attacker.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
        if (attacker.Health.CurrentBarValue == attacker.Health.MinBarValue)
        {
            attacker.PieceRaycastForTile().Totem.GetComponent<Totem>().ActivateTotem(TotemType.Corpo);
            GameObject.FindAnyObjectByType<RoundManager>().PieceMorreu(attacker.Owner, attacker.gameObject);

            GameObject.Destroy(attacker.gameObject);

            #region Analytics Combate
            analytics.SetPecasEliminadas();
            #endregion

        }
        if (defender.Health.CurrentBarValue == defender.Health.MinBarValue)
        {
            defender.PieceRaycastForTile().Totem.GetComponent<Totem>().ActivateTotem(TotemType.Corpo);
            GameObject.FindAnyObjectByType<RoundManager>().PieceMorreu(defender.Owner, defender.gameObject);
            GameObject.Destroy(defender.gameObject);
            analytics.SetPecasMortas();
        }
        FindAnyObjectByType<InGameUi>().UpdateLifeBarOwnerBase();
        attacker.IsDuringAction = false;
    }

    public void Reproduce(GameObject father, GameObject motherTile, List<GameObject> tilesAvailable)
    {
        Piece fatherScript = father.GetComponent<Piece>();
        Piece motherScript = motherTile.GetComponent<Tile>().TileRaycastForPiece();
        GameObject selectedTile = tilesAvailable[UnityEngine.Random.Range(0, tilesAvailable.Count)];
        GameObject offspring = GameObject.Instantiate(father, selectedTile.transform.position + Vector3.up * 1, Quaternion.identity);
        Piece offspringScript = offspring.GetComponent<Piece>();
        Piece.InicializarPiece(offspring, offspringScript.PieceRaycastForTile().gameObject, fatherScript.Diet, fatherScript.Owner, 1);
        fatherScript.Fertility.CurrentBarValue = 0;
        motherScript.Fertility.CurrentBarValue = 0;
        fatherScript.IsDuringAction = false;
        offspringScript.indicador.SetActive(true);
        Piece.SetParent(offspring, father.transform.parent.gameObject);

        #region Analytics Numero de Reproducoes
        analytics.SetNumeroReproducoes();
        #endregion
    }

    private static bool LoseEnergyToAct(GameObject piece, float actionFactor)
    {
        Piece pieceScript = piece.GetComponent<Piece>();
        Tile tile = pieceScript.PieceRaycastForTile();
        float energyLoss;
        float temperaturaFactor = Mathf.Abs(tile.Temperature.CurrentValue - pieceScript.Temperature.IdealValue);
        float humidadeFactor = 0.4f * Mathf.Abs(tile.Humidity.CurrentValue - pieceScript.Humidity.IdealValue);
        float isDoenteFactor = Convert.ToInt32(pieceScript.IsDoente) + 1f;
        if (temperaturaFactor < 10 || temperaturaFactor > 10)
        {
            temperaturaFactor = 5;
            energyLoss = actionFactor * isDoenteFactor * (temperaturaFactor + humidadeFactor);

        }
        else if (humidadeFactor < 10 || humidadeFactor > 10)
        {
            humidadeFactor = 5;
            energyLoss = actionFactor * isDoenteFactor * (temperaturaFactor + humidadeFactor);
        }
        else
        {
            energyLoss = actionFactor * isDoenteFactor * (temperaturaFactor + humidadeFactor) / 3;

        }
        if (pieceScript.Energy.CurrentBarValue >= energyLoss)
        {
            pieceScript.Energy.CurrentBarValue -= energyLoss;
            return true;
        }
        pieceScript._isDuringAction = false;
        FindAnyObjectByType<PlayerRaycast>().FimDoBlink(pieceScript.PieceRaycastForTile());
        return false;
    }
    #endregion

    #region M�todos de Inicaliza��o
    public static void InicializarPiece(GameObject piece, GameObject tile, PieceDiet pieceDiet, Owner owner, int level)
    {
        Piece pieceScript = piece.GetComponent<Piece>();
        pieceScript._biomeReferences = GameObject.FindFirstObjectByType<BiomeReferences>();
        pieceScript._ownerReference = GameObject.FindFirstObjectByType<OwnerReference>();
        pieceScript._tileTypeReferences = GameObject.FindFirstObjectByType<TileTypeReferences>();
        pieceScript.PontosMutagenicos = 0;
        pieceScript.Diet = pieceDiet;
        pieceScript.Owner = owner;
        PieceLevelHandler.SetPieceAttributes(pieceScript, level);
        pieceScript._renderer.material = pieceScript._ownerReference.GetColor(pieceScript.Owner);
        pieceScript.Humidity = new EnviromentStatus(pieceScript.PieceRaycastForTile().Humidity);
        pieceScript.Temperature = new EnviromentStatus(pieceScript.PieceRaycastForTile().Temperature);
        pieceScript.SetDietMultipliers();
        pieceScript.PieceRaycastForTile().Owner = pieceScript.Owner;
        pieceScript.IsDuringAction = false;
        GameObject.FindFirstObjectByType<RoundManager>().AdicionarPieceEmLista(pieceScript.Owner, piece);
        pieceScript.HuntMultiplier = 100;
        pieceScript.PlantMultiplier = 100;
        pieceScript.HealthMultiplier = 100;
        pieceScript.EnergyMultiplier = 100;
        pieceScript.FertilityMultiplier = 100;
        pieceScript.HungerMultiplier = 100;
        pieceScript.StrengthMultiplier = 100;
        pieceScript.HumidityMultiplier = 100;
        pieceScript.TemperatureMultiplier = 100;
    }

    public static void SetParent(GameObject parent, GameObject son) { parent.transform.SetParent(son.transform); }
    #endregion

    #region M�todos de Raycast
    public Tile PieceRaycastForTile()
    {
        Vector3 rayOrigin = transform.position;
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _rayDistanceTile, TileLayerMask))
        {
            return hit.collider.CompareTag("Tile") ? hit.collider.GetComponent<Tile>() : null;
        }
        return null;
    }
    #endregion

    #region M�todos de Multiplicadores
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
            TotemType.Graos => _huntMultiplier,
            TotemType.Plantas => _huntMultiplier,
            TotemType.Ponto_Mutagenico => 1,
            TotemType.Corpo => _huntMultiplier,
            _ => 1f
        };
    }
    #endregion

    #region M�todos de Muta��o e Sa�de
    public bool AddMutation(MutationBase mutationToAdd)
    {
        if (mutationToAdd.Cost > FindAnyObjectByType<RoundManager>().GetMutationPointOwnerBased(Owner) || _incompatibleMutations.Contains(mutationToAdd) || !(mutationToAdd.IsMutationUnlockable(this)))
        {
            Debug.Log("Custo: " + (mutationToAdd.Cost > FindAnyObjectByType<RoundManager>().GetMutationPointOwnerBased(Owner)));
            Debug.Log("Incompatibilidade: " + _incompatibleMutations.Contains(mutationToAdd));
            Debug.Log("Falta Requerimento: " + !(mutationToAdd.IsMutationUnlockable(this)));
            Debug.Log("Incompatível");
            return false;
        }
        Debug.Log("Compatível");
        foreach (MutationBase incompatible in mutationToAdd.IncompatibleMutations)
        {
            _incompatibleMutations.Add(incompatible);
        }
        mutationToAdd.Mutate(this);


        analytics.SetTempoPrimeiraCompra();
        analytics.SetTempoPrimeiraCompra();
        analytics.SetNumeroTotalCompras();
        analytics.SetMaximoMutacoesCriatura(_appliedMutations);
        analytics.SetMinimoMutacoesCriatura(_appliedMutations);
        analytics.SetNumeroMutacoesHerbCarn(mutationToAdd.name);
        analytics.SetTempoMaxCompras();

        //clado.AddListaClado(_owner, mutationToAdd.name);

        return true;
    }
    public bool AddMutation(MutationBase mutationToAdd, bool firstTurn)
    {
        foreach (MutationBase incompatible in mutationToAdd.IncompatibleMutations)
        {
            _incompatibleMutations.Add(incompatible);
        }
        mutationToAdd.Mutate(this);
        //clado.AddListaClado(_owner, mutationToAdd.name);

        return true;
    }
    public void ChangeDiet(PieceDiet newDiet)
    {
        if (newDiet == PieceDiet.Carnivore)
        {
            _diet = newDiet;
            _appliedMutations.Remove(Resources.Load<MutationDiet>("Mutation/01Herbivore"));
            _incompatibleMutations.Add(Resources.Load<MutationDiet>("Mutation/01Herbivore"));
            _appliedMutations.Add(Resources.Load<MutationDiet>("Mutation/02Carnivore"));
        }
        else if (newDiet == PieceDiet.Omnivore)
        {
            _diet = newDiet;
            _appliedMutations.Remove(Resources.Load<MutationDiet>("Mutation/02Carnivore"));
            _incompatibleMutations.Add(Resources.Load<MutationDiet>("Mutation/02Carnivore"));
            _appliedMutations.Add(Resources.Load<MutationDiet>("Mutation/03Omnivore"));
        }
        else
        {
            _diet = PieceDiet.Herbivore;
            _appliedMutations.Add(Resources.Load<MutationDiet>("Mutation/01Herbivore"));
        }
    }

    public void SetVisualPart(PieceParts newPart, Mesh newVisual)
    {
        switch (newPart)
        {
            case PieceParts.Olhos:
                if (_olhoCustomizavelAtivos != null) Destroy(_olhoCustomizavelAtivos);
                Instantiate(newVisual, _olhoCustomizavel.transform.position, _olhoCustomizavel.transform.rotation);
                break;
            case PieceParts.Boca:
                if (_bocaCustomizavelAtivos != null) Destroy(_bocaCustomizavelAtivos);
                Instantiate(newVisual, _bocaCustomizavel.transform.position, _bocaCustomizavel.transform.rotation);
                break;
            case PieceParts.Rabo:
                if (_caudaCustomizavelAtivos != null) Destroy(_caudaCustomizavelAtivos);
                Instantiate(newVisual, _caudaCustomizavel.transform.position, _caudaCustomizavel.transform.rotation);
                break;
            case PieceParts.Mao:
                foreach (GameObject g in _maosCustomizavelAtivos)
                {
                    if (g != null) Destroy(g);
                }
                foreach (Transform t in _patasCustomizavel)
                {
                    Instantiate(newVisual, t.transform.position, t.transform.rotation);
                }
                break;
        }
    }

    public void LoseLifeUnderDisastre()
    {
        if (IsUnderDesastre)
        {
            Tile tile = PieceRaycastForTile();
            if (tile == null) return;
            float healthLoss;
            float temperaturaFactor = Mathf.Abs(tile.Temperature.CurrentValue - this.Temperature.IdealValue);
            float humidadeFactor = 0.4f * Mathf.Abs(tile.Humidity.CurrentValue - this.Humidity.IdealValue);
            float isDoenteFactor = Convert.ToInt32(this.IsDoente) + 1f;
            if (temperaturaFactor < 10 || temperaturaFactor > 10)
            {
                temperaturaFactor = 5;
                healthLoss = isDoenteFactor * (temperaturaFactor + humidadeFactor);

            }
            else if (humidadeFactor < 10 || humidadeFactor > 10)
            {
                humidadeFactor = 5;
                healthLoss = isDoenteFactor * (temperaturaFactor + humidadeFactor);
            }
            else
            {
                healthLoss = isDoenteFactor * (temperaturaFactor + humidadeFactor) / 3;

            }

            if (this.Health.CurrentBarValue >= healthLoss)
            {
                this.Health.CurrentBarValue -= healthLoss;
            }
            else
            {
                FindAnyObjectByType<RoundManager>().PieceMorreu(this.Owner, this.gameObject);
            }
        }
    }

    private void CheckForIllness()
    {
        if (Alerta.Count >= 3)
        {
            if (UnityEngine.Random.Range(1, 24) <= Alerta.Count)
            {
                _isDoente = true;
            }
        }
        else if (_isDoenteTime > 3 && _isDoente == true)
        {
            _isDoente = false;
            _isDoenteTime = 0;
        }
        if (_isDoente == true)
        {
            _isDoenteTime++;
            System.Random rand = new System.Random();
            if (rand.Next(0, 3) == 0)
            {
                List<GameObject> tilesAdjacentes = this.PieceRaycastForTile().TilesAdjacentes;
                foreach (var tile in tilesAdjacentes)
                {
                    tile.GetComponent<Tile>().TileRaycastForPiece().IsDoente = true;
                }
            }
        }
    }
    #endregion

    #region M�todos de turno

    public void EndTurnRoutine()
    {
        float energyGain;
        float temperaturaFactor = Mathf.Abs(this.PieceRaycastForTile().Temperature.CurrentValue - this.Temperature.IdealValue);
        float humidadeFactor = 0.4f * Mathf.Abs(this.PieceRaycastForTile().Humidity.CurrentValue - this.Humidity.IdealValue);
        float isDoenteFactor = Convert.ToInt32(this.IsDoente) + 1f;
        if (temperaturaFactor < 10 || temperaturaFactor > 10)
        {
            temperaturaFactor = 10;
            energyGain = (temperaturaFactor + humidadeFactor) / isDoenteFactor;

        }
        else if (humidadeFactor < 10 || humidadeFactor > 10)
        {
            humidadeFactor = 10;
            energyGain = (temperaturaFactor + humidadeFactor) / isDoenteFactor;
        }
        else
        {
            energyGain = (temperaturaFactor + humidadeFactor) / isDoenteFactor * 2;

        }
        this.Health.CurrentBarValue += (energyGain) * _healthMultiplier / 100;
        Debug.Log("Energy Gain:" + energyGain);
        this._energyBar.CurrentBarValue += (energyGain) * _energyMultiplier / 100;
        Debug.Log("Energy:" + this._energyBar.CurrentBarValue);
        this._fertilityBar.CurrentBarValue += (energyGain) * _fertilityMultiplier / 100;
        Debug.Log("Fertility:" + this._fertilityBar.CurrentBarValue);
        this._hungerBar.CurrentBarValue -= (energyGain) * _hungerMultiplier / 100;
        Debug.Log("Hunger:" + this._hungerBar.CurrentBarValue);
        AlertHandler.AlertVerificationRoutine(this);
        this.CheckForIllness();
        this.Humidity.CurrentValue = this.PieceRaycastForTile().Humidity.CurrentValue;
        this.Temperature.CurrentValue = this.PieceRaycastForTile().Temperature.CurrentValue;
        LoseLifeUnderDisastre();
    }

    public void AtivarIndicador()
    {
        indicador.SetActive(true);
        // indicador.transform.LookAt(FindAnyObjectByType<Camera>().transform); 
        indicador.GetComponent<SpriteRenderer>().color = FindAnyObjectByType<OwnerReference>().GetColor(this.Owner).color;
    }
    public void DesativarIndicador()
    {
        indicador.SetActive(false);
    }
    #endregion
}
