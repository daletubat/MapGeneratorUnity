using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapPieceLookUp : MonoBehaviour
{
	public List<MapNode> HallwayPieces;
	public List<RoomNode> RoomPieces;

	[SerializeField] private List<MapNode> RandomHallwayPiecePool = new List<MapNode>();
	[SerializeField] private List<RoomNode> RandomRoomPiecePool = new List<RoomNode>();

	public void Start()
	{
		generateRandomHallwayPiecePool();
		generateRandomRoomPiecePool();
	}

	#region HallwayPieces

	public GameObject GetRandomHallwayPieceWithConnection(EConnectionPoints requiredConnection)
	{
		if (RandomHallwayPiecePool.Count == 0)
			generateRandomHallwayPiecePool();

		List<GameObject> nodesWithRequiredConneciton = new List<GameObject>();

		foreach(var piece in RandomHallwayPiecePool)
		{
			if(piece.ConnectionPoints.Contains(requiredConnection))
			{
				nodesWithRequiredConneciton.Add(piece.gameObject);
			}
		}

		return nodesWithRequiredConneciton[Random.Range(0, nodesWithRequiredConneciton.Count-1)];
	}

	private void generateRandomHallwayPiecePool()
	{
		RandomHallwayPiecePool.Clear();

		foreach (var pieceInfo in HallwayPieces)
		{
			for (int i = 0; i < pieceInfo.Prevalance; i++)
			{
				RandomHallwayPiecePool.Add(pieceInfo);
			}
		}
	}

	#endregion

	#region RoomPieces

	public GameObject GetRandomRoomPieceWithRequiredConnectionPair(EConnectionPoints connectionPoint, EWallSide requiredWallType)
	{
		if (RandomHallwayPiecePool.Count == 0)
			generateRandomRoomPiecePool();

		List<GameObject> nodesWithRequiredWallType = new List<GameObject>();

		foreach (var piece in RandomRoomPiecePool)
		{
			if (piece.WallConnectionPairs.Contains(new KeyValuePair<EConnectionPoints, EWallSide>(connectionPoint, requiredWallType)))
			{
				nodesWithRequiredWallType.Add(piece.gameObject);
			}
		}

		return nodesWithRequiredWallType[Random.Range(0, nodesWithRequiredWallType.Count - 1)];
	}

	private void generateRandomRoomPiecePool()
	{
		RandomRoomPiecePool.Clear();

		foreach (var pieceInfo in RoomPieces)
		{
			for (int i = 0; i < pieceInfo.Prevalance; i++)
			{
				RandomRoomPiecePool.Add(pieceInfo);
			}
		}
	}

	#endregion
}
