using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New Turret", menuName = "Turret")]
public class TurretData : ScriptableObject
{
    // Start is called before the first frame update
    
    public string turretName;
    public Image icon;
    public int ID;
    
}
