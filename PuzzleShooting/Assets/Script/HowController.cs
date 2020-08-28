using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class HowController : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject ExplanationArea;

    public RawImage image;

    public Text Exit;
    public Text Explanation;
    public Image GoT;
    public Image BackT;

    public VideoClip[] clips;

    string[] ExTexts;

    int textCount = 0;

    void Start()
    {
        SetExplanationText();

        float prov = 1f + ((float)Screen.height / 450f - 1) / 2f;
        PlayerArea.GetComponent<RectTransform>().anchoredPosition *= prov;
        PlayerArea.GetComponent<RectTransform>().sizeDelta *= prov;

        ExplanationArea.GetComponent<RectTransform>().anchoredPosition *= prov;
        ExplanationArea.GetComponent<RectTransform>().sizeDelta *= prov;

        image.rectTransform.anchoredPosition *= prov;
        image.rectTransform.sizeDelta *= prov;
        image.GetComponent<VideoPlayer>().Play();

        Exit.rectTransform.anchoredPosition *= prov;
        Exit.rectTransform.sizeDelta *= prov;
        Exit.fontSize = (int)(Exit.fontSize * prov);

        Explanation.rectTransform.anchoredPosition *= prov;
        Explanation.rectTransform.sizeDelta *= prov;
        Explanation.fontSize = (int)(Explanation.fontSize * prov);
        Explanation.text = ExTexts[textCount];

        GoT.rectTransform.anchoredPosition *= prov;
        GoT.rectTransform.sizeDelta *= prov;

        BackT.rectTransform.anchoredPosition *= prov;
        BackT.rectTransform.sizeDelta *= prov;
    }

    void Update() { }

    void SetExplanationText()
    {
        int i = 0;
        TextAsset csv = Resources.Load(@"CSV/SkillData/Explanation") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');

        ExTexts = new string[int.Parse(info[1])];

        while(st.Peek() > -1)
        {
            ExTexts[i] = st.ReadLine();
            i++;
        }
    }
    public void Go()
    {
        textCount++;
        if(textCount > 1) textCount = 1;
        Explanation.text = ExTexts[textCount];
        if(textCount == 1)
        {
            Explanation.text += "\n";
            Explanation.text += PanelController.buf_keys[0].ToString() + "\n";
            Explanation.text += PanelController.buf_keys[1].ToString() + "\n";
            Explanation.text += PanelController.buf_keys[2].ToString() + "\n";
        }
        image.GetComponent<VideoPlayer>().Stop();
        image.GetComponent<VideoPlayer>().clip = clips[textCount];
        image.GetComponent<VideoPlayer>().Play();
    }
    public void Back()
    {
        textCount--;
        if(textCount < 0) textCount = 0;
        Explanation.text = ExTexts[textCount];
        image.GetComponent<VideoPlayer>().Stop();
        image.GetComponent<VideoPlayer>().clip = clips[textCount];
        image.GetComponent<VideoPlayer>().Play();
    }

    public void BackSelect()
    {
        SceneManager.LoadScene("Select");
    }
}
