using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event System.Action<GameObject> ShowInfo;
    Status status;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
            if (Physics.Raycast(ray, out hit, float.MaxValue, mask)) {
                if (ShowInfo != null) ShowInfo(gameObject);
            }
        }
    }
}
