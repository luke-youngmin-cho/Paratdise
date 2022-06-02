using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/06
/// 최종수정일 : 
/// 설명 : 
/// 
/// 튜토리얼 실행
/// </summary>
public class TutorialPlayer : MonoBehaviour
{
    public static TutorialPlayer instance;
    [SerializeField] private GameObject tutorialPage;
    [SerializeField] private Text title;
    [SerializeField] private Text discription;
    public List<GameObject> gameObjectsForToggle; // 타이핑 이펙트가 끝나면 활성화/비활성화 시킬 게임오브젝트 리스트
    public bool toggleNormalON; // 이 변수가 true 이면 위의 리스트를 평상시 ON 시키겠다는 뜻.

    [System.Serializable]
    public class Tutorial
    {
        public string title;
        public string description;
    }
    private Tutorial[] tutorials =
        new Tutorial[13]
        {
            new Tutorial()
            {
                title = "조작방법",
                description = "이동 및 굴착\n좌측 하단에 있는 조이스틱으로 캐릭터를 움직일 수 있습니다.\n블록 방향으로 캐릭터를 움직일 시 해당 블록을 부숴 캐릭터가 이동 할 수 있습니다.\n단, 맵에는 파괴불가한 블록(벽)이 존재합니다."
            },
            new Tutorial()
            {
                title = "조작방법",
                description = "공격\n우측 하단에 있는 공격버튼으로 크리처를 공격할 수 있습니다."
            },
            new Tutorial()
            {
                title = "스테이지 클리어",
                description = "스테이지 클리어\n모든 스테이지의 최고 높이까지 올라간 후, 벽 사이의 비어있는 한 칸에 도착하면 클리어됩니다."
            },
            new Tutorial()
            {
                title = "게임 오버",
                description = "수치 0\n제한 시간내에 캐릭터가 탈출하지 못하거나 체력이 0이되면 게임 오버가 됩니다.\n추격 스테이지 제외, 1회에 한정하여 광고 시청 후 부활할 수 있습니다."
            },
            new Tutorial()
            {
                title = "게임 오버",
                description = "패널티\n캐릭터 사망시 가지고 있던 자원들은 모두 사라지게 됩니다.\n(스토리 조각, 엔딩 카드, 시나리오 아이템, 도구 강화 내용은 유지됩니다)"
            },
            new Tutorial()
            {
                title = "아이템",
                description = "타임캡슐\n맵에 랜덤으로 존재하는 아이템입니다.\n타임캡슐 획득 시 체력이 일정량 회복되며 최대 체력량이 증가합니다."
            },
            new Tutorial()
            {
                title = "아이템",
                description = "시나리오 아이템\n게임 내의 시나리오 분기점 선택에 따라 얻을 수 있는 아이템입니다.\n특정 챕터에서 자동으로 활성화 됩니다."
            },
            new Tutorial()
            {
                title = "추위 한도",
                description = "수정동굴 챕터의 스테이지에서는 추위 한도 시스템이 존재합니다.\n수치는 지속적으로 감소되며, 0이 되면 일정 시간동안 얼어붙습니다.\n얼어붙은 후 최대 수치로 다시 차오릅니다.\n특정 블록과 접촉할 시, 수치가 더 빠르게 감소될 수 있습니다."
            },
            new Tutorial()
            {
                title = "추위 한도",
                description = "불꽃 수정\n수정동굴 챕터의 4F 스테이지에서만 등장하는 블록입니다.\n접촉 시 이동 속도가 빨라지고, 추위 한도 수치가 증가합니다."
            },
            new Tutorial()
            {
                title = "정신력",
                description = "잔해더미 챕터의 스테이지에서는 정신력 시스템이 존재합니다.\n수치는 지속적으로 감소되며, 낮아질 수록 유령이 자주 출몰합니다.\n정신력 수치가 0이 되면 절망하게 되고, 일정 시간동안 방향이 반대로 입력됩니다.\n절망 후 정신력은 최대 수치로 다시 차오릅니다.\n특정 블록을 파괴할 시, 수치가 더 빠르게 감소될 수 있습니다."
            },
            new Tutorial()
            {
                title = "도구강화",
                description = "잔해더미 챕터의 스테이지에서는 정신력 시스템이 존재합니다.\n수치는 지속적으로 감소되며, 낮아질 수록 유령이 자주 출몰합니다.\n정신력 수치가 0이 되면 절망하게 되고, 일정 시간동안 방향이 반대로 입력됩니다.\n절망 후 정신력은 최대 수치로 다시 차오릅니다.\n특정 블록을 파괴할 시, 수치가 더 빠르게 감소될 수 있습니다."
            },
            new Tutorial()
            {
                title = "수집",
                description = "스토리 조각\n맵에 랜덤으로 존재하는 타임캡슐을 획득할 시 얻을 수 있는 아이템입니다.\n스토리 조각은 기사, 사진, 쪽지, 글귀, 아이템으로 나뉘어져 있습니다."
            },
            new Tutorial()
            {
                title = "수집",
                description = "엔딩카드\n게임 클리어 시 시나리오 분기점의 선택에 따른 엔딩 카드를 수집할 수 있습니다."
            }
        };


    public void PlayTutorial(int index)
    {
        if (index < 0 || 
            index >= tutorials.Length)
            return;

        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}

