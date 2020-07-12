using System;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
	[CreateAssetMenu(fileName = "WeaponDropData", menuName = "ScriptableObjects/WeaponDropData", order = 1)]
	public class WeaponDropsList : ScriptableObject
	{
		public List<Vector3> positions;
		public List<WeaponType> types;
	}
	public enum WeaponType
	{
		gerenade,
		missleLuncher,
		plasma,
		NULL
	}
}
