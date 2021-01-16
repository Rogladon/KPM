using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.System {
	[DisallowMultipleComponent]
	public class PanelActionInfo : MonoBehaviour {
		[Header("Components")]
		[SerializeField]
		private Text nameSkill;
		[SerializeField]
		private Text description;
		[SerializeField]
		private Text price;

		private RectTransform rect;
		private void Start() {
			rect = GetComponent<RectTransform>();
		}
		public void ShowAndSetInfo(string name, string description, string price, RectTransform _rect) {
			gameObject.SetActive(true);
			transform.position = _rect.transform.position;
			Vector2 pos = rect.anchoredPosition;
			pos.y += rect.sizeDelta.y / 2+_rect.sizeDelta.y/2+20;
			rect.anchoredPosition = pos;
			nameSkill.text = name;
			this.description.text = description;
			this.price.text = price;
		}
		public void HideInfo() {
			gameObject.SetActive(false);
		}
	}
}