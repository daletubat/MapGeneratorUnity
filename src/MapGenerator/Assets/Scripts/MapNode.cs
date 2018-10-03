using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
	public List<EConnectionPoints> ConnectionPoints;
	public int Prevalance;
	public PieceCoordinates Coordinates;

	public void Build(MapConstructor root, GameObject rootParentGameObject, int depth)
	{
		//Recursive Base Case
		if (depth <= 0) return;

		List<EConnectionPoints> pointsConnectedTo = new List<EConnectionPoints>();

		foreach(var connection in ConnectionPoints)
		{
			if (!canPlacePieceInConnection(root, connection))
			{
				Debug.Log($"Cannot place piece to the {connection} of piece {gameObject.name}.");
				continue;
			}

			//This doesn't make sense. Should change this later.
			GameObject newPiece = rootParentGameObject;

			switch(connection)
			{
				case EConnectionPoints.Up:
					newPiece = InstantiateNewPieceWithConnection(root, EConnectionPoints.Down, rootParentGameObject);
					break;

				case EConnectionPoints.Down:
					newPiece = InstantiateNewPieceWithConnection(root, EConnectionPoints.Up, rootParentGameObject);
					break;

				case EConnectionPoints.Left:
					newPiece = InstantiateNewPieceWithConnection(root, EConnectionPoints.Right, rootParentGameObject);
					break;

				case EConnectionPoints.Right:
					newPiece = InstantiateNewPieceWithConnection(root, EConnectionPoints.Left, rootParentGameObject);
					break;
			}

			if(newPiece != rootParentGameObject)
			{
				root.Map.Add(newPiece.GetComponent<MapNode>().Coordinates, newPiece.GetComponent<MapNode>());
				newPiece.GetComponent<MapNode>().Build(root, rootParentGameObject, depth - 1);
				pointsConnectedTo.Add(connection);
			}
		}

		foreach(var successfulConnection in pointsConnectedTo)
		{
			ConnectionPoints.Remove(successfulConnection);
		}
	}

	private GameObject InstantiateNewPieceWithConnection(MapConstructor root, EConnectionPoints connection, GameObject parentGameObject)
	{	
		GameObject newPiece = Instantiate(Singletons.MapPieceLookUp.GetRandomPieceWithConnection(connection));
		newPiece.transform.parent = parentGameObject.transform;
		newPiece.GetComponent<MapNode>().ConnectionPoints.Remove(connection);

		switch (connection)
		{
			//These connections are counter-intuitive. The up connection means that it's being placed below the caller.
			case EConnectionPoints.Up:
				newPiece.transform.position = transform.position + new Vector3(0, 0, -2f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z - 1);
				break;

			case EConnectionPoints.Down:
				newPiece.transform.position = transform.position + new Vector3(0, 0, 2f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z + 1);
				break;

			case EConnectionPoints.Left:
				newPiece.transform.position = transform.position + new Vector3(2, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X + 1, Coordinates.Z);
				break;

			case EConnectionPoints.Right:
				newPiece.transform.position = transform.position + new Vector3(-2, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X - 1, Coordinates.Z);
				break;
		}

		return newPiece;
	}

	private bool canPlacePieceInConnection(MapConstructor root, EConnectionPoints connection)
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

		return !root.Map.ContainsKey(nextCoordinates);
	}
}
