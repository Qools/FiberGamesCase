using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : UIPanel
{
    [SerializeField] private List<Button> _buttonList;

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = DataManager.Instance.GetLevel();

        for (int i = 0; i < _buttonList.Count; i++)
        {
            if (i + 1 < levelReached)
                _buttonList[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        GameManager.Instance.LoadLevel(levelName);
    }
}
