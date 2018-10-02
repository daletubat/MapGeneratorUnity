using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
	public List<EConnectionPoints> ConnectionPoints;
	public int Prevalance;

	public void Build(GameObject root, int depth)
	{
		if (depth <= 0) return;
		
		foreach(var connection in ConnectionPoints)
		{
			//This doesn't make sense. Should change this later.
			GameObject newPiece = root;

			switch(connection)
			{
				case EConnectionPoints.Up:
					newPiece = InstantiateNewPieceWithConnection(EConnectionPoints.Down, root);
					newPiece.transform.position = transform.position + new Vector3(0, 0, 2f);
					break;

				case EConnectionPoints.Down:
					newPiece = InstantiateNewPieceWithConnection(EConnectionPoints.Up, root);
					newPiece.transform.position = transform.position + new Vector3(0, 0, -2f);
					break;

				case EConnectionPoints.Left:
					newPiece = InstantiateNewPieceWithConnection(EConnectionPoints.Right, root);
					newPiece.transform.position = transform.position + new Vector3(-2, 0, 0f);
					break;

				case EConnectionPoints.Right:
					newPiece = InstantiateNewPieceWithConnection(EConnectionPoints.Left, root);
					newPiece.transform.position = transform.position + new Vector3(2, 0, 0f);
					break;
			}

			if(newPiece != root)
				newPiece.GetComponent<MapNode>().Build(root, depth - 1);
		}
	}

	private GameObject InstantiateNewPieceWithConnection(EConnectionPoints connection, GameObject root)
	{
		GameObject newPiece = Instantiate(Singletons.MapPieceLookUp.GetRandomPieceWithConnection(connection));
		newPiece.transform.parent = root.transform;
		newPiece.GetComponent<MapNode>().ConnectionPoints.Remove(connection);

		return newPiece;
	}
}
