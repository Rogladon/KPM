using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
	public delegate void Func();
	public Dictionary<string, Func> functions { get; private set; } = new Dictionary<string, Func>();
    public void Event(string name) {
		if (functions.ContainsKey(name)) {
			functions[name]();
		}
	}
	public void AddFunc(string name, Func func) {
		functions.Add(name, func);
	}
}
