using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UnitCore.Buffs {
	public class BuffState : Buff, IBuff {
		public enum State {
			strenght,
			defense,
			hp,
			ap,
			heelAp
		}
		[SerializeField]
		StateIntDictionary _dictionary = StateIntDictionary.New<StateIntDictionary>();

		Dictionary<State, int> dictionary {
			get {
				return _dictionary.dictionary;
			}
		}
		public void OnActive() {
			
		}

		public void OnAwake(Unit unit) {
			this.unit = unit;
			foreach(var i in dictionary) {
				switch (i.Key) {
					case State.strenght: 
						unit.unitState.strenght += i.Value;
						break;
					case State.defense:
						unit.unitState.defense+= i.Value;
						break;
					case State.hp:
						unit.unitState.maxHp+= i.Value;
						break;
					case State.ap:
						unit.unitState.maxAp+= i.Value;
						break;
					case State.heelAp:
						unit.unitState.heelAp+= i.Value;
						break;
				}
			}
		}

		public void OnDestroy() {
			foreach (var i in dictionary) {
				switch (i.Key) {
					case State.strenght:
						unit.unitState.strenght -= i.Value;
						break;
					case State.defense:
						unit.unitState.defense -= i.Value;
						break;
					case State.hp:
						unit.unitState.maxHp -= i.Value;
						break;
					case State.ap:
						unit.unitState.maxAp -= i.Value;
						break;
					case State.heelAp:
						unit.unitState.heelAp -= i.Value;
						break;
				}
			}
		}

		public void OnEndStep() {
			
		}

		public void OnStartStep() {
			
		}

		public void OnUpdate() {
			
		}
	}
}