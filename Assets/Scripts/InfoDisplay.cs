using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public GameObject infoPane;
    public Text systemName;
    public GameObject statusEffects;
    public GameObject statusButtonPrefab;
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
        RenderStatusEffects(obj.GetComponent<Status>());
    }

    void RenderStatusEffects(Status status) {
        List<Status.Effect> effects = status.GetEffects();
        Button[] btns = statusEffects.GetComponentsInChildren<Button>();
        for (int i = 0; i < effects.Count; i++) {
            GameObject btn;
            if (i < btns.Length) {
                // reuse btn if we have one at this index
                btn = btns[i].gameObject;
                btns[i].onClick.RemoveAllListeners();
            } else {
                // create new if we don't
                btn = Instantiate(statusButtonPrefab);
                btn.transform.SetParent(statusEffects.transform, false);
                btn.transform.Translate(Vector3.down * i * btn.GetComponent<RectTransform>().rect.height);
            }
            Status.Effect e = effects[i];
            btn.GetComponentInChildren<Text>().text = e.name;
            btn.GetComponent<Button>().onClick.AddListener(() => {
                // Remove effect
                status.RemoveEffect(e);
                // Re-render list of effects
                RenderStatusEffects(status);
            });
        }
        for (int i = effects.Count; i < btns.Length; i++) {
            // if more btns than effects, destroy remaining btns
            Destroy(btns[i].gameObject);
        }
    }
}
