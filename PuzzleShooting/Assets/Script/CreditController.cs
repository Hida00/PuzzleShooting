using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    public Text Title;
    public Text _Exit;
    public Text Material;
    public Text[] texts;

    public GameObject Panel;

    public AudioSource BGM;

    void Start()
    {
        float prov = 1f + ((Screen.height / 450f - 1f) / 3f);
        Title.fontSize = (int)(Title.fontSize * prov);
        Title.rectTransform.anchoredPosition *= prov;
        Title.rectTransform.sizeDelta *= prov;
        _Exit.fontSize = (int)(_Exit.fontSize * prov);
        _Exit.rectTransform.anchoredPosition *= prov;
        _Exit.rectTransform.sizeDelta *= prov;
        Material.fontSize = (int)(Material.fontSize * prov);
        Material.rectTransform.anchoredPosition *= prov;
        Material.rectTransform.sizeDelta *= prov;
        Panel.GetComponent<RectTransform>().anchoredPosition *= prov;
        Panel.GetComponent<RectTransform>().sizeDelta *= prov;

        foreach(var x in texts)
        {
            x.fontSize = (int)(x.fontSize * prov);
            x.rectTransform.anchoredPosition *= prov;
            x.rectTransform.sizeDelta *= prov;

            var child = x.transform.GetChild(0).GetComponent<Text>();
            child.fontSize = (int)(child.fontSize * prov);
            child.rectTransform.anchoredPosition *= prov;
            child.rectTransform.sizeDelta *= prov;
        }

        BGM.volume = SelectController.volume;
        BGM.Play();
    }

    void Update() { }

    public void Exit()
    {
        SceneManager.LoadScene("Setting");
    }
}
