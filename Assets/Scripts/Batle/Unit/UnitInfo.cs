using UnityEngine;

namespace Battle.UnitCore {
	[CreateAssetMenu(fileName = "DefaultUnitInfo", menuName = "UnitInfo", order = 0)]
	public class UnitInfo : ScriptableObject {
		public string nameUnit;
		public Unit prefab;
		public UnitState state;
	}
}
