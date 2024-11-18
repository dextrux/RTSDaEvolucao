using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    #region Atributos da peça
    [SerializeField]
    private int _pontosMutagenicos;
    [SerializeField]
    private StatusBar _healthBar = new StatusBar(100, 100);
    [SerializeField]
    private StatusBar _energyBar = new StatusBar(100, 100);
    [SerializeField]
    private StatusBar _fertilityBar = new StatusBar(100, 0);
    [SerializeField]
    private StatusBar _strength = new StatusBar(100, 100);
    [SerializeField]
    private StatusBar _hungerBar = new StatusBar(100, 100);
    [SerializeField]
    private Humidity _humidity;
    [SerializeField]
    private Temperature _temperature;
    [SerializeField]
    private PieceDiet _diet;
    [SerializeField]
    private Owner _owner;
    [SerializeField]
    private PieceLevel _level;
    [SerializeField]
    private List<Alerta> _alerta;
    [SerializeField]
    private bool _isResting = false;
    [SerializeField]
    private bool _isUnderDisastre;
    [SerializeField]
    private bool _isDoente;
    [SerializeField]
    private bool _isDuringAction;
    #endregion

    // Atributos Raycast
    private const float _rayDistanceTile = 10f;
    [SerializeField]
    public LayerMask TileLayerMask;
    int sickCounter;

    #region Propriedades
    public int PontosMutagenicos { get { return _pontosMutagenicos; } set { _pontosMutagenicos = value; } }
    public StatusBar Energy { get { return _energyBar; } }
    public StatusBar Health { get { return _healthBar; } }
    public StatusBar Fertility { get { return _fertilityBar; } }
    public StatusBar Hunger { get { return _hungerBar; } }
    public PieceDiet Diet { get { return _diet; } set { _diet = value; } }
    public Owner Owner { get { return _owner; } set { _owner = value; } }
    public List<Alerta> Alerta { get { return _alerta; } }
    public PieceLevel Level { get { return _level; } set { _level = value; } }
    public StatusBar Strength { get { return _strength; } }
    public Humidity Humidity { get { return _humidity; } }
    public Temperature Temperature { get { return _temperature; } }
    public bool Resting { get { return _isResting; } set { _isResting = value; } }
    public bool IsDoente { get { return _isDoente; } set { _isDoente = value; } }
    public bool IsUnderDesastre { get { return _isUnderDisastre; } set { _isUnderDisastre = value; } }
    public bool IsDuringAction { get { return _isDuringAction; } set { _isDuringAction = value; } }
    #endregion
    //Atributos para mutação
    private List<MutationBase> _appliedMutations;
    private List<MutationBase> _incompatibleMutations;
    private float _huntMultiplier;
    private float _plantMultiplier;
    public List<MutationBase> IncompatibleMutations { get { return _incompatibleMutations; } }
    public List<MutationBase> AppliedMutations { get { return _appliedMutations; } }
    private void Start()
    {
        this.GetComponent<Renderer>().material.color = OwnerColors.GetColor(Owner);
        PieceRaycastForTile().Owner = Owner;
        SetMultiplier();
    }
    public Tile PieceRaycastForTile()
    {
        Vector3 rayOriginTransform = this.transform.position;
        if (Physics.Raycast(rayOriginTransform, Vector3.down, out RaycastHit hit, _rayDistanceTile, TileLayerMask))
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.green);
            if (hit.collider.gameObject.tag == "Tile")
            {
                return hit.collider.gameObject.GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.red);
            return null;
        }
    }
    //Verifica se a peça tem energia e se será necessário mever
    public void Eat(GameObject tile)
    {
        Piece pieceScript = this.GetComponent<Piece>();
        if (!pieceScript.Resting && LoseEnergyToAct(pieceScript.gameObject, pieceScript.PieceRaycastForTile().gameObject, 2))
        {
            EatRoutine(tile, pieceScript, true);
        }
        else if (!pieceScript.Resting && !LoseEnergyToAct(pieceScript.gameObject, pieceScript.PieceRaycastForTile().gameObject, 2))
        {
            pieceScript.Energy.CurrentBarValue = 0;
            EatRoutine(tile, pieceScript, false);
        }
    }
    //Realiza a rotina de alimentação de acordo com o alimento
    private void EatRoutine(GameObject tile, Piece pieceScript, bool walk)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        FoodTotem tileFoodTotemScript = tileScript.Totem.GetComponent<FoodTotem>();
        //Verifica se o alimento é uma caça, se for menor ou igual a 3 é caça 
        if ((int)tileFoodTotemScript.FoodSize <= 3)
        {
            switch (pieceScript.Diet)
            {
                case PieceDiet.Herbivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _huntMultiplier * ((tileFoodTotemScript.FoodQuantity));
                    PontosMutagenicos++;
                    break;
                case PieceDiet.Carnivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _huntMultiplier * (tileFoodTotemScript.FoodQuantity);
                    PontosMutagenicos++;
                    break;
                case PieceDiet.Omnivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _huntMultiplier * (tileFoodTotemScript.FoodQuantity);
                    PontosMutagenicos++;
                    break;
                default:
                    Debug.Log("Exceção encontrada na dieta das peças");
                    break;
            }
        }
        //Se falhar na verificação anterior o alimento é um tipo de planta e entra nesse else
        else if ((int)tileFoodTotemScript.FoodSize > 3)
        {
            //Vegetal
            switch (pieceScript.Diet)
            {
                case PieceDiet.Carnivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _plantMultiplier * (tileFoodTotemScript.FoodQuantity);
                    break;
                case PieceDiet.Herbivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _plantMultiplier * (tileFoodTotemScript.FoodQuantity);
                    break;
                case PieceDiet.Omnivore:
                    pieceScript.Hunger.CurrentBarValue = pieceScript.Hunger.CurrentBarValue + _plantMultiplier * (tileFoodTotemScript.FoodQuantity);
                    break;
                default:
                    Debug.Log("Exceção encontrada na dieta das peças");
                    break;
            }
        }
        tileFoodTotemScript.SetTotemAsInactive();
        if (walk)
        {
            StartCoroutine(Walk(tile, true));
        }
    }
    //Define os multiplicadores da dieta;
    private void SetMultiplier()
    {
        if (_diet == PieceDiet.Herbivore)
        {
            _plantMultiplier = 1.4f;
            _huntMultiplier = 0.4f;
        }
        else if (_diet == PieceDiet.Carnivore)
        {
            _plantMultiplier = 0.4f;
            _huntMultiplier = 1.4f;
        }
        else
        {
            _plantMultiplier = 1;
            _huntMultiplier = 1;
        }
    }
    //Adiciona as mutações a peça
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
    public void Rest(GameObject piece)
    {
        //Adicionar o restaurar da barra de energia
        piece.GetComponent<Piece>().Resting = true;
    }
    public IEnumerator Walk(GameObject newTile, bool energyTaken)
    {
        Piece pieceScript = this.GetComponent<Piece>();
        if (energyTaken)
        {
            Tile oldTile = pieceScript.PieceRaycastForTile();
            oldTile.Owner = Owner.None;

            Vector3 startPosition = this.transform.position;
            Vector3 newPosition = new Vector3(newTile.transform.position.x, 5, newTile.transform.position.z);

            float elapsedTime = 0;

            while (elapsedTime < 3)
            {
                this.transform.position = Vector3.Lerp(startPosition, newPosition, elapsedTime / 3);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            this.transform.position = newPosition; // Garantir que o objeto esteja na posição final.

            Tile newTileScript = pieceScript.PieceRaycastForTile();
            newTileScript.Owner = pieceScript.Owner;
        }
        else
        {
            if (!pieceScript.Resting && LoseEnergyToAct(pieceScript.gameObject, pieceScript.PieceRaycastForTile().gameObject, 1))
            {
                Tile oldTile = pieceScript.PieceRaycastForTile();
                oldTile.Owner = Owner.None;

                Vector3 startPosition = this.transform.position;
                Vector3 newPosition = new Vector3(newTile.transform.position.x, 5, newTile.transform.position.z);

                float elapsedTime = 0;

                while (elapsedTime < 3)
                {
                    this.transform.position = Vector3.Lerp(startPosition, newPosition, elapsedTime / 3);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                this.transform.position = newPosition; // Garantir que o objeto esteja na posição final.

                Tile newTileScript = pieceScript.PieceRaycastForTile();
                newTileScript.Owner = pieceScript.Owner;
            }
        }
        IsDuringAction = false;
    }

    public void Fight(GameObject newtile)
    {
        Piece pieceInvader = this.GetComponent<Piece>();

        if (!pieceInvader.Resting && LoseEnergyToAct(pieceInvader.gameObject, pieceInvader.PieceRaycastForTile().gameObject, 1))
        {
            Piece pieceHolder = newtile.GetComponent<Tile>().TileRaycastForPiece();
            pieceInvader.Health.CurrentBarValue -= pieceHolder.Strength.CurrentBarValue;
            pieceHolder.Health.CurrentBarValue -= pieceInvader.Strength.CurrentBarValue;
            FightDeathVerification(pieceInvader, pieceHolder, newtile);
        }


    }

    private void FightDeathVerification(Piece invader, Piece holder, GameObject newTile)
    {
        if (invader.Health.CurrentBarValue == invader.Health.MinBarValue)
        {
            Destroy(invader.gameObject);
        }
        if (holder.Health.CurrentBarValue == holder.Health.MinBarValue)
        {
            Destroy(holder.gameObject);
            invader.StartCoroutine(Walk(newTile, true));
        }
    }

    public void Reproduce(GameObject father, GameObject motherTile, List<GameObject> tilesAvailable)
    {
        Piece fatherScript = father.GetComponent<Piece>();
        Tile motherTileScript = motherTile.GetComponent<Tile>();
        Piece motherScript = motherTileScript.TileRaycastForPiece();
        System.Random rand = new System.Random();
        GameObject newTile = tilesAvailable[rand.Next(0, tilesAvailable.Count)];
        Vector3 newPiecePosition = new Vector3(newTile.transform.position.x, 5f, newTile.transform.position.z);
        GameObject newPiece = Instantiate(father, newPiecePosition, Quaternion.identity);
        Piece son = newPiece.AddComponent<Piece>();
        son.Temperature.IdealTemperature = (2 * fatherScript.Temperature.IdealTemperature + 2 * motherScript.Temperature.IdealTemperature + motherTileScript.Temperature.CurrentTemperature / 5);
        son.Humidity.IdealHumidity = (2 * fatherScript.Humidity.IdealHumidity + 2 * motherScript.Humidity.IdealHumidity + motherTileScript.Humidity.CurrentHumidity / 5);
        StatusBar sonFertilityBar = son.Fertility;
        sonFertilityBar.CurrentBarValue = 0;
        son.Owner = fatherScript.Owner;
        son.Diet = fatherScript.Diet;
        SetPieceAtributes(son);
    }

    public bool LoseEnergyToAct(GameObject piece, GameObject tile, float factor)
    {
        //(| Tr - TI | +(0.4 * | Ur - UI |))
        //Alterar para levar em conta as mutações
        Piece pieceScript = piece.GetComponent<Piece>();
        Tile tileScript = tile.GetComponent<Tile>();
        GameObject Manager = GameObject.Find("Manager");
        RoundManager roundManager = Manager.GetComponent<RoundManager>();
        if (!pieceScript.Resting)
        {
            if (pieceScript.Energy.CurrentBarValue > 0)
            {
                if (pieceScript.Energy.CurrentBarValue >= factor * (Convert.ToInt32(pieceScript.IsDoente) + 1) * (pieceScript.Energy.CurrentBarValue - (Mathf.Abs(tileScript.Temperature.CurrentTemperature - pieceScript.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tileScript.Humidity.CurrentHumidity - pieceScript.Humidity.IdealHumidity)))))
                {
                    pieceScript.Energy.CurrentBarValue -= factor * (Convert.ToInt32(pieceScript.IsDoente) + 1) * (pieceScript.Energy.CurrentBarValue - (Mathf.Abs(tileScript.Temperature.CurrentTemperature - pieceScript.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tileScript.Humidity.CurrentHumidity - pieceScript.Humidity.IdealHumidity))));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public void LoseLifeUnderDisastre(Piece piece)
    {
        //(| Tr - TI | +(0.4 * | Ur - UI |))
        Tile tile = PieceRaycastForTile();
        if (piece.IsUnderDesastre)
        {
            piece.Energy.CurrentBarValue = piece.Energy.CurrentBarValue - (Mathf.Abs(tile.Temperature.CurrentTemperature - piece.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tile.Humidity.CurrentHumidity - piece.Humidity.IdealHumidity)));
        }
    }

    public void SetPieceAtributes(Piece piece)
    {
        //Level 1 =  (0 - 3 mutações) - HP máximo: 100 - Energia: 100 - Força: 20
        //Level 2 = (4 - 6 mutações) - HP máximo: 300 - Energia: 200 - Força: 20
        //Level 3 = HP máximo: 900 - Energia: 400 - Força: 30
        switch ((int)piece.Level)
        {
            case 1:
                piece.Health.MaxBarValue = 100;
                piece.Energy.MaxBarValue = 100;
                piece.Strength.CurrentBarValue = 20;
                break;
            case 2:
                piece.Health.MaxBarValue = 300;
                piece.Energy.MaxBarValue = 200;
                piece.Strength.CurrentBarValue = 20;
                break;
            case 3:
                piece.Health.MaxBarValue = 900;
                piece.Energy.MaxBarValue = 400;
                piece.Strength.CurrentBarValue = 30;
                break;
            default:
                Debug.Log("Exceção encontrada no nivelamento das peças");
                break;

        }
        piece.Hunger.CurrentBarValue = 100;
    }

    public void AlertVerificationRoutineRoutine()
    {
        Tile tile = PieceRaycastForTile();

        if (tile != null)
        {
            // Verificação de Temperatura
            if (Mathf.Abs(this.Temperature.IdealTemperature - tile.Temperature.CurrentTemperature) > 15)
            {
                if (!Alerta.Contains(global::Alerta.Temperatura))
                    Alerta.Add(global::Alerta.Temperatura);
            }
            else
            {
                Alerta.Remove(global::Alerta.Temperatura);
            }

            // Verificação de Umidade
            if (Mathf.Abs(this.Humidity.IdealHumidity - tile.Humidity.CurrentHumidity) > 15)
            {
                if (!Alerta.Contains(global::Alerta.Umidade))
                    Alerta.Add(global::Alerta.Umidade);
            }
            else
            {
                Alerta.Remove(global::Alerta.Umidade);
            }

            // Efeitos Adjacentes de Temperatura
            if (Alerta.Contains(global::Alerta.Temperatura))
            {
                if (this.Temperature.IdealTemperature > tile.Temperature.CurrentTemperature)
                {
                    if (!Alerta.Contains(global::Alerta.Frio))
                        Alerta.Add(global::Alerta.Frio);
                }
                else
                {
                    Alerta.Remove(global::Alerta.Frio);
                }

                if (this.Temperature.IdealTemperature < tile.Temperature.CurrentTemperature)
                {
                    if (!Alerta.Contains(global::Alerta.Calor))
                        Alerta.Add(global::Alerta.Calor);
                }
                else
                {
                    Alerta.Remove(global::Alerta.Calor);
                }
            }
            else
            {
                // Remover os alertas adjacentes se não há alerta de Temperatura
                Alerta.Remove(global::Alerta.Frio);
                Alerta.Remove(global::Alerta.Calor);
            }

            // Efeitos Adjacentes de Umidade
            if (Alerta.Contains(global::Alerta.Umidade))
            {
                if (this.Humidity.IdealHumidity < tile.Humidity.CurrentHumidity)
                {
                    if (!Alerta.Contains(global::Alerta.Ressecacao))
                        Alerta.Add(global::Alerta.Ressecacao);
                }
                else
                {
                    Alerta.Remove(global::Alerta.Ressecacao);
                }

                if (this.Humidity.IdealHumidity > tile.Humidity.CurrentHumidity)
                {
                    if (!Alerta.Contains(global::Alerta.Desconforto))
                        Alerta.Add(global::Alerta.Desconforto);
                }
                else
                {
                    Alerta.Remove(global::Alerta.Desconforto);
                }
            }
            else
            {
                // Remover os alertas adjacentes se não há alerta de Umidade
                Alerta.Remove(global::Alerta.Ressecacao);
                Alerta.Remove(global::Alerta.Desconforto);
            }

            // Verificação de Fome (Barra de Fome abaixo de 25%)
            if (this.Hunger.CurrentBarValue < 25f * this.Hunger.MaxBarValue)
            {
                if (!Alerta.Contains(global::Alerta.Fome))
                    Alerta.Add(global::Alerta.Fome);
            }
            else
            {
                Alerta.Remove(global::Alerta.Fome);
            }

            // Verificação de Cansaço (Barra de Energia abaixo de 25%)
            if (this.Energy.CurrentBarValue < 25f * this.Energy.MaxBarValue)
            {
                if (!Alerta.Contains(global::Alerta.Cansaco))
                    Alerta.Add(global::Alerta.Cansaco);
            }
            else
            {
                Alerta.Remove(global::Alerta.Cansaco);
            }
        }
    }

    private void IsDoenteStatusChecker()
    {
        System.Random rand = new System.Random();
        int chance = rand.Next(1, 6);
        if (this.Alerta.Count >= 3 && chance == 5)
        {
            this.IsDoente = true;
        }
        else
        {
            this.IsDoente = false;
        }
    }

    private void IsDoenteEffect()
    {
        if (IsDoente)
        {
            Tile tile = PieceRaycastForTile();
            for (int i = 0; i < tile.TilesAdjacentes.Count; i++)
            {
                if (tile.TileRaycastForPiece() != null)
                {
                    Piece piece = tile.TilesAdjacentes[i].GetComponent<Tile>().TileRaycastForPiece();
                    if (piece != null)
                    {
                        System.Random random = new System.Random();
                        int chance = random.Next(1, 4);
                        bool isGettingSick = chance == 3 ? piece.IsDoente = true : false;
                        piece.IsDoente = isGettingSick;
                    }
                }
            }
            sickCounter++;
            if (sickCounter == 4)
            {
                IsDoente = false;
                sickCounter = 0;
            }
        }
    }
    private void HungerBarChecker()
    {
        if (this.Hunger.CurrentBarValue < 25)
        {
            Health.CurrentBarValue *= 0.85f;
        }
        else if (this.Hunger.CurrentBarValue > 85)
        {
            Health.CurrentBarValue *= 1.15f;
        }
    }
    private void CheckDisasterCondition()
    {
        IsUnderDesastre = PieceRaycastForTile().IsUnderDesastre;
        if (IsUnderDesastre)
        {
            LoseLifeUnderDisastre(this.GetComponent<Piece>());
        }
    }
    public void PieceEndRoundRoutine()
    {
        CheckDisasterCondition();
        IsDoenteStatusChecker();
        IsDoenteEffect();
        HungerBarChecker();
        AlertVerificationRoutineRoutine();
        if (Health.CurrentBarValue <= 0)
        {
            Destroy(this);
        }

        //Faltou no Gdd a taxa de recupera??o e diminui??o dos atributos abaixo
        Health.CurrentBarValue += Health.MaxBarValue / 4;
        Energy.CurrentBarValue += Energy.MaxBarValue / 4;
        Fertility.CurrentBarValue += Fertility.MaxBarValue / 5;
        Hunger.CurrentBarValue -= Hunger.MaxBarValue / 5;
    }
}