using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        RenderButtons.RenderList(
            statusButtonPrefab,
            effects.Select(e => e.name).ToList(),
            statusEffects,
            index => () => RenderEffectInfo(effects[index], status)
        );
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
        RenderButtons.RenderList(
            statusButtonPrefab,
            fixes,
            effectFixes,
            index => () => RemoveEffect(e, status)
        );
    }
}
