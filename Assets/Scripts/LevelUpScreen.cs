using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    private List<string> abillitiesToChoose;
    public event Action<string> AbilityChosen = delegate { };
    public List<TextMeshProUGUI> buttons;

    public void Initialize(List<string> abilitiesNames)
    {
        for (int i = 0; i < 4; i++) {
            buttons[i].transform.parent.gameObject.SetActive(false);
        }
        abillitiesToChoose = abilitiesNames;
        for (var i = 0; i < abilitiesNames.Count; i++) {
            buttons[i].text = abilitiesNames[i];
            buttons[i].transform.parent.gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    [UsedImplicitly]
    public void ChooseAbility(int ability)
    {
        Time.timeScale = 1;
        AbilityChosen.Invoke(abillitiesToChoose[ability]);
        gameObject.SetActive(false);
    }
}