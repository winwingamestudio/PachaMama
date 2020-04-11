using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovmentNote : MonoBehaviour {

        private float RotateSpeed = 3f;
        private float Radius = 0.4f;

        private Vector2 _centre;
        private float _angle;

        private void Start()
        {
            _centre = transform.position;
        }

        private void Update()
        {

            _angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
        }

}
