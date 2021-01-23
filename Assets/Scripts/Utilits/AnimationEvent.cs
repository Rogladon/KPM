using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle.UnitCore {
	

	public class AnimationEvent : MonoBehaviour {
		public class Event {
			public string nameEvent;
			public float position;
			public Func func;
			public delegate void Func();

			public Event() {

			}
			public Event(string name) {
				nameEvent = name;
			}

			public override bool Equals(object obj) {
				if(obj is Event) {
					return nameEvent == (obj as Event).nameEvent;
				}
				return false;
			}
			public override int GetHashCode() {
				return base.GetHashCode();
			}
		}
		public List<Event> events { get; private set; } = new List<Event>();
		public void Init(Animation anim, List<Animations> animations) {
			foreach(var i in animations) {
				foreach(var j in i.clips) {
					foreach(var e in j.events){
						Event ev = new Event();
						ev.nameEvent = e.stringParameter;
						ev.position = e.time;
						ev.func = () => { };
						events.Add(ev);
					}
				}
			}
		}

		public void FireEvent(string name) {
			Event ev = events.Find(p => p.nameEvent == name);
			if (ev != null) {
				ev.func();
			} else {
				Debug.Log($"Events {name} doesn`t fired, because event missing!");
			}
		}
		public void AddFunc(string name, Event.Func func) {
			Event ev = events.Find(p => p.nameEvent == name);
			if (ev != null) {
				ev.func = func;
			} else {
				Debug.Log($"Events {name} doesn`t added, because event missing!");
			}
		}
	}
}