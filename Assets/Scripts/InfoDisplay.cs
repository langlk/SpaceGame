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
    public GameObject player;
    public GameObject effectInfo;
    public Text effectName;
    public GameObject effectFixes;
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
        statusEffects.SetActive(true);
        effectInfo.SetActive(false);
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
            Text t = btn.GetComponentInChildren<Text>();
            t.text = e.name;
            t.color = e.AvailableFixes(player.GetComponent<Inventory>().items).Count > 0 ? Color.black : Color.red;
            btn.GetComponent<Button>().onClick.AddListener(() => {
                RenderEffectInfo(e, status);
            });
        }
        for (int i = effects.Count; i < btns.Length; i++) {
            // if more btns than effects, destroy remaining btns
            Destroy(btns[i].gameObject);
        }
    }

    void RemoveEffect(Status.Effect e, Status status) {
        // Remove effect
        status.RemoveEffect(e);
        // Re-render list of effects
        RenderStatusEffects(status);
    }

    void RenderEffectInfo(Status.Effect e, Status status) {
        statusEffects.SetActive(false);
        effectInfo.SetActive(true);
        effectName.text = e.name;
        List<string> fixes = e.AvailableFixes(player.GetComponent<Inventory>().items);
        Button[] btns = effectFixes.GetComponentsInChildren<Button>();
        for (int i = 0; i < fixes.Count; i++) {
            GameObject btn;
            if (i < btns.Length) {
                // reuse btn if we have one at this index
                btn = btns[i].gameObject;
                btns[i].onClick.RemoveAllListeners();
            } else {
                // create new if we don't
                btn = Instantiate(statusButtonPrefab);
                btn.transform.SetParent(effectFixes.transform, false);
                btn.transform.Translate(Vector3.down * i * btn.GetComponent<RectTransform>().rect.height);
            }
            string fix = fixes[i];
            Text t = btn.GetComponentInChildren<Text>();
            t.text = fix;
            
            btn.GetComponent<Button>().onClick.AddListener(() => {
                RemoveEffect(e, status);
            });
        }
        for (int i = fixes.Count; i < btns.Length; i++) {
            // if more btns than effects, destroy remaining btns
            Destroy(btns[i].gameObject);
        }
        
    }
}
