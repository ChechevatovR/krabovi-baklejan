using System;
using DefaultNamespace;
using UnityEngine;

    public class FishMover : MonoBehaviour
    {
        public FishCreator fc;
        
        private void FixedUpdate()
        {
            transform.position += transform.rotation * Vector3.forward * .01f;
            
            if (transform.position.magnitude >= 100)
            {
                fc.Create();
                Destroy(gameObject);
            }
        }
    }