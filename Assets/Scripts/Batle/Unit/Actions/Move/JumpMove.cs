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
		//TOODOO расчет скорости
		[SerializeField]
		float speedheight = 0.1f;

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
			StartCoroutine(Fly());
		}

		protected IEnumerator Fly() {
			int countStep = (int)(unit.state.GetTime(nameAnimation) / Time.deltaTime);
			if (countStep == 0) {
				unit.position = position.Value;
				fly = false;
				yield return null;
			}
			float speed = Vector3.Distance(unit.position, position.Value) / countStep;
			int step = 0;
			Vector3 direction = (position.Value - unit.position).normalized;
			yield return new WaitUntil(() => {
				unit.position += direction * speed;
				Vector3 p = unit.position;
				p.x = 0; p.z = 0;
				Vector3 h = step < countStep / 2 ? new Vector3(0, height, 0):Vector3.zero;
				Vector3 pos = Vector3.Lerp(p, h, speedheight);
				Debug.Log($"{pos}  {h} {p}");
				pos.x =  unit.position.x; pos.z = unit.position.z;
				unit.position = pos;
				step++;
				if (step > countStep) {
					return true;
				}
				return false;
			});
			fly = false;
			unit.agent.enabled = true;
		}

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
