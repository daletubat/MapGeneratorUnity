using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapPieceLookUp : MonoBehaviour
{
	public List<MapNode> Pieces;
	[SerializeField] private List<MapNode> RandomPiecePool = new List<MapNode>();

	public void Start()
	{
		generateRandomPiecePool();
	}

	public MapNode GetRandomPiece()
	{
		return Pieces[Random.Range(0, Pieces.Count)];
	}

	public GameObject GetRandomPieceWithConnection(EConnectionPoints requiredConnection)
	{
		if (RandomPiecePool.Count == 0)
			generateRandomPiecePool();

		List<GameObject> nodesWithRequiredConneciton = new List<GameObject>();

		foreach(var piece in RandomPiecePool)
		{
			if(piece.ConnectionPoints.Contains(requiredConnection))
			{
				nodesWithRequiredConneciton.Add(piece.gameObject);
			}
		}

		return nodesWithRequiredConneciton[Random.Range(0, nodesWithRequiredConneciton.Count-1)];
	}

	private void generateRandomPiecePool()
	{
		RandomPiecePool.Clear();

		foreach (var pieceInfo in Pieces)
		{
			for (int i = 0; i < pieceInfo.Prevalance; i++)
			{
				RandomPiecePool.Add(pieceInfo);
			}
		}
	}
}
