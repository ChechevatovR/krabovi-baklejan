using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class FishCreator : MonoBehaviour
    {
        public GameObject fishParent;
        public GameObject fishPrefab;
        public int fishAmount;

        private void Start()
        {
            for (int i = 0; i < fishAmount; i++) Create();
        }

        public void Create()
        {
            Vector3 pos = new Vector3(Random.value * 15 - 30, Random.value * 6 + 4, Random.value * 15 - 30);
            GameObject fish = Instantiate(fishPrefab,
                        pos,
                        Quaternion.Euler(0, (Random.value * 2 - 1) * 180, 0),
                        fishParent.transform);
            fish.GetComponent<FishMover>().fc = this;
        }
    }
}