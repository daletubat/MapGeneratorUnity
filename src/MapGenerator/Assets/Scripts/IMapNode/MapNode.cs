using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour, IMapNode
{
	public List<EConnectionPoints> ConnectionPoints;
	public int Prevalance;
	public float PieceSize;
	public PieceCoordinates Coordinates;
	public bool ContainsEndpoint;

	public void Build(IConstructor root, GameObject rootParentGameObject, int depth)
	{
		if (depth <= 0) return;

		List<EConnectionPoints> pointsConnectedTo = new List<EConnectionPoints>();

		foreach (var connection in ConnectionPoints)
		{
			if (!root.RequestSpaceAvailable(Coordinates, connection))
			{
				Debug.Log($"Cannot place piece to the {connection} of piece {gameObject.name}.");
				continue;
			}

			GameObject newPiece = InstantiateNewPieceWithConnection(connection.Opposite(), rootParentGameObject);

			root.Map.Add(newPiece.GetComponent<MapNode>().Coordinates, newPiece.GetComponent<MapNode>());
			pointsConnectedTo.Add(connection);
			StartCoroutine(WaitThenBuild(root, rootParentGameObject, depth, newPiece));
		}

		foreach(var successfulConnection in pointsConnectedTo)
			ConnectionPoints.Remove(successfulConnection);

		//TODO: Fix this
		Debug.Log($"{gameObject.name} has {ConnectionPoints.Count} connections left");
		ContainsEndpoint = ConnectionPoints.Count > 0;
	}

	private GameObject InstantiateNewPieceWithConnection(EConnectionPoints connection, GameObject parentGameObject)
	{	
		GameObject newPiece = Instantiate(Singletons.MapPieceLookUp.GetRandomHallwayPieceWithConnection(connection));
		newPiece.transform.parent = parentGameObject.transform;
		newPiece.GetComponent<MapNode>().ConnectionPoints.Remove(connection);

		switch (connection)
		{
			//These connections are counter-intuitive. The up connection means that it's being placed below the caller.
			case EConnectionPoints.Up:
				newPiece.transform.position = transform.position + new Vector3(0, 0, -PieceSize);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z - 1);
				break;

			case EConnectionPoints.Down:
				newPiece.transform.position = transform.position + new Vector3(0, 0, PieceSize);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z + 1);
				break;

			case EConnectionPoints.Left:
				newPiece.transform.position = transform.position + new Vector3(PieceSize, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X + 1, Coordinates.Z);
				break;

			case EConnectionPoints.Right:
				newPiece.transform.position = transform.position + new Vector3(-PieceSize, 0, 0f);
				newPiece.GetComponent<MapNode>().Coordinates = new PieceCoordinates(Coordinates.X - 1, Coordinates.Z);
				break;
		}

		return newPiece;
	}

	public IEnumerator WaitThenBuild(IConstructor root, GameObject rootParentGameObject, int depth, GameObject newPiece)
	{
		yield return new WaitForSeconds(0.05f);
		newPiece.GetComponent<MapNode>().Build(root, rootParentGameObject, depth - 1);
	}
}
