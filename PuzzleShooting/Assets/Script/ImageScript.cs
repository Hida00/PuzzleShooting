using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    //識別用の番号
    public int Num;

    //上・右・下・左の繋がる所は１、繋がらない所は０
    int[] sides = new int[4];

    //PanelControllerオブジェクト、終了時に使用
    PanelController _panelController;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
    }
    
    void Update()
    {
        //オブジェクトを消してスキルの終了
        if(!_panelController.isSkill)
        {
            Destroy(this.gameObject);
        }
        //Debug用。Escapeキーを押すとスキルの終了
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _panelController.isSkill = false;
        }
    }
    //Imageをクリックした時の処理
    public void Click()
    {
        //startとendは回転しないようにする
        if (Num != 0 && Num != 5)
        {
            //クリック毎に時計回りに90°回転
            Quaternion q = Quaternion.Euler(0 , 0 , -90f);
            this.transform.rotation *= q;
        }
    }
    public void Light()
    {
        //start以外光らせる
        if (Num != 0)
        {
            string s = this.name.Split('(')[0];
            var sprite = Resources.Load<Sprite>(@"Image/Recovery/" + s + "_light");
            this.GetComponent<Image>().sprite = sprite;
        }
    }
}
