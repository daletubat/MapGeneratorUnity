using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
	public List<EConnectionPoints> ConnectionPoints;
	public int Prevalance;
	public float PieceSize;
	public PieceCoordinates Coordinates;
	public bool ContainsEndpoint;

	public void Build(Dictionary<PieceCoordinates, MapNode> coordinateMap, GameObject rootParentGameObject, int depth)
	{
		if (depth <= 0) return;


		List<EConnectionPoints> successfulConnectionsMade = new List<EConnectionPoints>();

		foreach (var connection in ConnectionPoints)
		{
			if (!CanPlacePieceInConnection(connection, coordinateMap))
			{
				DevTools.Log($"Cannot place piece to the {connection} of piece {gameObject.name}.");
				continue;
			}

			GameObject newPiece = InstantiateRandomPieceAtConnection(connection, rootParentGameObject);

			coordinateMap.Add(newPiece.GetComponent<MapNode>().Coordinates, newPiece.GetComponent<MapNode>());
			successfulConnectionsMade.Add(connection);
			StartCoroutine(WaitThenBuildAtNode(newPiece.GetComponent<MapNode>(), coordinateMap, rootParentGameObject, depth));
		}

		foreach(var successfulConnection in successfulConnectionsMade)
			ConnectionPoints.Remove(successfulConnection);

		//TODO: Fix this
		DevTools.Log($"{gameObject.name} has {ConnectionPoints.Count} connections left");
		ContainsEndpoint = ConnectionPoints.Count > 0;
	}

	private GameObject InstantiateRandomPieceAtConnection(EConnectionPoints connection, GameObject parentGameObject)
	{
		//If we want to connect at a connection point, we need a piece with the opposite connection.
		var connectionPointForNewPiece = connection.Opposite();

		GameObject newPiece = Instantiate(Singletons.MapPieceLookUp.GetRandomPieceWithConnection(connectionPointForNewPiece));
		newPiece.transform.parent = parentGameObject.transform;
		newPiece.GetComponent<MapNode>().ConnectionPoints.Remove(connectionPointForNewPiece);

		PlacePieceAtConnection(newPiece, connection);

		return newPiece;
	}

	private void PlacePieceAtConnection(GameObject newPiece, EConnectionPoints connection)
	{
		switch (connection)
		{
			case EConnectionPoints.Up:
				newPiece.transform.position = transform.position + new Vector3(0, 0, PieceSize);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z + 1);
				break;

			case EConnectionPoints.Down:
				newPiece.transform.position = transform.position + new Vector3(0, 0, -PieceSize);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z - 1);
				break;

			case EConnectionPoints.Left:
				newPiece.transform.position = transform.position + new Vector3(-PieceSize, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X - 1, Coordinates.Z);
				break;

			case EConnectionPoints.Right:
				newPiece.transform.position = transform.position + new Vector3(PieceSize, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X + 1, Coordinates.Z);
				break;
		}
	}

	private bool CanPlacePieceInConnection(EConnectionPoints connection, Dictionary<PieceCoordinates, MapNode> coordinateMap)
	{
		PieceCoordinates nextCoordinates;
		switch (connection)
		{
			default:
				nextCoordinates = new PieceCoordinates(0, 0);
				break;

			case EConnectionPoints.Up:
				nextCoordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z + 1);
				break;
			case EConnectionPoints.Down:
				nextCoordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z - 1);
				break;
			case EConnectionPoints.Left:
				nextCoordinates = new PieceCoordinates(Coordinates.X - 1, Coordinates.Z);
				break;
			case EConnectionPoints.Right:
				nextCoordinates = new PieceCoordinates(Coordinates.X + 1, Coordinates.Z);
				break;
		}

		return !coordinateMap.ContainsKey(nextCoordinates);
	}

	public IEnumerator WaitThenBuildAtNode(MapNode node, Dictionary<PieceCoordinates, MapNode> coordinateMap, GameObject rootParentGameObject, int depth)
	{
		yield return new WaitForSeconds(0.05f);
		node.Build(coordinateMap, rootParentGameObject, depth - 1);
	}
}
