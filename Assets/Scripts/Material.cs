using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    protected List<FailureReason> failureReasons;

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
        public List<string> fixes;

        public FailureReason(string newName, int newRate, float newDuration, RollModDelegate newModifier, List<string> newFixes) {
            name = newName;
            baseRate = newRate;
            duration = newDuration;
            ModifyRoll = newModifier;
            fixes = newFixes;
        }

        public bool RollFailure(ref List<Status.Effect> effects) {
            int rate = baseRate / ModifyRoll(effects);
            if (Random.Range(0, rate) == 0) {
                effects.Add(new Status.Effect(name, duration, Time.time, true, fixes));
                return true;
            }
            return false;
        }
    }
}
