using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    /*[SerializeField] private List<PuzzleSlot> slotPrefabs;
    
    [SerializeField] private Transform slotParent, pieceParent;
    [SerializeField] private PuzzlePiece piecePrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }
    void spawn()
    {
        var randomSet = slotPrefabs.OrderBy(s=>Random.value).Take(3).ToList();
        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(randomSet[i], slotParent.GetChild(i).position, Quaternion.identity);
            var SpawnedPiece = Instantiate(piecePrefab, pieceParent.GetChild(i).position, Quaternion.identity);
            SpawnedPiece.Init(spawnedSlot);
        }
    }*/
}
