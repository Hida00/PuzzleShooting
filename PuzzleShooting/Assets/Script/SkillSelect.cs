using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class SkillSelect : MonoBehaviour
{
    public Image Panel;
    public Image SkillImage;
    public Text SkillName;
    public TextMeshProUGUI Exit;

    public GameObject ScrollView;
    public GameObject Content;
    public GameObject ExplanationPanel;

    Image[] skillPanels;
    public Image[] Frames;

    public int skillNumber = 0;

    public int[] Numbers = new int[3] { 0 , 1 , 2 };

    void Start()
    {
        Numbers = SelectController.SetSkills;
        Array.Sort(Numbers);

        float Width = Screen.width;
        float prov = (float)Screen.height / 450;
        ScrollView.GetComponent<RectTransform>().anchoredPosition = new Vector2(Width * 0.25f , 0);
        ScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(Width * 0.48f , Screen.height * 0.96f);

        Content.GetComponent<VerticalLayoutGroup>().padding
            = new RectOffset((int)(Width * 0.024f) , (int)(Width * 0.024f) , (int)(20 * prov) , (int)(20 * prov));

        Exit.rectTransform.anchoredPosition *= new Vector2(prov , prov);
        
        Create_Image();
    }
    void Update()
    {
    }
    void Create_Image()
    {
        int i = 0,count = 0;
        float prov = (float)Screen.height / 450;
        TextAsset csv = Resources.Load(@"CSV/SkillData/SkillData") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');
        skillPanels = new Image[int.Parse(info[1])];

        while(st.Peek() > -1)
        {
            string[] values = st.ReadLine().Split(',');

            var panel = Instantiate(Panel , Content.transform);
            panel.rectTransform.sizeDelta *= new Vector2(prov / 2 , prov / 2);

            var obj = Instantiate(SkillImage , panel.transform);
            obj.rectTransform.anchoredPosition = new Vector2(ScrollView.GetComponent<RectTransform>().sizeDelta.x * -0.375f , 0f);
            obj.rectTransform.sizeDelta = new Vector2(panel.rectTransform.sizeDelta.y * 0.75f , panel.rectTransform.sizeDelta.y * 0.75f);
            obj.GetComponent<SkillPanels>().Num = i;
            Sprite sprite = Resources.Load<Sprite>(@"Image/sample/" + values[1]);
            obj.sprite = sprite;
            if(count < 3 && Numbers[count] == i)
            {
                obj.GetComponent<SkillPanels>().select = true;
                count++;
            }
            
            var obj2 = Instantiate(SkillName , panel.transform);
            obj2.rectTransform.anchoredPosition = new Vector2(0.0f , 0f);
            obj2.rectTransform.sizeDelta = new Vector2(ScrollView.GetComponent<RectTransform>().sizeDelta.x * 0.5f , panel.rectTransform.sizeDelta.y * 0.85f);
            obj2.text = values[1];

            skillPanels[i] = obj;
            i++;
        }
        for(i = 0;i < 3;i++)
        {
            Frames[i].rectTransform.anchoredPosition *= prov;
            Frames[i].rectTransform.sizeDelta *= prov;
        }
        i = 0;

        foreach(var index in Numbers)
        {
            var Obj = Instantiate(skillPanels[index] , Frames[i].transform);
            Obj.rectTransform.anchoredPosition = new Vector2(0 , 0);
            Obj.GetComponent<RectTransform>().sizeDelta = Frames[i].GetComponent<RectTransform>().sizeDelta * 0.75f;
            Frames[i].GetComponent<SkillFrame>().set_Image = Obj;
            Obj.GetComponent<SkillPanels>().Parent = skillPanels[index].GetComponent<SkillPanels>();
            Obj.GetComponent<SkillPanels>().frameNum = i + 1;
            Obj.sprite = skillPanels[index].sprite;
            i++;
        }
    }
    public void ExitAndSave()
    {
        SelectController.SetSkills = Numbers;
        SceneManager.LoadScene("Select");
    }
    public void ShowExplanation(string name)
    {
        float prov = (float)Screen.height / 450;
        var obj = Instantiate(ExplanationPanel , GameObject.Find("Canvas").transform);
        obj.name = name + "Explanation";
        TextAsset csv = Resources.Load(@"CSV/SkillData/" + name) as TextAsset;
        StringReader st = new StringReader(csv.text);
        obj.transform.GetComponentInChildren<Text>().text = st.ReadToEnd();
        obj.transform.GetComponentInChildren<Text>().fontSize = (int)((float)obj.transform.GetComponentInChildren<Text>().fontSize * prov);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-320f * prov , 0 , 0);
        obj.GetComponent<RectTransform>().sizeDelta *= prov;
    }
    public void DeleteExplanation(string name)
    {
        Destroy(GameObject.Find(name + "Explanation"));
    }
}
