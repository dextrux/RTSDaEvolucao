using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class RoundManager : MonoBehaviour
{
    //Atributos da classe
    [SerializeField]
    private Owner roundOwner;
    public Owner RoundOwner {  get { return roundOwner; } set { roundOwner = value; } }
    [SerializeField]
    List<GameObject> _atlanticaTiles;
    [SerializeField]
    List<GameObject> _araucariasTiles;
    [SerializeField]
    List<GameObject> _caatingaTiles;
    [SerializeField]
    List<GameObject> _pampaTiles;
    [SerializeField]
    List<GameObject> _pantanalTiles;
    [SerializeField]
    List<Piece> P1;
    [SerializeField]
    List<Piece> P2;
    [SerializeField]
    List<Piece> P3;
    [SerializeField]
    List<Piece> P4;
    [SerializeField]
    List<Piece> NPC;
    [SerializeField]
    List<Piece> _piecesSickByEffect;
    [SerializeField]
    List<GameObject> _tilesUnderBigDissaster;
    [SerializeField]
    List<GameObject> _tilesUnderSmallDissaster;
    [SerializeField]
    List<List<GameObject>> biomaTiles;
    int turnos;
    int _bigDisasterIndex;
    readonly int _maxTurnos = 15;
    public GameObject prefabPiece;
    public GameObject parentP1;
    public GameObject parentP2;
    public GameObject parentP3;
    public GameObject parentP4;
    public GameObject parentNPC;
    private bool _instanciarComida;
    public Camera P1Cam;
    public Camera P2Cam;
    public Camera P3Cam;
    public Camera P4Cam;
    public GameObject npcPrefabPiece; // Prefab alternativo para peças, caso necessário
    private Dictionary<Owner, List<Piece>> ownerPiecesDict;

    // ReadOnly strings
    private readonly string _atlanticaTileName = "Standard Atlantica";
    private readonly string _araucariasTileName = "Standard Araucarias";
    private readonly string _caatingaTileName = "Standard Caatinga";
    private readonly string _pampaTileName = "Standard Pampa";
    private readonly string _pantanalTileName = "Standard Pantanal";

    private void Awake()
    {
        BuscarTiles();
        InitializeOwnerPiecesDict();
        // Inicializa os dicionários
        ownerCameras = new Dictionary<Owner, Camera>
    {
        { Owner.P1, P1Cam },
        { Owner.P2, P2Cam },
        { Owner.P3, P3Cam },
        { Owner.P4, P4Cam },
        { Owner.NPC, null } // NPC não tem uma câmera associada
    };

        // Prefabs padrão e alternativo
        ownerPrefabs = new Dictionary<Owner, GameObject>
    {
        { Owner.P1, prefabPiece },
        { Owner.P2, prefabPiece },
        { Owner.P3, prefabPiece },
        { Owner.P4, prefabPiece },
        { Owner.NPC, npcPrefabPiece } // Sempre usa o prefab alternativo para NPC
    };
    }

    private void Update()
    {
        VerifyActionPlayerEnd(RoundOwner);


        if (VerifyRoundEnd())
        {
            RoundEndRoutine();
        }
        if(turnos != 0 && turnos % 5 == 0)
        {
            RoundMultiplosDeCinco();
        }
        if (turnos > 10 && turnos % 5 == 4)
        {            
            foreach(var tile in _tilesUnderBigDissaster)
            {
                switch (_bigDisasterIndex)
                {
                    case 0:
                        tile.GetComponent<Tile>().TransformarTile(Biome.Caatinga);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().TransformarTile(Biome.Pantanal);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().TransformarTile(Biome.Mata_das_Araucarias);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().TransformarTile(Biome.Mata_Atlantica);
                        break;
                    default:
                        Debug.Log("Exceção encontrada no sorteio do desastre maior");
                        break;
                }
            }
            _tilesUnderBigDissaster.Clear();
        }
        if (turnos > 5 && turnos % 5 == 2)
        {          
            _tilesUnderSmallDissaster.Clear();
        }
    }

    private void RoundEndRoutine()
    {
        foreach (var owner in ownerPiecesDict.Keys)
        {
            foreach (var piece in ownerPiecesDict[owner])
            {
                piece.PieceEndRoundRoutine();
            }
        }

        turnos++;
        RoundOwner = Owner.P1;
    }

    private void InitializeOwnerPiecesDict()
    {
        ownerPiecesDict = new Dictionary<Owner, List<Piece>>
    {
        { Owner.P1, P1 },
        { Owner.P2, P2 },
        { Owner.P3, P3 },
        { Owner.P4, P4 },
        { Owner.NPC, NPC }
    };
    }

    private bool VerifyGameEnd()
    {
        // Verifica se o número de turnos alcançou o máximo permitido
        if (turnos > _maxTurnos)
        {
            Debug.Log("Fim de jogo, número máximo de turnos alcançado.");
            return true;
        }
        return false;
    }
    private bool VerifyRoundEnd()
    {
        // Verifica se todas as peças em todas as listas específicas estão em estado de descanso (Resting = true)
        bool allPiecesResting = P1.All(piece => piece.Resting) &&
                                P2.All(piece => piece.Resting) &&
                                P3.All(piece => piece.Resting) &&
                                P4.All(piece => piece.Resting) &&
                                NPC.All(piece => piece.Resting);

        if (allPiecesResting)
        {
            Debug.Log("Fim da rodada: todas as peças estão em descanso.");
            return true;
        }

        // Caso nenhuma condição de fim de rodada seja atendida, retorna falso
        return false;
    }

    // Atualize o método VerifyActionPlayerEnd()
    public void VerifyActionPlayerEnd(Owner owner)
    {
        if (ownerPiecesDict.TryGetValue(owner, out List<Piece> piecesToCheck))
        {
            bool piecesResting = piecesToCheck.All(piece => piece.Resting);

            if (piecesResting)
            {
                Debug.Log($"Fim da rodada para {owner}: todas as peças estão em descanso.");
                RoundOwner = GetNextOwner(owner);
            }
        }
        else
        {
            Debug.LogWarning("Owner desconhecido: " + owner);
        }
    }

    private Owner GetNextOwner(Owner currentOwner)
    {
        switch (currentOwner)
        {
            case Owner.P1: return Owner.P2;
            case Owner.P2: return Owner.P3;
            case Owner.P3: return Owner.P4;
            case Owner.P4: return Owner.NPC;
            case Owner.NPC: return Owner.P1;
            default: return currentOwner; // Segurança em caso de um valor inesperado
        }
    }

    private void Start()
    {
        turnos = 1;
        biomaTiles = new List<List<GameObject>>
        {
        _araucariasTiles,
        _atlanticaTiles,
        _caatingaTiles,
        _pantanalTiles
        };
        InstanciarPeças();
        do
        {
            if (_instanciarComida)
            {
                AtivarTotens();
            }
        } while (!_instanciarComida);
        RoundOwner = Owner.P1;
    }
    private void RoundMultiplosDeCinco()
    {

        foreach (var tile in biomaTiles)
        {
            for (int i = 0; i < tile.Count; i++)
            {
                if (tile[i].GetComponent<Tile>().Totem.activeInHierarchy)
                {
                    tile[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().SetTotemAsInactive();
                }
            }
           
        }
        AtivarPontosMutagenicos();
        AtivarTotens();
        DesastreManager desastreManager = this.GetComponent<DesastreManager>();
        List<GameObject> list = desastreManager.SortearBiomaParaDesastre();
        if (turnos % 2 == 1)
        {
            _tilesUnderSmallDissaster = list;    
            desastreManager.SortearEventoMenor(list);
        }
        else if (turnos % 2 == 0)
        {
            _tilesUnderBigDissaster = list;
            _bigDisasterIndex = desastreManager.SortearEventoMaior(list);
        }
    }
     private void AtivarPontosMutagenicos() { }

    

    private Dictionary<Owner, Camera> ownerCameras;
    private Dictionary<Owner, GameObject> ownerPrefabs;

    private void InstanciarPeças()
    {
        GameObject newObject;
        Vector3 instantiatePosition;
        System.Random random = new System.Random();

        // Lista dos proprietários e seus respectivos pais
        Dictionary<Owner, GameObject> ownersParents = new Dictionary<Owner, GameObject>
    {
        { Owner.P1, parentP1 },
        { Owner.P2, parentP2 },
        { Owner.P3, parentP3 },
        { Owner.P4, parentP4 },
        { Owner.NPC, parentNPC }
    };

        // Listas de peças específicas para cada Owner
        Dictionary<Owner, List<Piece>> ownerPieceLists = new Dictionary<Owner, List<Piece>>
    {
        { Owner.P1, P1 },
        { Owner.P2, P2 },
        { Owner.P3, P3 },
        { Owner.P4, P4 },
        { Owner.NPC, NPC }
    };

        // Para cada bioma e para cada proprietário, geramos 3 peças
        foreach (var tiles in biomaTiles)
        {
            foreach (var ownerEntry in ownersParents)
            {
                Owner owner = ownerEntry.Key;
                GameObject parent = ownerEntry.Value;
                List<Piece> ownerPiecesList = ownerPieceLists[owner];
                GameObject prefabToUse = ownerPrefabs[owner]; // Define o prefab padrão para o owner

                // Verifica se a câmera correspondente é null
                if (ownerCameras[owner] == null)
                {
                    prefabToUse = npcPrefabPiece; // Se a câmera é null, usa o prefab alternativo
                }

                for (int j = 0; j < 1; j++)
                {
                    int i;
                    do
                    {
                        i = random.Next(0, tiles.Count);
                        instantiatePosition = new Vector3(tiles[i].transform.position.x, 5f, tiles[i].transform.position.z);
                    } while (tiles[i].GetComponent<Tile>().Owner != Owner.None && tiles[i].GetComponent<Tile>().TileType == TileType.Posicionamento);

                    // Instancia e configura a nova peça
                    newObject = Instantiate(prefabToUse, instantiatePosition, Quaternion.identity);

                    Piece newPiece = newObject.GetComponent<Piece>();
                    newPiece.Owner = owner;
                    newPiece.Level = PieceLevel.One;
                    newPiece.Diet = PieceDiet.Herbivore;
                    newPiece.SetPieceAtributes(newPiece);
                    newObject.transform.SetParent(parent.transform);

                    // Adiciona a peça à lista específica de acordo com o Owner
                    ownerPiecesList.Add(newPiece);
                }
            }
        }

        _instanciarComida = true;
    }




    private void BuscarTiles()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(_atlanticaTileName))
            {
                _atlanticaTiles.Add(obj);
            }
            else if (obj.name.Contains(_araucariasTileName))
            {
                _araucariasTiles.Add(obj);
            }
            else if (obj.name.Contains(_caatingaTileName))
            {
                _caatingaTiles.Add(obj);
            }
            else if (obj.name.Contains(_pampaTileName))
            {
                _pampaTiles.Add(obj);
            }
            else if (obj.name.Contains(_pantanalTileName))
            {
                _pantanalTiles.Add(obj);
            }
            else
            {
                Debug.Log($"{obj.name} não pertence aos tiles especificados.");
            }
        }

        // Para verificar a quantidade de objetos encontrados em cada lista (opcional)
        Debug.Log($"Tiles Atlântica: {_atlanticaTiles.Count}");
        Debug.Log($"Tiles Araucárias: {_araucariasTiles.Count}");
        Debug.Log($"Tiles Caatinga: {_caatingaTiles.Count}");
        Debug.Log($"Tiles Pampa: {_pampaTiles.Count}");
        Debug.Log($"Tiles Pantanal: {_pantanalTiles.Count}");
    }


     private List<GameObject> SortearTilesParaAtivarTotens()
    {
        System.Random random = new System.Random();
        List<GameObject> list = new List<GameObject>();

        foreach (var tiles in biomaTiles)
        {
            int count = 0;

            while (count < 10)
            {
                int decider = random.Next(0, tiles.Count);
                GameObject tile = tiles[decider];
                Tile tileComponent = tile.GetComponent<Tile>();

                // Verifica se o tile tem Owner.None e TileType.Posicionamento
                if (!list.Contains(tile) && !tileComponent.TileRaycastForPiece() && tileComponent.TileType == TileType.Posicionamento)
                {
                    list.Add(tile);
                    count++;
                }
            }
        }

        return list;
    }

    public void AtivarTotens()
    {
        List<GameObject> list = SortearTilesParaAtivarTotens();

        foreach (var totem in list)
        {
            FoodSize size = (FoodSize)DefinirProporçãoComida();
            totem.GetComponent<Tile>().Totem.GetComponent<FoodTotem>().SetTotemAsActive(size);
        }
    }
    public int DefinirProporçãoComida()
    {
        System.Random random = new System.Random();
        return random.Next(1, 6);
    }

}
