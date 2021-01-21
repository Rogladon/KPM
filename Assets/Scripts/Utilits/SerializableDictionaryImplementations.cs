using System;
 
using UnityEngine;



// ---------------
//  string => Animation
// ---------------
[Serializable]
public class StringAnimationsDictionary : SerializableDictionary<string, Battle.UnitCore.Animations> { }

// ---------------
//  State => int
// ---------------
[Serializable]
public class StateIntDictionary : SerializableDictionary<Battle.UnitCore.Buffs.BuffState.State, int> { }
