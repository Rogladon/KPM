using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Batle.UnitCore;

namespace Batle.System {
	public class BattleHUD : MonoBehaviour {

		[Header("ComponentsUI")]
		[SerializeField]
		Text hp;
		[SerializeField]
		Text ap;
		[SerializeField]
		List<Button> actions = new List<Button>();

		private Unit unit = null;

		public void SetUnit(Unit unit) {
			this.unit = unit;
			for(int i = 0; i < unit.actions.Count; i++) {
				if (unit.actions[i].isActive()) {
					Action act = unit.actions[i] as Action;
					if (!act.isDefault) {
						actions[i].image.sprite = act.icon;
						actions[i].onClick.AddListener(() => {
							unit.SetAction(i);
						});
					}
				}
			}
		}
		public void ResetUnit() {
			unit = null;
			actions.ForEach(p => {
				p.image = null;
				p.onClick.RemoveAllListeners();
			});
		}

		void Update() {
			if (unit)
				SetInfo();
		}

		void SetInfo() {
			hp.text = unit.hp.ToString();
			ap.text = unit.ap.ToString();

		}
	}
}