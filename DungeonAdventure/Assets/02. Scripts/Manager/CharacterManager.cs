using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //싱글톤 패턴
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            // 할당되지 않았을 때, 외부에서 CharacterManager.Instance 로 접근하는 경우
            // 게임 오브젝트를 만들어주고 CharacterManager 스크립트를 AddComponent
            if (_instance == null)
            {   //호출 시 존재하지 않으면 생성
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    private Player _player;

    public Player Player
    {
        get { return _player;}
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
