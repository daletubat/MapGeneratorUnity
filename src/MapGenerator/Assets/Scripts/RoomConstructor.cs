using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConstructor : MonoBehaviour, IConstructor
{
	public int Depth;
	public bool UseMaterialColors;

	public Dictionary<PieceCoordinates, IMapNode> Map { get; private set; } 
	public RoomNode OriginNode;

	private void Start()
	{
		Random.InitState((int)System.DateTime.Now.Ticks);

		Map = new Dictionary<PieceCoordinates, IMapNode>();

		OriginNode.Coordinates = new PieceCoordinates(0, 0);
		Map.Add(OriginNode.Coordinates, OriginNode);

		OriginNode.Build(this, gameObject, Depth);
		if (!UseMaterialColors)
		{
			Material newMat = new Material(Shader.Find("Standard"));
			foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			{
				mr.material = newMat;
			}
		}
	}

	public bool RequestSpaceAvailable(PieceCoordinates coordinates, EConnectionPoints connection)
	{
		PieceCoordinates nextCoordinates;
		switch (connection)
		{
			default:
				nextCoordinates = new PieceCoordinates(0, 0);
				break;

			case EConnectionPoints.Up:
				nextCoordinates = new PieceCoordinates(coordinates.X, coordinates.Z + 1);
				break;
			case EConnectionPoints.Down:
				nextCoordinates = new PieceCoordinates(coordinates.X, coordinates.Z - 1);
				break;
			case EConnectionPoints.Left:
				nextCoordinates = new PieceCoordinates(coordinates.X - 1, coordinates.Z);
				break;
			case EConnectionPoints.Right:
				nextCoordinates = new PieceCoordinates(coordinates.X + 1, coordinates.Z);
				break;
		}

		return !Map.ContainsKey(nextCoordinates);
	}
}
