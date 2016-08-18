using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TitleController : MonoBehaviour {
    enum MenuButton
    {
        GAME = 0,
        LOAD,
        QUIT
    };

    [SerializeField]
    private Button[] m_menuButtons;

    private int m_currentMenuNum;
    private Action<int> m_selectAction;
    private Action<int> m_noSelectAction;
    private bool m_isUp;
    private bool m_isDown;
    private List<EventTrigger> m_eventTriggerList = new List<EventTrigger>();

    // Use this for initialization
    private void Start () {
        this.m_currentMenuNum = 0;

        //現在選択された時に使用する処理
        this.m_selectAction = (int menuIndex) => {
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = Color.red;
            colorBlock.highlightedColor = Color.red;
            colorBlock.pressedColor = Color.red;
            colorBlock.disabledColor = Color.white;
            colorBlock.colorMultiplier = 1.0f;
            this.m_menuButtons[menuIndex].colors = colorBlock;
        };

        //選択されていない時に使用する処理
        this.m_noSelectAction = (int menuIndex) =>
        {
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = Color.white;
            colorBlock.pressedColor = Color.red;
            colorBlock.disabledColor = Color.white;
            colorBlock.colorMultiplier = 1.0f;
            this.m_menuButtons[menuIndex].colors = colorBlock;
        };

        //初期設定
        GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);

        //ボタンが押された時の処理
        /*
        foreach (var button in this.m_menuButtons.Select((value, index) => new { value, index }))
        {
            EventTrigger trigger = button.value.GetComponent<EventTrigger>();
            //m_eventTrigger[button.index] = button.value.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();

            Debug.Log("trigger : "+ trigger + "  index:" + button.index);

            //マウスオーバーのIDを設定
            entry.eventID = EventTriggerType.PointerEnter;

            entry.callback.AddListener((data)=> {
                Debug.Log("test :"+button.index + "  "+button.value.ToString() + "  "+data.selectedObject.name);
                //this.m_currentMenuNum = button.index;
                //GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
            });

            trigger.triggers.Add(entry);

        }
        */
    }
	

	// Update is called once per frame
	private void Update () {
        //メニュー上に移動
        if ( ( Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.W) )  && this.m_isUp)
        {
            this.m_isUp = false;
            this.m_isDown = false;

            int count = this.m_menuButtons.Length - 1;
            this.m_currentMenuNum = (this.m_currentMenuNum + count) % this.m_menuButtons.Length;

            GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
        }

        //メニュー下に使用
        if ( ( Input.GetKeyDown("down") || Input.GetKeyDown(KeyCode.S) )  && this.m_isDown)
        {
            this.m_isUp = false;
            this.m_isDown = false;

            this.m_currentMenuNum++;
            this.m_currentMenuNum = System.Math.Abs(this.m_currentMenuNum % 3);

            GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
        }

        //決定ボタン
        if ( Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("select num "+this.m_currentMenuNum);
        }
        

        //メニュー移動を再度行えるよう対応
        this.m_isUp = true;
        this.m_isDown = true;
    }
    

    public void OnMouseGameButton()
    {
        Debug.Log("TEST001");
        this.m_currentMenuNum = 0;
        GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
    }

    public void OnClickGameButton()
    {
        Debug.Log("Game drag");
    }

    public void OnMouseLoadButton()
    {
        Debug.Log("TEST002");
        this.m_currentMenuNum = 1;
        GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
    }

    public void OnClickLoadButton()
    {
        Debug.Log("Load drag");
    }

    public void OnMouseQuitButton()
    {
        Debug.Log("TEST003");
        this.m_currentMenuNum = 2;
        GameUtility.AniSelectMenu<Button>(this.m_menuButtons, this.m_currentMenuNum, this.m_selectAction, this.m_noSelectAction);
    }
    
    public void OnClickQuitButton()
    {
        Debug.Log("Quit drag");
    }
}
