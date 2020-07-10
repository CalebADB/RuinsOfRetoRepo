using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace masterFeature
{
    [CustomPropertyDrawer(typeof(Dictionary_SpeedXfloat))]
    public class Dictionary_SpeedXfloat_Drawer : SerializableDictionary_Drawer<LocalPhysicsEngine.SpeedXs, float> { }

    [CustomPropertyDrawer(typeof(Dictionary_SpeedYfloat))]
    public class Dictionary_SpeedYfloat_Drawer : SerializableDictionary_Drawer<LocalPhysicsEngine.SpeedYs, float> { }
}
