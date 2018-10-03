using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EConnectionPoints
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3
}

public static class EConnectionPointsExtension
{
	public static EConnectionPoints Opposite(this EConnectionPoints connectionPoint)
	{
		//Should probably find a better base case for this
		return connectionPoint == EConnectionPoints.Up ? EConnectionPoints.Down :
			connectionPoint == EConnectionPoints.Down ? EConnectionPoints.Up :
			connectionPoint == EConnectionPoints.Left ? EConnectionPoints.Right :
			connectionPoint == EConnectionPoints.Right ? EConnectionPoints.Left :
			EConnectionPoints.Up;
	}
}
