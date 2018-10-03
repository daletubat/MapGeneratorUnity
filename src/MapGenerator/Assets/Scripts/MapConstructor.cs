using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
	public int Depth;

	public Dictionary<PieceCoordinates, MapNode> Map;
	public MapNode OriginNode;

	private void Start()
	{
		Random.InitState((int)System.DateTime.Now.Ticks);

		Map = new Dictionary<PieceCoordinates, MapNode>();

		OriginNode.Coordinates = new PieceCoordinates(0, 0);
		Map.Add(OriginNode.Coordinates, OriginNode);

		OriginNode.Build(this, gameObject, Depth);
	}
}
