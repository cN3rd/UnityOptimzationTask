using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    
    [SerializeField] private PlayerCharacterController bobby;
    [SerializeField] private GameObject skillsHolder;
    
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
    }

    private void Update()
    {
        skillsHolder = GameObject.Find("Skills Group");
        SkillButtonUI[] skillsButtonUI = skillsHolder.GetComponentsInChildren<SkillButtonUI>();
        
        for (int i = 0; i < skillsButtonUI.Length; i++)
        {
            skillsButtonUI[i].GetComponent<SkillButtonUI>().skillIcon.sprite = skillsButtonUI[i].skillIcons[i];
            skillsButtonUI[i].GetComponent<SkillButtonUI>().skillNameText.text = "Skill " + (i + 1);
        }
    }
}
