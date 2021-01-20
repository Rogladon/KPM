using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;

#if UNITY_EDITOR

// ---------------
//  string => Animationss
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StringAnimationsDictionary))]
public class StringAnimationsDictionaryDrawer : SerializableDictionaryDrawer<string, Battle.UnitCore.Animations> {
	protected override SerializableKeyValueTemplate<string, Battle.UnitCore.Animations> GetTemplate() {
		return GetGenericTemplate<StringAnimationsDictionaryTemplate>();
	}
}
internal class StringAnimationsDictionaryTemplate : SerializableKeyValueTemplate<string, Battle.UnitCore.Animations> { }

// ---------------
//  State => int
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StateIntDictionary))]
public class StateIntDictionaryDrawer : SerializableDictionaryDrawer<Battle.UnitCore.Buffs.BuffState.State, int> {
	protected override SerializableKeyValueTemplate<Battle.UnitCore.Buffs.BuffState.State, int> GetTemplate() {
		return GetGenericTemplate<StateIntDictionaryTemplate>();
	}
}
internal class StateIntDictionaryTemplate : SerializableKeyValueTemplate<Battle.UnitCore.Buffs.BuffState.State, int> { }



#endif
