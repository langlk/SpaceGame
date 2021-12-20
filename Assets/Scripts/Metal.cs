using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    Dictionary<string, int> rates = new Dictionary<string, int>{
        { "rusted", 100 },
        { "corroded", 100 },
        { "fractured", 20 },
        { "warped", 100 }
    };
    float waitInterval = .5f;
    float lastCalculated = 0;
    
    public delegate void UpdateRates(ref Dictionary<string, int> rates);
    // Start is called before the first frame update
    void Start()
    {
        Status.OnStatusChange += StatusChanged;
        CalculateFailures();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCalculated > waitInterval) {
            CalculateFailures();
            lastCalculated = Time.time;
        }
    }

    void OnDestroy() {
        Status.OnStatusChange -= StatusChanged; 
    }

    void CalculateFailures() {
        foreach (KeyValuePair<string, int> kvp in rates) {
            int d100 = Random.Range(1, kvp.Value + 1);
            if (d100 == 1) {
                print("Failure: " + transform.gameObject.name + " " + kvp.Key);
            }
        }
    }

    void StatusChanged() {
        print("Status change");
        // updateRates(ref rates);
    }
}

