using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCoordinates //: MonoBehaviour
{
	public int X;
	public int Z;

	public PieceCoordinates(int x, int z)
	{
		X = x;
		Z = z;
	}

	public override bool Equals(object obj)
	{
		var coordinates = obj as PieceCoordinates;
		return coordinates != null &&
			   X == coordinates.X &&
			   Z == coordinates.Z;
	}

	public override int GetHashCode()
	{
		var hashCode = 1911744652;
		hashCode = hashCode * -1521134295 + X.GetHashCode();
		hashCode = hashCode * -1521134295 + Z.GetHashCode();
		return hashCode;
	}
}
