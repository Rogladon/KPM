using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;
using UnityEngine.AI;
using System.Linq;

namespace Battle.UnitCore.Actions {
	public class JumpMove : Action, IAction {
		Vector3? position = null;
		//LineRenderer line;
		Transform aim;
		[SerializeField]
		Color defaultColor;
		[SerializeField]
		Color notApColor;
		[SerializeField]
		float _distance;
		[SerializeField]
		float height;

		protected float distance => _distance;
		BattleStaticContainer cont => BattleStaticContainer.instance;

		bool fly = false;
		public void Action() {
			if (!position.HasValue) return;
			if (price > unit.ap) return;
			unit.state.PlaySinglton(nameAnimation);
			fly = true;
			unit.agent.destination = position.Value;
			unit.agent.enabled = false;
			unit.transform.LookAt(new Vector3(position.Value.x, unit.position.y, position.Value.z));
			StartCoroutine(Fly());
		}

		protected IEnumerator Fly() {
			float speed = Vector3.Distance(unit.position, position.Value) / (GetTime("endJump") - GetTime("startJump"));
			float speedHeightUp = (height - unit.position.y) / (GetTime("middleJump") - GetTime("startJump"));
			float speeedHeightDown = (height - unit.position.y) / (GetTime("endJump") - GetTime("middleJump"));
			int act = -2;
			unit.state.animEvent.AddFunc("startJump", () => { act=0; });
			unit.state.animEvent.AddFunc("middleJump", () => { act = 1; });
			unit.state.animEvent.AddFunc("endJump", () => { act = -1; });
			Vector3 direction = (position.Value - unit.position).normalized;
			yield return new WaitUntil(() => {
				Vector3 pos;
				switch (act) {
					case 0:
						pos = unit.position + direction * speed * Time.deltaTime;
						pos.y += speedHeightUp * Time.deltaTime;
						unit.position = pos;
						break;
					case 1:
						pos = unit.position + direction * speed * Time.deltaTime;
						pos.y -= speeedHeightDown * Time.deltaTime;
						unit.position = pos;
						break;
				}
				if(act == -1) {
					return true;
				} else {
					return false;
				}
			});
			fly = false;
			unit.agent.enabled = true;
		}

		private float GetTime(string name) => unit.state.animEvent.GetTime(name);

		public bool isActive() {
			return true;
		}

		public bool isLock() {
			return fly;
		}

		public void OnAwake(Unit unit) {
			this.unit = unit;
		}

		public void OnChoiceSelf() {
			aim = Instantiate(cont.aimMove).transform;
		}

		public void OnDestroy() {

		}

		public void OnEndStep() {
		}

		public void OnResetSelf() {
			Destroy(aim.gameObject);
		}

		public void OnStartStep() {

		}
		public void OnUpdate() {
			position = BattleSystem.GetPositionMouse();
			if (!position.HasValue) {
				aim.gameObject.SetActive(false);
				return;
			}
			aim.gameObject.SetActive(true);
			position = unit.position + (position.Value - unit.transform.position).normalized * distance;
			if (!BattleSystem.IsTerrain(position.Value)) {
				position = null;
				return;
			}
			aim.position = new Vector3(position.Value.x, cont.heightUI, position.Value.z);
		}

	}
}
