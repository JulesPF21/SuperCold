using System.Collections;
using Interface;
using UnityEngine;

public class MoovingTarget : MonoBehaviour, IFreezable
{
    
    public Rigidbody RigidBody { get; private set; }
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
            time += Time.deltaTime * speed;
            float x = Mathf.PingPong(time, amplitude * 2f) - amplitude;
            transform.position = startPos + new Vector3(x, 0f, 0f);
        }
    
}
    private void Awake()
    {
        CanFreeze = true;
        RigidBody = GetComponent<Rigidbody>();
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
