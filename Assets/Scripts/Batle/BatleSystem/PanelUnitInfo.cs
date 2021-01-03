using UnityEngine;
using UnityEngine.UI;
using Battle.UnitCore;

namespace Battle.System {
	public class PanelUnitInfo : MonoBehaviour{
		[SerializeField]
		Text textTeam;
		[SerializeField]
		Text hp;
		[SerializeField]
		Text ap;
		[SerializeField]
		Text nameUnit;

		RectTransform rect => GetComponent<RectTransform>();


		public void SetInfo(Unit unit, Vector2 pos) {
			gameObject.SetActive(true);
			rect.anchoredPosition = pos+rect.sizeDelta;
			textTeam.text = $"Team: {unit.team}";
			hp.text = $"HP: {unit.hp}";
			ap.text = $"AP: {unit.ap}";
			nameUnit.text = unit.unitInfo.nameUnit;
		}
		public void ResetInfo() {
			gameObject.SetActive(false);
		}
	}
}
