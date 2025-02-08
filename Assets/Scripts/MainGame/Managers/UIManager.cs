using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    
    [SerializeField] private PlayerCharacterController bobby;
    [SerializeField] GameObject skillButtonGroup;
    [SerializeField] SkillButtonUI[] skillsButtonUI;
    
    public void RefreshHPText(int newHP)
    {
        hpText.text = newHP.ToString();
    }

    private void Awake()
    {
        bobby.onTakeDamageEventAction += RefreshHPText;
    }

    private void Start()
    {
        hpText.text = bobby.Hp.ToString();
        
        for (int i = 0; i < skillsButtonUI.Length; i++)
        {
            skillsButtonUI[i].skillIcon.sprite = skillsButtonUI[i].skillIcons[i];
            skillsButtonUI[i].skillNameText.text = "Skill " + (i + 1);
        }
    }
}
