using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/26
/// ���������� : 
/// ���� : 
/// 
/// SceneInformation �� ���� ����ڰ� ������ �ӹ����� �����ε��ư����ϴ� Ŭ����
/// </summary>
public class GoBackToPreviousScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneMover.MoveTo(SceneInformation.oldSceneName);
    }

}
