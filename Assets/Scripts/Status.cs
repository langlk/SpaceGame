using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public List<Metal> Materials;
    public float failureRollInterval = 2;
    float lastRolled;
    List<Effect> effects = new List<Effect>();
    // Start is called before the first frame update
    void Start()
    {
        Ship.ShipCrashed += OnShipCrashed;
    }

    void FixedUpdate() {
        ClearExpiredEffects();
        if (Time.time - lastRolled > failureRollInterval) {
            lastRolled = Time.time;
            RollFailures();
        }
    }

    void OnDestroy() {
        Ship.ShipCrashed -= OnShipCrashed;
    }

    void OnShipCrashed() {
        effects.Add(new Effect("crashed", 4, Time.time));
    }

    void ClearExpiredEffects() {
        effects.RemoveAll(effect => effect.maxDuration != -1 && Time.time > effect.timeStarted + effect.maxDuration);
    }

    void RollFailures() {
        foreach (Metal material in Materials) {
            material.RollFailures(ref effects);
        }
    }

    public void PrintEffects() {
        foreach (Effect effect in effects) {
            print((effect.isFailure ? "Failure: " : "") + gameObject.name + " is " + effect.name);
        }
    }

    public struct Effect {
        public string name;
        public float maxDuration;
        public float timeStarted;
        public bool isFailure;

        public Effect(string newName, float newDuration, float time, bool failure = false) {
            name = newName;
            maxDuration = newDuration;
            timeStarted = time;
            isFailure = failure;
        }
    }
}
