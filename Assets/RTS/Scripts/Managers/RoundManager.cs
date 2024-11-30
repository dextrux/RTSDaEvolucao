using DG.Tweening;
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
    //Atributos
    #region Valores de Referência
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
    Vector3 _pieceHeight = new Vector3(0f, 5f, 0f);
    [SerializeField]
    List<Owner> owners = new List<Owner>();
    public Camera MainCam;
    private Owner _roundOwner;
    private int currentIndexOwner;
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

    #region Lista de Peças
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

    #region
    #endregion

    //Propriedades
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
    #endregion

    //Métodos
    #region Rotinas Unity
    private void Awake()
    {
        owners.Add(Owner.P1); 
        owners.Add(Owner.P2); 
        //owners.Add(Owner.P3); 
        //owners.Add(Owner.P4); 
        //owners.Add(Owner.P5); 
        BuscarTiles();
    }

    private void Start()
    {
        _quantidadeDePontosMutagenicosSpawn = owners.Count * 3;
        _quantidadeDeComidaSpawn = owners.Count * 5;
        PrimeiroTurno();
    }
    #endregion

    #region Pieces
    public void InstanciarPeçasParaJogo(int quantidade, List<Owner> owners)
    {
        for (int i = 0; i < quantidade; i++)
        {
            foreach (var owner in owners)
            {
                // Obtenha as posições necessárias
                Vector3[] positions = new Vector3[4];
                positions[0] = SortearTilesParaPieces(1, 2)[0].transform.position + _pieceHeight;
                positions[1] = SortearTilesParaPieces(1, 3)[0].transform.position + _pieceHeight;
                positions[2] = SortearTilesParaPieces(1, 4)[0].transform.position + _pieceHeight;
                positions[3] = SortearTilesParaPieces(1, 5)[0].transform.position + _pieceHeight;

                // Instancie e inicialize as peças
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
                Debug.Log("Erro no parenteamento de objeto");
                break;
        }
        Piece.InicializarPiece(
            newObject,
            newObject.GetComponent<Piece>().PieceRaycastForTile().gameObject,
            PieceDiet.Herbivore,
            owner,
            1
        );      
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
                Debug.Log($"{obj.name} não pertence aos tiles especificados.");
            }
        }

        // Para verificar a quantidade de objetos encontrados em cada lista (opcional)
        Debug.Log($"Tiles One: {_biomaOne.Count}");
        Debug.Log($"Tiles Two: {_biomaTwo.Count}");
        Debug.Log($"Tiles Three: {_biomaThree.Count}");
        Debug.Log($"Tiles Four: {_biomaFour.Count}");
        Debug.Log($"Tiles Five: {_biomaFive.Count}");
    }
    List<GameObject> SortearTilesRandom(int quantidade)
    {
        List<GameObject> tiles = new List<GameObject>();
        List<GameObject> chosenTileList = new List<GameObject>();
        System.Random random = new System.Random();
        while (tiles.Count < quantidade)
        {
            // Seleciona uma lista de tiles aleatória
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
                    Debug.Log("Exceção ao selecionar bioma");
                    break;
            }

            // Seleciona um tile aleatório dentro da lista
            GameObject chosenTile = chosenTileList[random.Next(0, chosenTileList.Count)];

            // Adiciona o tile apenas se ainda não estiver na lista
            if (!tiles.Contains(chosenTile))
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
                    Debug.Log("Exceção ao selecionar bioma");
                    break;
            }

            // Seleciona um tile aleatório dentro da lista
            GameObject chosenTile = chosenTileList[random.Next(0, chosenTileList.Count)];

            // Adiciona o tile apenas se ainda não estiver na lista
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
            // Seleciona uma lista de tiles aleatória
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
                    Debug.Log("Exceção ao selecionar bioma");
                    break;
            }            // Adiciona o tile apenas se ainda não estiver na lista
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
    int DefinirProporçãoComida()
    {
        System.Random random = new System.Random();
        return random.Next(1, 6);
    }
    void AtivarTotensComida(List<GameObject> list)
    {
        foreach (var tiles in list)
        {
            TotemType totemType = (TotemType)DefinirProporçãoComida();
            tiles.GetComponent<Tile>().Totem.GetComponent<Totem>().ActivateTotem(totemType);
            _TotensAtivos.Add(tiles.GetComponent<Tile>().Totem);
        }
    }
    void AtivarTotensPontosMutagenicos(List<GameObject> list)
    {
        foreach (var tiles in list)
        {
            TotemType totemType = TotemType.Ponto_Mutagênico;
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
        InstanciarPeçasParaJogo(1, owners);
        MainCam.GetComponent<PlayerRaycast>().playerCamOwner = owners[0];
        currentIndexOwner = 0;
        _roundOwner = owners[currentIndexOwner];
        AtivarTotensComida(SortearTilesRandom(15));
        AtivarTotensPontosMutagenicos(SortearTilesRandom(15));
        _CurrentTurno = 1;

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
        if (GameObject.FindAnyObjectByType<InGameUi>()._NextTurnAdvice.ClassListContains("turn-screen-open"))
        {
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
            }
            else
            {
                currentIndexOwner += 1;
            }

            _roundOwner = owners[currentIndexOwner];
            Debug.Log(owners[currentIndexOwner]);
            MainCam.GetComponent<PlayerRaycast>().playerCamOwner = _roundOwner;
        }
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
                Debug.Log("Exceção encontrada no owner de GameOver");
                break;
        }
        return isGameOver;
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
    }

    #endregion

    #region Listas
    // Método para criar uma lista aninhada fictícia (apenas para teste)
    public List<List<GameObject>> GetNestedList(List<GameObject> innerList )
    {
        List<List<GameObject>> nestedList = new List<List<GameObject>>();

        // Preenche com algumas listas internas fictícias
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

    // Método para descompactar List<List<GameObject>> em List<GameObject>
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
            default: Debug.Log("Exceção encontrada ao adicionar Piece em lista");
                break;
        }
    }

    

    #endregion

}
