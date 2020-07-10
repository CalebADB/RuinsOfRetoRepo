using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace masterFeature
{
    [Serializable] public class Dictionary_SpeedXfloat : SerializableDictionary<LocalPhysicsEngine.SpeedXs, float> { }

    [Serializable] public class Dictionary_SpeedYfloat : SerializableDictionary<LocalPhysicsEngine.SpeedYs, float> { }
}