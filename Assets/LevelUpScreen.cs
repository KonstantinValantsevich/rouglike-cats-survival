using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    private List<string> abillitiesToChoose;
    public event Action<string> AbilitieChosen = delegate { };
    public List<TextMeshProUGUI> buttons;

    public void Initialize(List<string> abilitiesNames)
    {
        abillitiesToChoose = abilitiesNames;
        for (var i = 0; i < 4; i++) {
            buttons[i].text = abilitiesNames[i];
        }
        gameObject.SetActive(true);
    }

    [UsedImplicitly]
    public void ChooseAbility(int ability)
    {
        Time.timeScale = 1;
        AbilitieChosen.Invoke(abillitiesToChoose[ability]);
        gameObject.SetActive(false);
    }
}