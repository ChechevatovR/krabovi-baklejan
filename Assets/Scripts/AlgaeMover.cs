using System;
using System.Collections.Generic;
using UnityEngine;

    public class AlgaeMover : MonoBehaviour
    {
        public Vector3 direction = new Vector3(1, 0, 1);
        public float length = 1;
        public float amplitude = 1;
        public float speed = 1;

        private Mesh mesh;
        private Vector3[] verticesInitial;
        private Vector3[] verticesShifted;

        private void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            verticesInitial = mesh.vertices;
            verticesShifted = mesh.vertices;
            direction = transform.rotation * direction * -1f;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < verticesInitial.Length; i++)
            {
                verticesShifted[i] = verticesInitial[i] + direction * (float) (verticesInitial[i].y * Math.Sin(Time.time * speed + length * verticesInitial[i].y) * amplitude);
            }

            mesh.vertices = verticesShifted;
        }
    }