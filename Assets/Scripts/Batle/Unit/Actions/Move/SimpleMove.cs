﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;
using UnityEngine.AI;
using System.Linq;

namespace Battle.UnitCore {
	public class SimpleMove : Action, IAction {
		Vector3? position = null;
		LineRenderer line;
		Transform aim;
		[SerializeField]
		Color defaultColor;
		[SerializeField]
		Color notApColor;
		[SerializeField]
		float multiply = 3f;
		float lenght;
		BattleStaticContainer cont => BattleStaticContainer.instance;
		public void Action() {
			if (!position.HasValue) return;

			int ap = Mathf.RoundToInt(lenght);
			if (unit.ap - ap < 0) return;
			unit.Action(ap);
			Vector3 pos = position.Value;
			pos.y = unit.position.y;
			unit.agent.destination = pos;
			ResetLine();
		}

		public bool isActive() {
			return true;
		}

		public bool isLock() {
			return unit.agent.desiredVelocity != Vector3.zero;
		}

		public void OnAwake(Unit unit) {
			this.unit = unit;
		}

		public void OnChoiceSelf() {
			line = Instantiate(cont.lineMove);
			aim = Instantiate(cont.aimMove).transform;
			ResetLine();
		}

		public void OnDestroy() {
			
		}

		public void OnEndStep() {
			OnResetSelf();
		}

		public void OnResetSelf() {
			Destroy(line.gameObject);
			Destroy(aim.gameObject);
		}

		public void OnStartStep() {
			
		}

		public void OnUpdate() {
			position = BattleSystem.ChoicePosition();
			if (!position.HasValue) {
				ResetLine();
				return;
			}
			NavMeshPath path = new NavMeshPath();
			if (unit.agent.CalculatePath(position.Value, path)) {
				SetLine(path.corners);
			}
		}
		public string test;
		void SetLine(Vector3[] pos) {
			pos = pos.Select(p => new Vector3(p.x, cont.heightUI, p.z)).ToArray();
			lenght = 0;
			pos.Aggregate((x, y) => {
				lenght += Vector3.Distance(x, y);
				return x;
			});
			if(Mathf.Round(lenght) > unit.ap) {
				//line.material.color = notApColor;
				//line.material.GetTexturePropertyNames().All(p => { Debug.Log(p); return true; });
				line.material.SetVector("_EmissionColor", notApColor * multiply);
				//line.material.SetColor(test, notApColor);
			} else {
				//line.material.color = defaultColor;
				line.material.SetVector("_EmissionColor", defaultColor * multiply);
			}
			line.positionCount = pos.Length;
			line.SetPositions(pos.Select(p => new Vector3(p.x, cont.heightUI, p.z)).ToArray());
			aim.gameObject.SetActive(true);
			Vector3 posAim = pos[pos.Length - 1];
			posAim.y = cont.heightUI;
			aim.position = posAim;
		}
		void ResetLine() {
			line.positionCount = 0;
			aim.gameObject.SetActive(false);
		}
	}
}