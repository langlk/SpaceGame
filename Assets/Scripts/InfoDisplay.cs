using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public GameObject infoPane;
    public Text systemName;
    // Start is called before the first frame update
    void Start()
    {
        InputHandler.ShowInfo += OnShowInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        InputHandler.ShowInfo -= OnShowInfo;
    }

    void OnShowInfo(GameObject obj) {
        infoPane.SetActive(true);
        systemName.text = obj.name;
    }
}
