using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour, IMapNode
{
	//public List<EConnectionPoints> WallConnectionPoints;
	public RoomNodeSerializableDictionary WallConnectionPairs;
	public int Prevalance;
	public float PieceSize;
	public PieceCoordinates Coordinates;

	public void Build(IConstructor root, GameObject rootParentGameObject, int depth)
	{
		if (depth <= 0) return;

		List<EConnectionPoints> pointsConnectedTo = new List<EConnectionPoints>();

		foreach (var pair in WallConnectionPairs)
		{

			var connectionLocation = pair.Key;
			var wallType = pair.Value;

			if (!root.RequestSpaceAvailable(Coordinates, connectionLocation))
			{
				Debug.Log($"Cannot place piece to the {connectionLocation} of piece {gameObject.name}.");
				continue;
			}

			GameObject newPiece = InstantiateNewPieceWithWallType(connectionLocation.Opposite(), wallType, rootParentGameObject);

			root.Map.Add(newPiece.GetComponent<RoomNode>().Coordinates, newPiece.GetComponent<RoomNode>());
			pointsConnectedTo.Add(connectionLocation);
			StartCoroutine(WaitThenBuild(root, rootParentGameObject, depth, newPiece));
		}

		foreach (var successfulConnection in pointsConnectedTo)
			WallConnectionPairs.Remove(successfulConnection);
	}

	private GameObject InstantiateNewPieceWithWallType(EConnectionPoints connectionLocation, EWallSide wallType, GameObject parentGameObject)
	{
		GameObject newPiece = Instantiate(Singletons.MapPieceLookUp.GetRandomRoomPieceWithRequiredConnectionPair(connectionLocation, wallType));
		newPiece.transform.parent = parentGameObject.transform;
		newPiece.GetComponent<RoomNode>().WallConnectionPairs.Remove(connectionLocation);

		switch(connectionLocation)
		{
			case EConnectionPoints.Up:
				newPiece.transform.position = transform.position + new Vector3(0, 0, -PieceSize);
				newPiece.GetComponent<RoomNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z - 1);
				break;

			case EConnectionPoints.Down:
				newPiece.transform.position = transform.position + new Vector3(0, 0, PieceSize);
				newPiece.GetComponent<RoomNode>().Coordinates = new PieceCoordinates(Coordinates.X, Coordinates.Z + 1);
				break;

			case EConnectionPoints.Left:
				newPiece.transform.position = transform.position + new Vector3(PieceSize, 0, 0f);
				newPiece.GetComponent<RoomNode>().Coordinates = new PieceCoordinates(Coordinates.X + 1, Coordinates.Z);
				break;

			case EConnectionPoints.Right:
				newPiece.transform.position = transform.position + new Vector3(-PieceSize, 0, 0f);
				newPiece.GetComponent<RoomNode>().Coordinates = new PieceCoordinates(Coordinates.X - 1, Coordinates.Z);
				break;
		}

		return newPiece;
	}

	public IEnumerator WaitThenBuild(IConstructor root, GameObject rootParentGameObject, int depth, GameObject newPiece)
	{
		yield return new WaitForSeconds(0.05f);
		newPiece.GetComponent<RoomNode>().Build(root, rootParentGameObject, depth - 1);
	}
}

[System.Serializable]
public class RoomNodeSerializableDictionary : SerializableDictionaryBase<EConnectionPoints, EWallSide> { }


