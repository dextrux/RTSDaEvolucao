using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

public class RoundManager : MonoBehaviour
{
    #region Analytics
    public GameObject manager;
    private Analytics analytics;
    #endregion

    //Atributos
    #region Valores de Refer�ncia
    [SerializeField]
    public int _maxTurnos;
    [SerializeField]
    public int _periodicidadeDesastreMenor;
    [SerializeField]
    public int _periodicidadeDesastreMaior;
    [SerializeField]
    public int _quantidadeDeComidaSpawn;
    [SerializeField]
    public int _quantidadeDePontosMutagenicosSpawn;
    [SerializeField]
    public int _currentTurno;
    [SerializeField]
    Vector3 _pieceHeight = new Vector3(0f, 1f, 0f);
    [SerializeField]
    List<Owner> owners = new List<Owner>();
    public Camera MainCam;
    private Owner _roundOwner;
    private int currentIndexOwner;
    [SerializeField] private InGameUi _inGameUi;
    #endregion

    #region ReadOnly nomes Biomas
    public static readonly string nomeBiomaOne = "Standard Pampa"; //Bioma Central
    public static readonly string nomeBiomaTwo = "Standard Atlantica";
    public static readonly string nomeBiomaThree = "Standard Araucarias";
    public static readonly string nomeBiomaFour = "Standard Caatinga";
    public static readonly string nomeBiomaFive = "Standard Pantanal";
    #endregion

    #region Listas de Biomas
    [SerializeField]
    private List<GameObject> _biomaOne; //Bioma Central
    [SerializeField]
    private List<GameObject> _biomaTwo;
    [SerializeField]
    private List<GameObject> _biomaThree;
    [SerializeField]
    private List<GameObject> _biomaFour;
    [SerializeField]
    private List<GameObject> _biomaFive;
    #endregion

    #region Lista de Pe�as
    [SerializeField]
    private List<GameObject> _p1Pieces;
    [SerializeField]
    private List<GameObject> _p2Pieces;
    [SerializeField]
    private List<GameObject> _p3Pieces;
    [SerializeField]
    private List<GameObject> _p4Pieces;
    [SerializeField]
    private List<GameObject> _p5Pieces;
    #endregion

    #region Mutacoes
    [SerializeField]
    private int _p1MutationPoint;
    [SerializeField]
    private int _p2MutationPoint;
    [SerializeField]
    private int _p3MutationPoint;
    [SerializeField]
    private int _p4MutationPoint;
    [SerializeField]
    private int _p5MutationPoint;
    [SerializeField] private GameObject _gameOverUi;
    [SerializeField] private GameObject _gameOverUiDeath;
    #endregion

    #region Referências Desastre
    [SerializeField]
    List<GameObject> _tilesUnderBigDissaster;
    [SerializeField]
    List<GameObject> _tilesUnderSmallDissaster;
    [SerializeField]
    int _indexDesastreMenor;
    [SerializeField]
    int _indexDesastreMaior;
    private bool _isUnderBigDisaster;
    private bool _isUnderSmallDisaster;
    #endregion

    #region Prefabs
    public GameObject pcPrefab;
    public GameObject npcPrefabPiece;
    #endregion

    #region Parent Transforms de cada Owner
    public GameObject parentP1;
    public GameObject parentP2;
    public GameObject parentP3;
    public GameObject parentP4;
    public GameObject parentP5;
    #endregion

    #region Totens
    [SerializeField]
    List<GameObject> _totensAtivos;
    #endregion

    #region Propriedades
    public int _MaxTurnos { get => _maxTurnos; set => _maxTurnos = value; }
    public int _PeriodicidadeDesastreMenor { get => _periodicidadeDesastreMenor; set => _periodicidadeDesastreMenor = value; }
    public int _PeriodicidadeDesastreMaior { get => _periodicidadeDesastreMaior; set => _periodicidadeDesastreMaior = value; }
    public int _QuantidadeDeComidaSpawn { get => _quantidadeDeComidaSpawn; set => _quantidadeDeComidaSpawn = value; }
    public int _QuantidadeDePontosMutagenicosSpawn { get => _quantidadeDePontosMutagenicosSpawn; set => _quantidadeDePontosMutagenicosSpawn = value; }
    public int _CurrentTurno { get => _currentTurno; set => _currentTurno = value; }
    public List<GameObject> _BiomaOne { get => _biomaOne; set => _biomaOne = value; }
    public List<GameObject> _BiomaTwo { get => _biomaTwo; set => _biomaTwo = value; }
    public List<GameObject> _BiomaThree { get => _biomaThree; set => _biomaThree = value; }
    public List<GameObject> _BiomaFour { get => _biomaFour; set => _biomaFour = value; }
    public List<GameObject> _BiomaFive { get => _biomaFive; set => _biomaFive = value; }
    public List<GameObject> _P1Pieces { get => _p1Pieces; set => _p1Pieces = value; }
    public List<GameObject> _P2Pieces { get => _p2Pieces; set => _p2Pieces = value; }
    public List<GameObject> _P3Pieces { get => _p3Pieces; set => _p3Pieces = value; }
    public List<GameObject> _P4Pieces { get => _p4Pieces; set => _p4Pieces = value; }
    public List<GameObject> _P5Pieces { get => _p5Pieces; set => _p5Pieces = value; }
    public List<GameObject> _TilesUnderBigDissaster { get => _tilesUnderBigDissaster; set => _tilesUnderBigDissaster = value; }
    public List<GameObject> _TilesUnderSmallDissaster { get => _tilesUnderSmallDissaster; set => _tilesUnderSmallDissaster = value; }
    public int _IndexDesastreMenor { get => _indexDesastreMenor; set => _indexDesastreMenor = value; }
    public int _IndexDesastreMaior { get => _indexDesastreMaior; set => _indexDesastreMaior = value; }
    public List<GameObject> _TotensAtivos { get => _totensAtivos; set => _totensAtivos = value; }
    public Owner RoundOwner { get => _roundOwner; }
    public int PontosP1Mutacoes { get => _p1MutationPoint; }
    public int PontosP2Mutacoes { get => _p2MutationPoint; }
    public int PontosP3Mutacoes { get => _p3MutationPoint; }
    public int PontosP4Mutacoes { get => _p4MutationPoint; }
    public int PontosP5Mutacoes { get => _p5MutationPoint; }

    #endregion

    #region Rotinas Unity
    private void Awake()
    {
        this.owners = FindAnyObjectByType<Observer>().Owners;
        BuscarTiles();
    }

    private void Start()
    {
        #region Set Analytics
        manager = GameObject.Find("Manager");
        analytics = manager.GetComponent<Analytics>();
        #endregion

        _quantidadeDePontosMutagenicosSpawn = owners.Count * 3;
        _quantidadeDeComidaSpawn = owners.Count * 5;
        PrimeiroTurno();
        HandlerIndicadores();
    }

    private void Update()
    {
        if (GameOver(Owner.P1) && GameOver(Owner.P2) && GameOver(Owner.P3) && GameOver(Owner.P4) && GameOver(Owner.P5))
        {
            AllPlayersDied();
        }
    }
    #endregion

    #region Pieces

    public void HandlerIndicadores()
    {
        // Lista de todos os poss�veis Owners
        Owner[] allOwners = { Owner.P1, Owner.P2, Owner.P3, Owner.P4, Owner.P5 };

        // Itera sobre todos os Owners
        foreach (var owner in allOwners)
        {
            if (owner == MainCam.GetComponent<PlayerRaycast>().playerCamOwner)
            {
                // Ativa indicadores para o Owner da vez
                AtivarIndicadores(owner);
            }
            else
            {
                // Desativa indicadores para os outros Owners
                DesativarIndicador(owner);
            }
        }
    }

    private void ProcessarIndicadores(List<GameObject> pieces, Action<Piece> action)
    {
        if (pieces.Count > 0)
        {
            foreach (var piece in pieces)
            {
                var pieceComponent = piece.GetComponent<Piece>();
                action(pieceComponent);
            }
        }
    }

    private void AtivarIndicadores(Owner owner)
    {
        switch (owner) // Usar o par�metro `owner`
        {
            case Owner.P1:
                ProcessarIndicadores(_P1Pieces, piece => piece.AtivarIndicador());
                break;
            case Owner.P2:
                ProcessarIndicadores(_P2Pieces, piece => piece.AtivarIndicador());
                break;
            case Owner.P3:
                ProcessarIndicadores(_P3Pieces, piece => piece.AtivarIndicador());
                break;
            case Owner.P4:
                ProcessarIndicadores(_P4Pieces, piece => piece.AtivarIndicador());
                break;
            case Owner.P5:
                ProcessarIndicadores(_P5Pieces, piece => piece.AtivarIndicador());
                break;
        }
    }

    private void DesativarIndicador(Owner owner)
    {
        switch (owner) // Usar o par�metro `owner`
        {
            case Owner.P1:
                ProcessarIndicadores(_P1Pieces, piece => piece.DesativarIndicador());
                break;
            case Owner.P2:
                ProcessarIndicadores(_P2Pieces, piece => piece.DesativarIndicador());
                break;
            case Owner.P3:
                ProcessarIndicadores(_P3Pieces, piece => piece.DesativarIndicador());
                break;
            case Owner.P4:
                ProcessarIndicadores(_P4Pieces, piece => piece.DesativarIndicador());
                break;
            case Owner.P5:
                ProcessarIndicadores(_P5Pieces, piece => piece.DesativarIndicador());
                break;
        }
    }


    public void InstanciarPecasParaJogo(int quantidade, List<Owner> owners)
    {
        for (int i = 0; i < quantidade; i++)
        {
            foreach (var owner in owners)
            {
                // Obtenha as posi��es necess�rias
                Vector3[] positions = new Vector3[4];
                positions[0] = SortearTilesParaPieces(1, 2)[0].transform.position + _pieceHeight;
                positions[1] = SortearTilesParaPieces(1, 3)[0].transform.position + _pieceHeight;
                positions[2] = SortearTilesParaPieces(1, 4)[0].transform.position + _pieceHeight;
                positions[3] = SortearTilesParaPieces(1, 5)[0].transform.position + _pieceHeight;

                // Instancie e inicialize as pe�as
                foreach (var position in positions)
                {
                    InstanciarEInicializarPiece(position, owner);
                }
            }
        }
    }

    private void InstanciarEInicializarPiece(Vector3 position, Owner owner)
    {
        GameObject newObject = Instantiate(pcPrefab, position, Quaternion.identity);
        switch (owner)
        {
            case Owner.P1:
                Piece.SetParent(newObject, parentP1);
                break;
            case Owner.P2:
                Piece.SetParent(newObject, parentP2);
                break;
            case Owner.P3:
                Piece.SetParent(newObject, parentP3);
                break;
            case Owner.P4:
                Piece.SetParent(newObject, parentP4);
                break;
            case Owner.P5:
                Piece.SetParent(newObject, parentP5);
                break;
            default:
                break;
        }
        Piece.InicializarPiece(
            newObject,
            newObject.GetComponent<Piece>().PieceRaycastForTile().gameObject,
            PieceDiet.Herbivore,
            owner,
            1
        );
        newObject.GetComponent<Piece>().AddMutation(Resources.Load<MutationBase>("Mutation/01Herbivore"), true);
    }

    public void AddMutationPoint(Owner pieceOwner)
    {
        switch (pieceOwner)
        {
            case Owner.P1:
                _p1MutationPoint++;
                break;
            case Owner.P2:
                _p2MutationPoint++;
                break;
            case Owner.P3:
                _p3MutationPoint++;
                break;
            case Owner.P4:
                _p4MutationPoint++;
                break;
            case Owner.P5:
                _p5MutationPoint++;
                break;
        }
    }
    public void AddMutationPoint(Owner pieceOwner, int value)
    {
        switch (pieceOwner)
        {
            case Owner.P1:
                _p1MutationPoint += value;
                break;
            case Owner.P2:
                _p2MutationPoint += value;
                break;
            case Owner.P3:
                _p3MutationPoint += value;
                break;
            case Owner.P4:
                _p4MutationPoint += value;
                break;
            case Owner.P5:
                _p5MutationPoint += value;
                break;
        }
    }
    public int GetMutationPointOwnerBased(Owner pieceOwner)
    {
        switch (pieceOwner)
        {
            case Owner.P1:
                return _p1MutationPoint;
            case Owner.P2:
                return _p2MutationPoint;
            case Owner.P3:
                return _p3MutationPoint;
            case Owner.P4:
                return _p4MutationPoint;
            default:
                return _p5MutationPoint;
        }
    }

    #endregion

    #region Tiles
    private void BuscarTiles()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(nomeBiomaOne))
            {
                _biomaOne.Add(obj);
            }
            else if (obj.name.Contains(nomeBiomaTwo))
            {
                _biomaTwo.Add(obj);
            }
            else if (obj.name.Contains(nomeBiomaThree))
            {
                _biomaThree.Add(obj);
            }
            else if (obj.name.Contains(nomeBiomaFour))
            {
                _biomaFour.Add(obj);
            }
            else if (obj.name.Contains(nomeBiomaFive))
            {
                _biomaFive.Add(obj);
            }
            else
            {
                //Debug.Log($"{obj.name} n�o pertence aos tiles especificados.");
            }
        }

        // Para verificar a quantidade de objetos encontrados em cada lista (opcional)
        /*Debug.Log($"Tiles One: {_biomaOne.Count}");
        Debug.Log($"Tiles Two: {_biomaTwo.Count}");
        Debug.Log($"Tiles Three: {_biomaThree.Count}");
        Debug.Log($"Tiles Four: {_biomaFour.Count}");
        Debug.Log($"Tiles Five: {_biomaFive.Count}");*/
    }
    List<GameObject> SortearTilesRandom(int quantidade)
    {
        List<GameObject> tiles = new List<GameObject>();
        List<GameObject> chosenTileList = new List<GameObject>();
        System.Random random = new System.Random();
        while (tiles.Count < quantidade)
        {
            // Seleciona uma lista de tiles aleat�ria
            switch (random.Next(1, 5))
            {
                case 1:
                    chosenTileList = _BiomaTwo;
                    break;
                case 2:
                    chosenTileList = _BiomaThree;
                    break;
                case 3:
                    chosenTileList = _BiomaFour;
                    break;
                case 4:
                    chosenTileList = _BiomaFive;
                    break;
                default:
                    Debug.Log("Exce��o ao selecionar bioma");
                    break;
            }

            // Seleciona um tile aleat�rio dentro da lista
            GameObject chosenTile = chosenTileList[random.Next(0, chosenTileList.Count)];

            // Adiciona o tile apenas se ainda n�o estiver na lista
            if (!tiles.Contains(chosenTile) && chosenTile.GetComponent<Tile>().TileType == TileType.Posicionamento && chosenTile.GetComponent<Tile>().Owner == Owner.None)
            {
                tiles.Add(chosenTile);
            }
        }
        return tiles;
    }

    List<GameObject> SortearTilesParaPieces(int quantidade, int index)
    {
        List<GameObject> tiles = new List<GameObject>();
        List<GameObject> chosenTileList = new List<GameObject>();
        System.Random random = new System.Random();
        while (tiles.Count < quantidade)
        {
            // Seleciona uma lista de tiles
            switch (index)
            {
                case 1:
                    chosenTileList = _BiomaOne;
                    break;
                case 2:
                    chosenTileList = _BiomaTwo;
                    break;
                case 3:
                    chosenTileList = _BiomaThree;
                    break;
                case 4:
                    chosenTileList = _BiomaFour;
                    break;
                case 5:
                    chosenTileList = _BiomaFive;
                    break;
                default:
                    Debug.Log("Exce��o ao selecionar bioma");
                    break;
            }

            // Seleciona um tile aleat�rio dentro da lista
            GameObject chosenTile = chosenTileList[random.Next(0, chosenTileList.Count)];

            // Adiciona o tile apenas se ainda n�o estiver na lista
            if (!tiles.Contains(chosenTile) && chosenTile.GetComponent<Tile>().Owner == Owner.None && chosenTile.GetComponent<Tile>().TileType == TileType.Posicionamento)
            {
                tiles.Add(chosenTile);
            }
        }
        return tiles;
    }
    #endregion

    #region Biomas
    List<List<GameObject>> SortearBiomas(int quantidade)
    {
        List<List<GameObject>> biomas = new List<List<GameObject>>();
        List<GameObject> chosenTileList = new List<GameObject>();
        System.Random random = new System.Random();
        while (biomas.Count < quantidade)
        {
            // Seleciona uma lista de tiles aleat�ria
            switch (random.Next(1, 5))
            {
                case 1:
                    chosenTileList = _BiomaTwo;
                    break;
                case 2:
                    chosenTileList = _BiomaThree;
                    break;
                case 3:
                    chosenTileList = _BiomaFour;
                    break;
                case 4:
                    chosenTileList = _BiomaFive;
                    break;
                default:
                    Debug.Log("Exce��o ao selecionar bioma");
                    break;
            }            // Adiciona o tile apenas se ainda n�o estiver na lista
            if (!biomas.Contains(chosenTileList))
            {
                biomas.Add(chosenTileList);
            }
        }
        return biomas;
    }
    #endregion

    #region Desastres
    void AplicarEfeitosDesastreMenor(List<List<GameObject>> bioma)
    {
        List<GameObject> flatList = GetNestedListFlattened(bioma);
        _isUnderSmallDisaster = true;
        _TilesUnderSmallDissaster = flatList;
        _indexDesastreMenor = BiomeDisasterManager.SortearEventoMenor(flatList);
    }
    void AplicarEfeitosDesastreMaior(List<List<GameObject>> bioma)
    {
        _isUnderBigDisaster = true;
        List<GameObject> flatList = GetNestedListFlattened(bioma);
        _TilesUnderBigDissaster = flatList;
        _indexDesastreMaior = BiomeDisasterManager.SortearEventoMaior(flatList);
    }
    void AcabarDesastreMenor(List<List<GameObject>> bioma, int indexDesastreMenor)
    {
        List<GameObject> flatList = GetNestedListFlattened(bioma);
        _isUnderSmallDisaster = false;
        BiomeDisasterManager.AcabarDesastre(flatList, false, _IndexDesastreMenor);

    }
    void AcabarDesastreMaior(List<List<GameObject>> bioma, int indexDesastreMaior)
    {
        _isUnderBigDisaster = false;
        List<GameObject> flatList = GetNestedListFlattened(bioma);
        BiomeDisasterManager.AcabarDesastre(flatList, true, _indexDesastreMaior);
    }
    #endregion

    #region Totens
    int DefinirProporcaoComida()
    {
        System.Random random = new System.Random();
        return random.Next(0, 6);
    }
    void AtivarTotensComida(List<GameObject> list)
    {
        foreach (var tiles in list)
        {
            TotemType totemType = (TotemType)DefinirProporcaoComida();
            tiles.GetComponent<Tile>().Totem.GetComponent<Totem>().ActivateTotem(totemType);
            _TotensAtivos.Add(tiles.GetComponent<Tile>().Totem);
        }
    }
    void AtivarTotensPontosMutagenicos(List<GameObject> list)
    {
        foreach (var tiles in list)
        {
            TotemType totemType = TotemType.Ponto_Mutagenico;
            tiles.GetComponent<Tile>().Totem.GetComponent<Totem>().ActivateTotem(totemType);
            _TotensAtivos.Add(tiles.GetComponent<Tile>().Totem);
        }
    }
    void DesativarTodosTotensAtivos(List<GameObject> list)
    {
        foreach (var tiles in list)
        {
            tiles.GetComponent<Totem>().DeactivateTotem();
        }
    }
    #endregion

    #region Turnos
    private void PrimeiroTurno()
    {
        //Blah
        InstanciarPecasParaJogo(1, owners);
        MainCam.GetComponent<PlayerRaycast>().playerCamOwner = owners[0];
        currentIndexOwner = 0;
        _roundOwner = owners[currentIndexOwner];
        AtivarTotensComida(SortearTilesRandom(15));
        AtivarTotensPontosMutagenicos(SortearTilesRandom(15));
        _CurrentTurno = 1;
        _inGameUi.UpdateLifeBarOwnerBase();
    }

    private void TurnosCinco()
    {
        DesativarTodosTotensAtivos(_TotensAtivos);
        AtivarTotensComida(SortearTilesRandom(_quantidadeDeComidaSpawn));
        AplicarEfeitosDesastreMenor(SortearBiomas(1));
        AtivarTotensPontosMutagenicos(SortearTilesRandom(_quantidadeDePontosMutagenicosSpawn));
    }

    private void TurnosDez()
    {
        DesativarTodosTotensAtivos(_TotensAtivos);
        AtivarTotensComida(SortearTilesRandom(_quantidadeDeComidaSpawn));
        AplicarEfeitosDesastreMaior(SortearBiomas(1));
        AtivarTotensPontosMutagenicos(SortearTilesRandom(_quantidadeDePontosMutagenicosSpawn));
    }
    public void PassarTurno()
    {
        RoundEndPiecesRoutine();
        if ((currentIndexOwner + 1) > (owners.Count - 1))
        {
            currentIndexOwner = 0;
            _currentTurno += 1;
            //Desastres
            if (_currentTurno % 5 == 0 && _currentTurno % 2 != 0)
            {
                TurnosCinco();
            }
            if (_currentTurno % 5 == 0 && _currentTurno % 2 == 0)
            {
                TurnosDez();
            }
            if (_currentTurno % 7 == 0 && _isUnderSmallDisaster == true)
            {
                AcabarDesastreMenor(GetNestedList(_tilesUnderSmallDissaster), _IndexDesastreMenor);
            }
            if (_currentTurno % 14 == 0 && _isUnderBigDisaster == true)
            {
                AcabarDesastreMaior(GetNestedList(_tilesUnderBigDissaster), _IndexDesastreMaior);
            }
            if (_CurrentTurno >= _MaxTurnos)
            {
                GameWin();
            }
            //if (_isUnderBigDisaster) 
            //{
            //    _progressionBigDisaster += 0.125f;
            //    Biome antes, depois;
            //    switch (_IndexDesastreMaior)
            //    {
            //        case 0:
            //            depois = Biome.Caatinga;
            //            break;
            //        case 1:
            //            depois = Biome.Pantanal;
            //            break;
            //        case 2:
            //            depois = Biome.Mata_das_Araucarias;
            //            break;
            //        case 3:
            //            depois = Biome.Mata_Atlantica;
            //            break;
            //        default:
            //            depois = Biome.Pampa;
            //            break;
            //    }
            //    antes = _tilesUnderBigDissaster[0].GetComponent<Tile>().Biome;
            //    Tile.TransitionTileTextureBigDisaster(_progressionBigDisaster,antes,depois, _tilesUnderBigDissaster);
            //}
        }
        else
        {
            currentIndexOwner += 1;
        }

        _roundOwner = owners[currentIndexOwner];
        MainCam.GetComponent<PlayerRaycast>().playerCamOwner = _roundOwner;
        GameObject.FindAnyObjectByType<InGameUi>().UpdateMutationPointText();
        _inGameUi.UpdateLifeBarOwnerBase();
        HandlerIndicadores();
    }
    private bool GameOver(Owner owner)
    {
        bool isGameOver = false;
        switch (owner)
        {
            case Owner.P1:
                bool isGameOver1 = _P1Pieces.Count == 0 ? true : false;
                isGameOver = isGameOver1;
                break;
            case Owner.P2:
                bool isGameOver2 = _P1Pieces.Count == 0 ? true : false;
                isGameOver = isGameOver2;
                break;
            case Owner.P3:
                bool isGameOver3 = _P1Pieces.Count == 0 ? true : false;
                isGameOver = isGameOver3;
                break;
            case Owner.P4:
                bool isGameOver4 = _P1Pieces.Count == 0 ? true : false;
                isGameOver = isGameOver4;
                break;
            case Owner.P5:
                bool isGameOver5 = _P1Pieces.Count == 0 ? true : false;
                isGameOver = isGameOver5;
                break;
            default:
                Debug.Log("Exce��o encontrada no owner de GameOver");
                break;
        }
        return isGameOver;
    }
    private void AllPlayersDied()
    {
        Time.timeScale = 0;
        _gameOverUiDeath.SetActive(true);
    }
    private void GameWin()
    {
        List<Owner> WinnerList = new List<Owner>();
        foreach (var owner in owners)
        {
            if (!GameOver(owner))
            {
                WinnerList.Add(owner);
                Debug.Log($"{owner} Venceu");


            }
        }
        analytics.AcabouJogo();
        _gameOverUi.SetActive(true);
    }

    private void RoundEndPiecesRoutine()
    {
        List<GameObject> pieces = new List<GameObject>();
        switch (owners[currentIndexOwner])
        {
            case Owner.P1:
                pieces = _P1Pieces;
                break;
            case Owner.P2:
                pieces = _P2Pieces;
                break;
            case Owner.P3:
                pieces = _P3Pieces;
                break;
            case Owner.P4:
                pieces = _P4Pieces;
                break;
            case Owner.P5:
                pieces = _P5Pieces;
                break;
            default:
                Debug.Log("Erro na rotina de final de turno (sele��o por owner)");
                break;
        }

        foreach (var piece in pieces)
        {
            piece.GetComponent<Piece>().EndTurnRoutine();
        }
    }
    #endregion

    #region Listas

    public bool VerificarSeExisteAction()
    {
        bool flag = false;
        foreach (var piece in _P1Pieces)
        {
            if (piece.GetComponent<Piece>().IsDuringAction)
            {
                flag = true;
            }
        }
        foreach (var piece in _P2Pieces)
        {
            if (piece.GetComponent<Piece>().IsDuringAction)
            {
                flag = true;
            }
        }
        foreach (var piece in _P3Pieces)
        {
            if (piece.GetComponent<Piece>().IsDuringAction)
            {
                flag = true;
            }
        }
        foreach (var piece in _P4Pieces)
        {
            if (piece.GetComponent<Piece>().IsDuringAction)
            {
                flag = true;
            }
        }
        foreach (var piece in _P5Pieces)
        {
            if (piece.GetComponent<Piece>().IsDuringAction)
            {
                flag = true;
            }
        }
        return flag;
    }

    // M�todo para criar uma lista aninhada fict�cia (apenas para teste)
    public List<List<GameObject>> GetNestedList(List<GameObject> innerList)
    {
        List<List<GameObject>> nestedList = new List<List<GameObject>>();

        // Preenche com algumas listas internas fict�cias
        for (int i = 0; i < 3; i++) // 3 listas internas
        {
            for (int j = 0; j < 5; j++) // 5 objetos em cada lista
            {
                GameObject obj = new GameObject($"Object_{i}_{j}");
                innerList.Add(obj);
            }

            nestedList.Add(innerList);
        }

        return nestedList;
    }

    // M�todo para descompactar List<List<GameObject>> em List<GameObject>
    public List<GameObject> GetNestedListFlattened(List<List<GameObject>> nestedList)
    {
        List<GameObject> flatList = new List<GameObject>();

        foreach (var innerList in nestedList)
        {
            flatList.AddRange(innerList); // Adiciona todos os elementos da lista interna
        }

        return flatList;
    }

    public void AdicionarPieceEmLista(Owner owner, GameObject piece)
    {
        switch (owner)
        {
            case Owner.P1:
                if (!_p1Pieces.Contains(piece))
                {
                    _p1Pieces.Add(piece);
                }
                break;
            case Owner.P2:
                if (!_p2Pieces.Contains(piece))
                {
                    _p2Pieces.Add(piece);
                }
                break;
            case Owner.P3:
                if (!_p3Pieces.Contains(piece))
                {
                    _p3Pieces.Add(piece);
                }
                break;
            case Owner.P4:
                if (!_p4Pieces.Contains(piece))
                {
                    _p4Pieces.Add(piece);
                }
                break;
            case Owner.P5:
                if (!_p5Pieces.Contains(piece))
                {
                    _p5Pieces.Add(piece);
                }
                break;
            default:
                Debug.Log("Exce��o encontrada ao adicionar Piece em lista");
                break;
        }
    }

    public void PieceMorreu(Owner owner, GameObject piece)
    {
        switch (owner)
        {
            case Owner.P1:
                if (_p1Pieces.Contains(piece))
                {
                    _p1Pieces.Remove(piece);
                }
                if (_p1Pieces.Count == 0)
                {

                }
                break;
            case Owner.P2:
                if (_p2Pieces.Contains(piece))
                {
                    _p2Pieces.Remove(piece);
                }
                if (_p2Pieces.Count == 0)
                {

                }
                break;
            case Owner.P3:
                if (_p3Pieces.Contains(piece))
                {
                    _p3Pieces.Remove(piece);
                }
                if (_p3Pieces.Count == 0)
                {

                }
                break;
            case Owner.P4:
                if (_p4Pieces.Contains(piece))
                {
                    _p4Pieces.Remove(piece);
                }
                if (_p4Pieces.Count == 0)
                {

                }
                break;
            case Owner.P5:
                if (_p5Pieces.Contains(piece))
                {
                    _p5Pieces.Remove(piece);
                }
                if (_p5Pieces.Count == 0)
                {

                }
                break;
            default:
                Debug.Log("Exce��o encontrada ao remover Piece em lista");
                break;
        }
    }

    #endregion

}
