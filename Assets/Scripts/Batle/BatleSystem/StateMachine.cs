using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;
using System;
using System.Linq;

namespace Battle.UnitCore {
	[Serializable]
	public struct Animations {
		public List<AnimationClip> clips;
		public bool isSinglton => clips.Count == 1;
	}
	public class StateMachine : MonoBehaviour {
		public delegate bool Condition(Unit unit);
		[Header("Animations")]
		[SerializeField]
		List<Animations> animations = new List<Animations>();

		public enum State {
			idle,
			skill1,
			skill2,
			skill3,
			skill4,
			skill5,
			skill6
		}
		public State state;

		private Animation anim;

		private void Start() {
			anim = GetComponentInChildren<Animation>();
			animations.ForEach(p => anim.AddClip(p.clips[0], p.clips[0].name));
			ResetState();
		}

		private void SetState(State _state) {
			state = _state;
			Debug.Log($"Animtion: {state.ToString()} : {(int)state} : {animations[(int)state].clips[0].name}");
			anim.Play(animations[(int)state].clips[0].name);
		}
		private void ResetState() {
			state = State.idle;
			anim.Play(animations[0].clips[0].name);
		}
		public void PlaySinglton(State _state) {
			SetState(_state);
			StartCoroutine(_PlaySinglton());
		}
		public void PlayUntil(State _state, Func<bool> predicate) {
			SetState(_state);
			FinishOnCondition(predicate);
		}
		public void FinishOnCondition(Func<bool> predicate) {
			StartCoroutine(_FinishOnCondition(predicate));
		}

		private IEnumerator _PlaySinglton() {
			yield return new WaitUntil(() => !anim.isPlaying);
			ResetState();
		}
		private IEnumerator _FinishOnCondition(Func<bool> predicate) {
			yield return new WaitUntil(predicate);
			Debug.Log("End");
			ResetState();
		}

		//TOODOO
		/*
		 * Кароч тут надо бы дописать систему с составными анимациями,
		 * Сначала первая например, через паузу вторая, или при совершении действия слудющая
		 * А еще можно сделать, тип если пережается то же состояние с пометкой продолжить,
		 * то тогда продолжается составная анимация, иначе новая,
		 * И еще если начинается составная анимация с флагом непрерываемя, то состояние не меняется,
		 * пока не отправится состояние с флагом специальным.
		 */
	}
}
