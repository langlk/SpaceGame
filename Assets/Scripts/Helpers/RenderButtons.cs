using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderButtons : MonoBehaviour
{
    public delegate UnityEngine.Events.UnityAction MakeOnClickDelegate(int index);
    public static void RenderList(GameObject btnPrefab, List<string> btnNames, GameObject btnContainer, MakeOnClickDelegate makeOnClick) {
        Button[] btns = btnContainer.GetComponentsInChildren<Button>();
        for (int i = 0; i < btnNames.Count; i++) {
            GameObject btn;
            if (i < btns.Length) {
                // reuse btn if we have one at this index
                btn = btns[i].gameObject;
                btns[i].onClick.RemoveAllListeners();
            } else {
                // create new if we don't
                btn = Instantiate(btnPrefab);
                btn.transform.SetParent(btnContainer.transform, false);
                btn.transform.Translate(Vector3.down * i * btn.GetComponent<RectTransform>().rect.height);
            }
            string name = btnNames[i];
            Text t = btn.GetComponentInChildren<Text>();
            t.text = name;
            
            btn.GetComponent<Button>().onClick.AddListener(makeOnClick(i));
        }
        for (int i = btnNames.Count; i < btns.Length; i++) {
            // if more btns than effects, destroy remaining btns
            Destroy(btns[i].gameObject);
        }
    }
}
