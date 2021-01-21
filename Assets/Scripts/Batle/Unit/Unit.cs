using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

namespace Battle.UnitCore {
	public struct BattleUnitState {
		public int strenght;
		public int defense;
		public int maxHp;
		public int maxAp;
		public int heelAp;
	}
	public class Unit : MonoBehaviour {
		//TOODOO
		[SerializeField]
		GameObject hover;
		//TOODOO
		public UnitInfo unitInfo;

		public List<IAction> actions { get; private set; }
		public List<IBuff> buffs { get; private set; } = new List<IBuff>();

		public int hp {
			get {
				return _hp;
			}
			private set {
				_hp = value <= unitState.maxHp ? value : unitState.maxHp;
				if(value <= 0) {
					_hp = 0;
					Death();
				}
			}
		}
		private int _hp;
		public int ap {
			get {
				return _ap;
			}
			private set {
				_ap = value <= unitState.maxAp ? value : unitState.maxAp;
			}
		}
		private int _ap;
		public BattleUnitState unitState;

		private bool isLock => currentAction != null ?currentAction.isLock():false;
		public bool isActive {
			get {
				return _isActive && !isDeath;
			}
			set {
				_isActive = value;
			}
		}
		private bool _isActive;
		public bool isDeath { get; private set; }

		public IAction currentAction { get; set; }
		public int team;

		public Vector3 position {
			get {
				return transform.position;
			}
			set {
				transform.position = new Vector3(value.x, transform.position.y, value.z);
			}
		}
		public float radius {
			get {
				return GetComponent<CapsuleCollider>().radius;
			}
		}
		public NavMeshAgent agent { get; private set; }
		public StateMachine state { get; private set; }
		//Test
		private void Start() {
			Init(team);
		}
		//Test

		public void Init(int team) {
			agent = GetComponent<NavMeshAgent>();
			state = GetComponent<StateMachine>();
			
			unitState = new BattleUnitState {
				maxHp = unitInfo.state.hp,
				maxAp = unitInfo.state.ap,
				heelAp = unitInfo.state.heelAp,
				strenght = unitInfo.state.strenght,
				defense = unitInfo.state.defense
			};
			hp = unitInfo.state.hp;
			ap = unitInfo.state.ap;
			actions = GetComponents<IAction>().ToList();
			actions.ForEach(p => p.OnAwake(this));
			buffs.ForEach(p => p.OnAwake(this));
			SetAction(0);
			this.team = team;
		}

		public void Update() {
			buffs.ForEach(p => p.OnUpdate());
			if (isLock) return;
			if (!isActive) return;
			if (currentAction != null) {
				currentAction.OnUpdate();
				if (Input.GetMouseButtonDown(0)) {
					currentAction.Action();
				}
			}
		}

		public void OnStartStep() {
			HeelAp();
			actions.ForEach(p => p.OnStartStep());
			buffs.ForEach(p => p.OnStartStep());
		}
		public void OnEndStep() {
			if(currentAction != null)
				currentAction.OnResetSelf();
			currentAction = null;
			actions.ForEach(p => p.OnEndStep());
			buffs.ForEach(p => p.OnEndStep());
		}

		public void Hit(int dmg) {
			hp -= dmg;
		}
		public void Action(int ap) {
			this.ap -= ap;
		}
		public void Death() {
			Debug.Log($"Died {name}");
			isDeath = true;
			//TOODOO
			agent.enabled = false;
			transform.Rotate(new Vector3(90, 0, 0));
		}
		public void HeelAp() {
			ap += unitState.heelAp;
		}

		public void SetAction(int index) {
			if(currentAction != null)
				currentAction.OnResetSelf();
			currentAction = actions[index];
			currentAction.OnChoiceSelf();
		}
		public void SetAction(IAction action) {
			currentAction = action;
			currentAction.OnChoiceSelf();
		}
		public void Activate() {
			isActive = true;
			currentAction = null;
		}
		public void Disactive() {
			isActive = false;
		}

		public void AddBuff(IBuff buff) {
			buffs.Add(buff);
			buff.OnAwake(this);
		}
		public void RemoveBuff(IBuff buff) {
			buffs.Remove(buff);
			buff.OnDestroy();
		}
		#region TOODOO
		//TOODOO
		float time;
		public void HoverOn() {
			hover.SetActive(true);
			time = 0.1f;
		}
		public void HoverOut() {
			hover.SetActive(false);
		}
		private void LateUpdate() {
			time -= Time.deltaTime;
			if(time <= 0) {
				HoverOut();
			}
		}
		//TOODOO
		#endregion

	}
}