using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이동 축을 선택하는 이넘타입
public enum MovingAxis
{
    X,
    Y,
    Z
}
public class MovingBlock : MonoBehaviour
{
    
    [SerializeField]  private MovingAxis axis;  //이동 축
    private float start;    //시작 지점
    private float move;     //이동 중
    [SerializeField] private float movingSpeed; //이동 속도
    [SerializeField] private float movingDistance;  //이동 거리
    private Vector3 position;   //transfrom.position 읽기 쉽게
    private void Start()
    {
        SettingAxis();
    }

    private void FixedUpdate()
    {
        MoveBlock();
    }

    //이동 축을 선택하는 메서드
    void SettingAxis()
    {
        position = transform.position;
        switch (axis)
        {
            case MovingAxis.X :
                start = position.x;
                break;
            case MovingAxis.Y:
                start = position.y;
                break;
            case MovingAxis.Z:
                start = position.z;
                break;
        }
    }

    
    //블록을 이동 시키는 메서드
    void MoveBlock()
    {
        move += Time.deltaTime * movingSpeed;
        float moveTransition = Mathf.PingPong(move, movingDistance) + start;

        switch (axis)
        {
            case MovingAxis.X :
                transform.position = new Vector3(moveTransition, position.y, position.z);
                break;
            case MovingAxis.Y:
                transform.position = new Vector3(position.x, moveTransition, position.z);
                break;
            case MovingAxis.Z:
                transform.position = new Vector3(position.x, position.y, moveTransition);
                break;
        }
    }

    //자신의 좌표보다 위에 플레이어가 오면 자식으로(자식 오브젝트가 되면 부모 오브젝트와 좌표를 공유)
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.transform.position.y > transform.position.y)
        {
            other.transform.parent = gameObject.transform;
        }
    }

    //탈출 시 부모 비우기
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
