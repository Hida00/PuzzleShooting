using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SkillSelect : MonoBehaviour
{
    public Image Panel;
    public Image SkillImage;
    public Text SkillName;

    public GameObject ScrollView;
    public GameObject Content;

    Image[] skillPanels;
    public Image[] Frames;

    public int skillNumber = 0;

    public int[] Numbers = new int[3] { 0 , 1 , 2 };

    void Start()
    {
        Numbers = SelectController.SetSkills;

        float Width = Screen.width;
        float prov = (float)Screen.height / 450;
        ScrollView.GetComponent<RectTransform>().anchoredPosition = new Vector2(Width * 0.25f , 0);
        ScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(Width * 0.48f , Screen.height * 0.96f);

        Content.GetComponent<VerticalLayoutGroup>().padding
            = new RectOffset((int)(Width * 0.024f) , (int)(Width * 0.024f) , (int)(20 * prov) , (int)(20 * prov));
        
        Create_Image();
    }

    void Update()
    {

    }
    void Create_Image()
    {
        int i = 0;
        float prov = (float)Screen.height / 450;
        TextAsset csv = Resources.Load(@"CSV/SkillData") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');
        skillPanels = new Image[int.Parse(info[1])];

        while(st.Peek() > -1)
        {
            string[] values = st.ReadLine().Split(',');

            var panel = Instantiate(Panel , new Vector3(0 , 0 , 0) , Quaternion.identity);
            panel.transform.SetParent(Content.transform , false);
            panel.rectTransform.sizeDelta *= new Vector2(prov / 2 , prov / 2);

            var obj = Instantiate(SkillImage , new Vector3(0 , 0 , 0) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(ScrollView.GetComponent<RectTransform>().sizeDelta.x * -0.375f , 0f);
            obj.rectTransform.sizeDelta = new Vector2(panel.rectTransform.sizeDelta.y * 0.85f , panel.rectTransform.sizeDelta.y * 0.85f);
            obj.GetComponent<SkillPanels>().Num = i + 1;
            Sprite sprite = Resources.Load<Sprite>(@"Image/sample/" + values[1]);
            obj.sprite = sprite;

            var obj2 = Instantiate(SkillName , new Vector3(0 , 0 , 0) , Quaternion.identity);
            obj2.transform.SetParent(panel.transform , false);
            obj2.rectTransform.anchoredPosition = new Vector2(0.0f , 0f);
            obj2.rectTransform.sizeDelta = new Vector2(ScrollView.GetComponent<RectTransform>().sizeDelta.x * 0.5f , panel.rectTransform.sizeDelta.y * 0.85f);
            obj2.text = values[1];

            skillPanels[i] = obj;
            i++;
        }
        i = 0;

        foreach(var index in Numbers)
        {
            var Obj = Instantiate(skillPanels[index]);
            Obj.transform.SetParent(Frames[i].transform , false);
            Obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 , 0);
            Obj.GetComponent<RectTransform>().sizeDelta = Frames[i].GetComponent<RectTransform>().sizeDelta * 0.75f;
            i++;
        }

    }
    public void ExitAndSave()
    {
        SelectController.SetSkills = Numbers;
        SceneManager.LoadScene("Select");
    }
}
