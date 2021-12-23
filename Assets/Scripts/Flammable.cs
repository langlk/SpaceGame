using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : Material
{
    void Start() {
        failureReasons = new List<FailureReason> {
            new FailureReason("on fire", 100, -1, effects =>
                effects.Exists(effect => effect.name == "crashed") ? 100 : 1)
        };
    }
}
