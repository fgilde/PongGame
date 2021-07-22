using UnityEngine;
using Assets.Scripts._base;

public class MiddleLineBehavior : UnityBaseBehaviour
{
    public Material ActiveMaterial;
    public Material InactiveMaterial;
    public float Probably = 0.3f;
    public bool IsActive = false;
    public float Interval = 1.2f;

    public override void Start()
    {
        if(IsActive)
            InvokeRepeating(() => UpdateEnabled(), Interval, Interval);        
        else
            SetEnabled(false);
    }

    private void UpdateEnabled()
    {
        SetEnabled(IsActive && Random.value <= Probably);
    }

    public void SetEnabled(bool isEnabled)
    {
        GetComponent<BoxCollider2D>().enabled = isEnabled;
        GetComponent<Renderer>().sharedMaterial = isEnabled ? ActiveMaterial : InactiveMaterial;
    }

}
