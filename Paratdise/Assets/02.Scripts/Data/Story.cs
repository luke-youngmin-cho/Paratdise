using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ���� ���� ������. 
/// ���� ��ȹ �������� ���� ������ ����
/// </summary>


//���� �������Ҵ� story Ŭ�����ε� 
//�̰� ����� ���δ��� ���� �̷����·� ������ֽø�ʴϴ� 

//���⼭�� �Ƹ� ��ȭ�� ����Ʈ�� �迭�� �����س��� 

//�߿��ѰŴ� �̰� ȣ���� Ŭ������ ��� �����ü������� ����
//���丮�� ȣ���� Ŭ������ �������� StageManager �� ȣ���Ұſ���
public class Story
{
    public StoryExecuteType executeType;
    public StoryEffectType effect;
    public int stage;
    //public string contents; // ���ڿ��� �Ⱦ��Ŵٴϱ� �̰� �ʿ����
    public Sprite[] sprite; // �̰ɷ� ��ȭ ������� �־������  �ϳ��� ���丮�� ������ְԲ� ���ֽø�ǿ� 
    

}
// ������������
public enum StoryExecuteType
{
    None,
    StartOfStage,
    EndOfStage,
}
// ����ȿ��
public enum StoryEffectType
{
    Idle,
    FadeIn,
    FadeOut,
    Shake,
}
