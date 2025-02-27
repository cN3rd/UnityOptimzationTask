using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SkillData
{
    [FormerlySerializedAs("sprite")] public Sprite icon;
    public string name;
}

[CreateAssetMenu(fileName = "SkillData", menuName = "Tiltan Games/Skill Data", order = 1)]
public class SkillDataGroup : ScriptableObject
{
    [FormerlySerializedAs("skillIcons")] public SkillData[] skills;
}