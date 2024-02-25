using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SliderSO", menuName = "ScriptableObjects/SliderUI")]
public class StaminaManager : ScriptableObject {
	public int value;
    public int maxValue;
}