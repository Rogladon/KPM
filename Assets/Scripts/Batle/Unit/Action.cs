using UnityEngine;

namespace Battle.UnitCore {
	public abstract class Action : MonoBehaviour{
		public Sprite icon;
		public string nameSkill;
		public string description;
		public float price;
		[SerializeField]
		protected string nameAnimation;
		protected Unit unit;

		public string GetDescription() {
			return description;
		}
		
	}
}
