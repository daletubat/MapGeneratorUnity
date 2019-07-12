using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapNode
{
	void Build(IConstructor root, GameObject rootParentGameObject, int depth);
}
