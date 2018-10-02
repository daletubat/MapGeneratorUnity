using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletons : MonoBehaviour
{
	[SerializeField] private MapPieceLookUp mapPieceLookUp;
	public static MapPieceLookUp MapPieceLookUp { get; private set; } 

	void Awake ()
	{
		MapPieceLookUp = mapPieceLookUp;	
	}
}
