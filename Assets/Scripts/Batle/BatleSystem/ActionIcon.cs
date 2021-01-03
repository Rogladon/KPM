using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UnitCore;
using UnityEngine.UI;

namespace Battle.System {
	public class ActionIcon : MonoBehaviour {
		private int index;
		private Unit unit;
		public void Init(Unit unit, int index) {
			Action actionInfo = unit.actions[index] as Action;
			GetComponent<Image>().sprite = actionInfo.icon;
			this.index = index;
			this.unit = unit;
			GetComponent<Button>().onClick.AddListener(() => SetAction());
		}
		void SetAction() {
			unit.SetAction(index);
		}
	}
}
