using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Material
{
    void Start() {
        failureReasons = new List<FailureReason> {
            new FailureReason(
                "fractured",
                100,
                -1,
                effects => effects.Exists(effect => effect.name == "crashed") ? 100 : 1,
                new List<string> { "welding torch" }
            ),
            new FailureReason(
                "rusted",
                100,
                -1,
                effects => effects.Exists(effect => effect.name == "damp") ? 10 : 1,
                new List<string> { "welding torch" }
            ),
            new FailureReason(
                "warped",
                100,
                -1,
                effects => effects.Exists(effect => effect.name == "crashed") ? 100 : 1,
                new List<string> { "hammer" }
            ),
            new FailureReason(
                "corroded",
                100,
                -1, effects =>
                effects.Exists(effect => effect.name == "acid") ? 20 : 1,
                new List<string> { "welding torch" }
            )
        };
    }
}
