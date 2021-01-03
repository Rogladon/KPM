using UnityEngine;

namespace Battle.UnitCore {
	public abstract class Action : MonoBehaviour{
		public Sprite icon;
		public string nameSkill;
		public string description;
		public float price;
		protected Unit unit;
		
	}
}
