using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConstructor
{
	Dictionary<PieceCoordinates, IMapNode> Map { get; }
	bool RequestSpaceAvailable(PieceCoordinates coordinates, EConnectionPoints connection);
}
