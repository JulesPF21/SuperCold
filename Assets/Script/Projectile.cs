using UnityEngine;
using Unity.FPS.Game; 


namespace Unity.FPS.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        
        [Header("Paramètres généraux")]
        public float life = 1000f;
        public float speed = 20f;
        public float damage = 25f;
        
        
        public GameObject impactVFX; 
        public float impactVFXLifetime = 2f;
    
        private Vector3 m_Velocity;

        private Rigidbody rb;
        private void Awake()
        {
            Destroy(gameObject, life);
            rb = GetComponent<Rigidbody>();
        }
    
        private void OnEnable()
        {
            m_Velocity = transform.forward * speed;
        }
    
        void FixedUpdate()
        {
            
            if(rb.isKinematic)
                return;
            
            rb.linearVelocity = m_Velocity;
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(gameObject.tag))
                return;
            
            Damageable target = collision.collider.GetComponent<Damageable>();
            if (target != null)
            {
                target.InflictDamage(damage, false, gameObject);
            }
            
            if (impactVFX != null)
            {
                GameObject vfx = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                Destroy(vfx, impactVFXLifetime);
            }
            
            Destroy(gameObject);
        }
}


}