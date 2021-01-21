using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UnitCore;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Battle.System {
	public class ActionIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
		private int index;
		private Unit unit;
		private PanelActionInfo panelInfo;
		private Action action;
		private RectTransform rect;
		public void Start() {
			rect = GetComponent<RectTransform>();
		}
		public void Init(Unit unit, int index, PanelActionInfo panel) {
			panelInfo = panel;
			action = unit.actions[index] as Action;
			GetComponent<Image>().sprite = action.icon;
			this.index = index;
			this.unit = unit;
			GetComponent<Button>().onClick.AddListener(() => SetAction());
		}
		public void Reset() {
			action = null;
			GetComponent<Image>().sprite = null;
			//this.index = -1;
			this.unit = null;
		}

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
			if(action)
				panelInfo.ShowAndSetInfo(action.nameSkill, action.GetDescription(), action.price.ToString(), rect);
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
			if(action)
				panelInfo.HideInfo();
		}

		void SetAction() {
			unit.SetAction(index);
		}


	}
}
