using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Battle.UnitCore;
using UnityEngine.EventSystems;
namespace Battle.System {
	public class BattleHUD : MonoBehaviour {

		[Header("ComponentsUI")]
		[SerializeField]
		Text currentTeam;
		[SerializeField]
		GameObject unitPanel;
		[SerializeField]
		Text hp;
		[SerializeField]
		Text ap;
		[SerializeField]
		Text minusAp;
		[SerializeField]
		PanelUnitInfo panelInfoUnit;
		[SerializeField]
		PanelActionInfo panelActionInfo;
		[SerializeField]
		List<ActionIcon> actions = new List<ActionIcon>();

		private Unit unit = null;
		private RectTransform canvas;

		private void Start() {
			canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		}

		public void SetUnit(Unit unit) {
			this.unit = unit;
			unitPanel.SetActive(true);
			for(int i = 0; i < unit.actions.Count; i++) {
				if (unit.actions[i].isActive()) {
					actions[i].Init(unit, i, panelActionInfo);
				}
			}
		}
		public void ResetUnit() {
			unit = null;
			unitPanel.SetActive(false);
		}

		void Update() {
			if (unit)
				SetInfo();
		}

		void SetInfo() {
			hp.text = unit.hp.ToString();
			ap.text = unit.ap.ToString();

		}
		public void GetInfoUnit(Unit unit, Vector2 screenPosition) {
			Vector2 pos = screenPosition;
			pos.x *= canvas.sizeDelta.x;
			pos.y *= canvas.sizeDelta.y;
			panelInfoUnit.SetInfo(unit, pos);
		}
		public void ResetInfoUnit() {
			panelInfoUnit.ResetInfo();
		}

		public void SetTeam(int team) {
			currentTeam.text = team.ToString();
		}
		public void SetApHud(int ap) {
			minusAp.text = $"-{ap}";
		}
	}
}