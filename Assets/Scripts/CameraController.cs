using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(1, 20)]
    private int speed = 10;
    private Transform target;
    private Vector3 offset = Vector3.zero;

    private void Start ()
    {
        target = Player.Instance.transform;
        offset = transform.position - target.position;
	}
	
	private void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
	}
}
