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



#endif
