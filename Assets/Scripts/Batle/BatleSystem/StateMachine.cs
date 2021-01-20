using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;
using System;
using System.Linq;

namespace Battle.UnitCore {
	[Serializable]
	public class Animations {
		public List<AnimationClip> clips;
		public bool isSinglton => clips.Count == 1;

		public AnimationClip this[int index] {
			get {
				return clips[index];
			}
		}
	}
	public class StateMachine : MonoBehaviour {
		public static string IDLE = "idle";
		public static string ATTACK = "attck";

		public delegate bool Condition(Unit unit);
		[Header("Animations")]
		[SerializeField]
		StringAnimationsDictionary _animations = StringAnimationsDictionary.New<StringAnimationsDictionary>();

		Dictionary<string, Animations> animations {
			get {
				return _animations.dictionary;
			}
		}

		private Animation anim;

		private string state;

		private void Start() {
			anim = GetComponentInChildren<Animation>();
			foreach(var p in animations) {
				for(int i = 0; i< p.Value.clips.Count; i++) {
					anim.AddClip(p.Value[i], $"{p.Key}-{i}");
				}
			}
			ResetState();
		}
		private void SetState(string _state) {
			state = _state;
			Debug.Log($"Animtion: {state.ToString()} : {animations[state].clips[0].name}");
			anim.Play(animations[state].clips[0].name);
		}
		private void ResetState() {
			state = IDLE;
			anim.Play(animations[state].clips[0].name);
		}
		public void PlaySinglton(string _state) {
			SetState(_state);
			StartCoroutine(_PlaySinglton());
		}
		public IEnumerable WaitPlaySinglton(string _state) {
			SetState(_state);
			yield return _PlaySinglton();
		}
		public void PlayUntil(string _state, Func<bool> predicate) {
			SetState(_state);
			FinishOnCondition(predicate);
		}
		public IEnumerator WaitPlayUntil(string _state, Func<bool> predicate) {
			SetState(_state);
			yield return _FinishOnCondition(predicate);
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
