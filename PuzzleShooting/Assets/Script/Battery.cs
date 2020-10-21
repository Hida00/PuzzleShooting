using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Battery : MonoBehaviour , IPuzzle
{
    GameObject panel;

    PanelController _panelController;
    PlayerController _playerController;

    string fileName;

    bool isSuccess = false;

    void Start()
    {
        fileName = SelectController.SelectName;

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        panel = GameObject.Find("Panel");
        _panelController = panel.GetComponent<PanelController>();
    }

    void Update()
    {
        
    }

    void Create_Image()
    {
        TextAsset csv = Resources.Load(@"CSV/StageData/" + fileName) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        
        while(reader.Peek() > -1)
        {

        }
    }
    public void Finish()
    {
        if (isSuccess) GameObject.Find("GameController").GetComponent<GameController>( ).IntervalSpawn(7 , _panelController.skillnum , 15f);
        else GameObject.Find("GameController").GetComponent<GameController>( ).IntervalSpawn(7 , _panelController.skillnum , 25f);

        _panelController.canskill[_panelController.skillnum] = false;
        _panelController.isSkill = true;

        _panelController.skillSpeed = 1;
        Time.timeScale = 1.0f;

        _panelController.Finish();
        panel.SetActive(false);
        _panelController.FinishTimeSet();
        Destroy(this.gameObject);
    }
}
