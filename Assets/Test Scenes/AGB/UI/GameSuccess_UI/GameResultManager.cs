using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int clearTime;
    private int countKillMonster;
    private int countKillBoss;

    private static GameResultManager _result;
    public static GameResultManager result
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_result)
            {
                _result = FindObjectOfType(typeof(GameResultManager)) as GameResultManager;

                if (_result == null)
                    Debug.Log("no Singleton obj");
            }
            return _result;
        }
    }

    private void Awake()
    {
        if (_result == null)
        {
            _result = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_result != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }
}
