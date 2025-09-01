using UnityEngine;

/// <summary>
/// Programmatically builds an 8x8 chess board using assets generated into Resources.
/// Attach to an empty GameObject in your scene.
/// </summary>
public class BoardBuilder2D : MonoBehaviour
{
    [Header("Layout")]
    public Transform boardRoot;    // parent for tiles and pieces
    public float tileWorldSize = 1f; // 1 unit per tile in 2D world

    [Header("Options")]
    public bool createPieces = false;  // set true later to drop pieces

    // Cached assets from Resources
    Sprite _tileLight, _tileDark;
    GameObject _tilePrefab, _piecePrefab;
    Sprite[] _pieceSprites; // 12 chess pieces

    void Awake()
    {
        LoadAssets();
        if (boardRoot == null)
        {
            var root = new GameObject("BoardRoot");
            root.transform.SetParent(transform, false);
            boardRoot = root.transform;
        }
        BuildBoard();
        if (createPieces) PlaceStartingPieces();
    }

    void LoadAssets()
    {
        _tileLight = Resources.Load<Sprite>("Sprites/TileLight");
        _tileDark = Resources.Load<Sprite>("Sprites/TileDark");
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _piecePrefab = Resources.Load<GameObject>("Prefabs/Piece");

        _pieceSprites = new Sprite[12];
        for (int i = 0; i < 12; i++)
            _pieceSprites[i] = Resources.Load<Sprite>($"Sprites/Piece_{i}");

        if (_tileLight == null || _tileDark == null || _tilePrefab == null)
            Debug.LogError("[Chess2D] Missing generated assets. Run Tools → Chess2D → Generate Assets (2D).");
    }

    void BuildBoard()
    {
        float unit = tileWorldSize;

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                var go = Instantiate(_tilePrefab, boardRoot);
                go.name = $"Tile_{c}_{r}";

                var sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = ((c + r) % 2 == 0) ? _tileLight : _tileDark;

                // position
                go.transform.localPosition = new Vector3(c * unit, r * unit, 0f);

                // auto-scale so tile fits exactly into 1x1 world unit
                float scale = ScaleToWidth(sr.sprite, tileWorldSize, 1f);
                go.transform.localScale = Vector3.one * scale;
            }
        }

        CenterCamera();
    }

    void CenterCamera()
    {
        var cam = Camera.main;
        if (cam == null) return;
        cam.orthographic = true;
        cam.transform.position = new Vector3(3.5f * tileWorldSize, 3.5f * tileWorldSize, -10f);
        cam.orthographicSize = 4.5f * tileWorldSize; // fits 8 tiles vertically with margin
    }

    void PlaceStartingPieces()
    {
        // Pawns
        for (int c = 0; c < 8; c++)
        {
            SpawnPiece(0, c, 1); // white pawns
            SpawnPiece(6, c, 6); // black pawns
        }

        // Rooks
        SpawnPiece(3, 0, 0); SpawnPiece(3, 7, 0);
        SpawnPiece(9, 0, 7); SpawnPiece(9, 7, 7);

        // Knights
        SpawnPiece(1, 1, 0); SpawnPiece(1, 6, 0);
        SpawnPiece(7, 1, 7); SpawnPiece(7, 6, 7);

        // Bishops
        SpawnPiece(2, 2, 0); SpawnPiece(2, 5, 0);
        SpawnPiece(8, 2, 7); SpawnPiece(8, 5, 7);

        // Queens
        SpawnPiece(4, 3, 0);   // white queen
        SpawnPiece(10, 3, 7);  // black queen

        // Kings
        SpawnPiece(5, 4, 0);   // white king
        SpawnPiece(11, 4, 7);  // black king
    }

    void SpawnPiece(int spriteIndex, int file, int rank)
    {
        var go = Instantiate(_piecePrefab, boardRoot);
        go.name = $"Piece_{spriteIndex}_{file}_{rank}";

        var sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = _pieceSprites[spriteIndex];
        sr.sortingOrder = 10; // render above tiles

        // auto-scale to fit tile with a margin so outline doesn’t touch edges
        float scale = ScaleToWidth(sr.sprite, tileWorldSize, 0.90f);
        go.transform.localScale = Vector3.one * scale;

        // center piece on its square
        go.transform.localPosition = new Vector3(file * tileWorldSize, rank * tileWorldSize, -0.1f);
    }

    /// <summary>
    /// Calculates scale factor so a sprite fits a target world width.
    /// </summary>
    float ScaleToWidth(Sprite s, float targetWorldWidth, float margin = 1f)
    {
        if (s == null) return 1f;
        float w = s.bounds.size.x;
        if (w <= 0f) w = s.rect.width / s.pixelsPerUnit;
        return (targetWorldWidth * margin) / w;
    }
}
