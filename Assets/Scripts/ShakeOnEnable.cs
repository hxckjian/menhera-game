using UnityEngine;
using Unity.Cinemachine;

public class ShakeOnEnable : MonoBehaviour
{
    CinemachineImpulseSource impulse;
    void Awake() => impulse = GetComponent<CinemachineImpulseSource>();
    void OnEnable() => impulse.GenerateImpulse();
}
