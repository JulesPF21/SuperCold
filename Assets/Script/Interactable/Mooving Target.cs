using System.Collections;
using Interface;
using UnityEngine;

public class MoovingTarget : MonoBehaviour, IFreezable
{
    private Rigidbody _rb;

    private float speed = 3f;        
    private float amplitude = 7f;    
    private bool freeze = false;     
    public bool CanFreeze { get; private set; }
    private float time;             
    private Vector3 startPos;       

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!freeze)
        {
            _rb.isKinematic = false;
            time += Time.deltaTime * speed;
            float x = Mathf.PingPong(time, amplitude * 2f) - amplitude;
            transform.position = startPos + new Vector3(x, 0f, 0f);
        }
        else
        {
            _rb.isKinematic = true;
        }
    
}
    private void Awake()
    {
        CanFreeze = true;
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Freeze()
    {
        freeze = true;
    }

    public void UnFreeze()
    {
        freeze = false;
    }

    
}
