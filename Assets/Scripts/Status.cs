using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public static event System.Action OnStatusChange;
    bool shipCrashed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shipCrashed) CrashShip();
    }

    void CrashShip() {
        print("Ship crashing");
        shipCrashed = true;
        if (OnStatusChange != null) {
            // void crashUpdate(ref Dictionary<string, int> rates) {
            //     rates["fractured"] = 1;
            //     rates["warped"] = 1;
            // }
            OnStatusChange();
            // yield return new WaitForSeconds(1);
            // void postCrashUpdate(ref Dictionary<string, int> rates) {
            //     rates["fractured"] = 100;
            //     rates["warped"] = 100;
            // }
            // OnStatusChange(postCrashUpdate);
        }
    }
}
