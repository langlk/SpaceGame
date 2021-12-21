using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    bool shipCrashed;
    public static event System.Action ShipCrashed;
    void Start()
    {
        
    }

    void Update()
    {
        if (!shipCrashed) CrashShip();
    }

    void CrashShip() {
        shipCrashed = true;
        if (ShipCrashed != null) ShipCrashed();
    }
}
