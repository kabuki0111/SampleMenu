using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;


class GameUtility : MonoBehaviour
{
    /// <summary>
    /// メニューが選択された時の処理(メニューのアニメーションで使用)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="m_menuButtons"></param>
    /// <param name="selectMenuNum"></param>
    /// <param name="selectAction"></param>
    /// <param name="noSelectAction"></param>
    static public void AniSelectMenu<T>(T[] m_menuButtons, int selectMenuNum, Action<int> selectAction = null, Action<int> noSelectAction = null)
    {
        foreach (var button in m_menuButtons.Select((value, index) => new { value, index }))
        {
            if (button.index == selectMenuNum)
            {
                if(selectAction != null)
                {
                    selectAction(button.index);
                }
            }
            else
            {
                if(selectAction != null)
                {
                    noSelectAction(button.index);
                }
            }
        }
    }
}