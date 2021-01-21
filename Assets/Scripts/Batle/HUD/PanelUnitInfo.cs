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
		Text strenght;
		[SerializeField]
		Text defense;
		[SerializeField]
		Text heelHp;
		[SerializeField]
		Text nameUnit;

		RectTransform rect => GetComponent<RectTransform>();


		public void SetInfo(Unit unit, Vector2 pos) {
			gameObject.SetActive(true);
			rect.anchoredPosition = pos+rect.sizeDelta;
			textTeam.text = $"Team: {unit.team}";
			hp.text = $"HP: {unit.hp}/{unit.unitState.maxHp}";
			ap.text = $"AP: {unit.ap}/{unit.unitState.maxAp}";
			strenght.text = $"Сила: {unit.unitState.strenght}";
			defense.text = $"Защита: {unit.unitState.defense}";
			heelHp.text = $"Восстановление AP: {unit.unitState.heelAp}";
			nameUnit.text = unit.unitInfo.nameUnit;
		}
		public void ResetInfo() {
			gameObject.SetActive(false);
		}
	}
}
