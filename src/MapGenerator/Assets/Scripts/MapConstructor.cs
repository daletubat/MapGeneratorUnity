using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
	public int Depth;
	private GameObject rootGameObject;
	private MapNode rootNode;
	private MapPieceLookUp mapPieceLookUp; 

	private void Start()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;
		GetComponent<MapNode>().Build(transform.parent.gameObject, Depth);
	}
}
