using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event2Utils
{
    
}

[Serializable]
public class BoolEvent : UnityEvent2<bool> { }

[Serializable]
public class Vector2Event : UnityEvent2<Vector2> { }

[Serializable]
public class FloatEvent : UnityEvent2<float> { }

[Serializable]
public class ButtonStateEvent : UnityEvent2<ButtonState> { }
