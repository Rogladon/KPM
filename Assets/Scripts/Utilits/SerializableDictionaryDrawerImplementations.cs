using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;

#if UNITY_EDITOR

// ---------------
//  string => Animations
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StringAnimationDictionary))]
public class StringAnimationDictionaryDrawer : SerializableDictionaryDrawer<string, Animation> {
	protected override SerializableKeyValueTemplate<string, Animation> GetTemplate() {
		return GetGenericTemplate<StringAnimationDictionaryTemplate>();
	}
}
internal class StringAnimationDictionaryTemplate : SerializableKeyValueTemplate<string, Animation> { }



#endif
