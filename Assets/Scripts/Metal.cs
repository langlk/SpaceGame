using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    List<FailureReason> failureReasons = new List<FailureReason> {
        new FailureReason("fractured", 100, -1, effects =>
            effects.Exists(effect => effect.name == "crashed") ? 100 : 1),
        new FailureReason("rusted", 100, -1, effects =>
            effects.Exists(effect => effect.name == "damp") ? 10 : 1),
        new FailureReason("warped", 100, -1, effects =>
            effects.Exists(effect => effect.name == "crashed") ? 100 : 1),
        new FailureReason("corroded", 100, -1, effects =>
            effects.Exists(effect => effect.name == "acid") ? 20 : 1)
    };

    public delegate int RollModDelegate(List<Status.Effect> effects);

    public void RollFailures(ref List<Status.Effect> effects) {
        foreach (FailureReason reason in failureReasons) {
            reason.RollFailure(ref effects);
        }
    }

    public struct FailureReason {
        public string name;
        public int baseRate;
        public float duration;
        public RollModDelegate ModifyRoll;

        public FailureReason(string newName, int newRate, float newDuration, RollModDelegate newModifier) {
            this.name = newName;
            this.baseRate = newRate;
            this.duration = newDuration;
            this.ModifyRoll = newModifier;
        }

        public bool RollFailure(ref List<Status.Effect> effects) {
            int rate = baseRate / ModifyRoll(effects);
            if (Random.Range(0, rate) == 0) {
                effects.Add(new Status.Effect(name, duration, Time.time));
                return true;
            }
            return false;
        }
    }
}

