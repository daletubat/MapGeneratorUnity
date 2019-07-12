using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWallSide
{
	TopWall = 0,
	BottomWall = 1,
	LeftWall = 2,
	RightWall = 3
}

public static class EWallSideExtension
{
	public static EWallSide Opposite(this EWallSide wallType)
	{
		//Should probably find a better base case for this
		return wallType == EWallSide.TopWall ? EWallSide.BottomWall :
			wallType == EWallSide.BottomWall ? EWallSide.TopWall :
			wallType == EWallSide.LeftWall ? EWallSide.RightWall :
			wallType == EWallSide.RightWall ? EWallSide.LeftWall :
			EWallSide.TopWall;
	}
}
