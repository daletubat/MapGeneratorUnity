using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
	public int Depth;
	public bool UseMaterialColors;

	public Dictionary<PieceCoordinates, MapNode> Map;
	public MapNode OriginNode;

	private void Start()
	{
		DevTools.SetDebugOutputs(false, false);

		Random.InitState((int)System.DateTime.Now.Ticks);

		Map = new Dictionary<PieceCoordinates, MapNode>();

		OriginNode.Coordinates = new PieceCoordinates(0, 0);
		Map.Add(OriginNode.Coordinates, OriginNode);

		OriginNode.Build(Map, gameObject, Depth);
		if(!UseMaterialColors)
		{
			Material newMat = new Material(Shader.Find("Standard"));
			foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			{
				mr.material = newMat;
			}
		}
	}
}
