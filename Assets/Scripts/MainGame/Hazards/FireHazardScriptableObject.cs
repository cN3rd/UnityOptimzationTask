using UnityEngine;

[CreateAssetMenu(fileName = "Fire Hazard Data",
    menuName = "Tiltan Games/Obstacles/Fire Hazard Data", order = 0)]
public class FireHazardScriptableObject : ScriptableObject
{ 
    
    [SerializeField] private int minimumDamage;
    [SerializeField] private int maximumDamage;
    
    public int GetRandomFireDamage()
    {
        int randomDamage = Random.Range(minimumDamage, maximumDamage + 1);
        Debug.Log("Random damage is " + randomDamage);
        return randomDamage;
    }
}
