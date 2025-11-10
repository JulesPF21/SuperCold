using System.Collections;
using Interface;
using UnityEngine;

public class WoodenWall : MonoBehaviour, IFlammable, IFreezable
{
    private Vector3 CurrentScale;

    [field: SerializeField] 
    public int TimeToBurn { get; private set; }
    public bool canBurn { get; private set; }
    
    public bool CanFreeze { get; private set; }

    private void Awake()
    {
        CanFreeze = true;
        canBurn = true;
    }
    public void StartBurning()
    {
        CanFreeze = false;
        CurrentScale = transform.localScale;
    }

    public void StopBurning()
    {
        Destroy(gameObject);
        CanFreeze = true;
    }

    public void SetBurnedProgress(float progress)
    {
        gameObject.transform.localScale = CurrentScale * (1-progress);
    }

    public void Freeze()
    {
        canBurn = false;
    }

    public void UnFreeze()
    {
        canBurn = true;
    }
}
