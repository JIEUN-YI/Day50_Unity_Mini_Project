using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurdle들의 ObjectPool을 설정하는 기능
public class HurdlePool : MonoBehaviour
{
    // 장애물의 프리팹 종류별로 각 10개씩 가지는 HurdleObj 타입의 리스트를 생성
    [SerializeField] public List<HurdleObj> hurdlePools = new List<HurdleObj>();
    private float size = 10;
    [SerializeField] HurdleObj prefab; // 생성할 프리펩

    [Header("HurdleController.cs")]
    [SerializeField] Transform makePoint; // 생성 위치

    // 게임을 시작하면 list를 순회하면서 프리팹으로 인스턴스를 생성함
    private void Awake()
    {
        for(int i = 0; i<size; i++)
        {
            HurdleObj hurdle = Instantiate(prefab);
            hurdle.gameObject.SetActive(false); // 생성 인스턴스 비활성화
            hurdle.transform.parent = transform;
            hurdlePools.Add(hurdle); // 리스트에 인스턴스 저장
        }
        makePoint = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().makePoint; // HurdleController의 makePoint 불러오기
    }

    public HurdleObj MakeHurdlePool(Vector2 position, List<HurdleObj> hurdlePool)
    {
    // 리스트의 용량이 0 이상일때 리스트에 저장한 장애물을 출력
        if(hurdlePool.Count > 0)
        {
            HurdleObj makedHurdle = hurdlePool[hurdlePool.Count - 1];
            makedHurdle.transform.position = position;
            makedHurdle.transform.parent = null;
            makedHurdle.hurdlePool = this;
            return makedHurdle;
        }
    // 리스트의 용량이 0 이하인 경우 인스턴스 생성하여 반환 - 가능한 리스트에서 사용할 수 있도록 리스트의 용량을 조절할 것
        else
        {
            return Instantiate(prefab);
        }
    }

    // 리스트의 반환 함수
    public void ReturnHurdlePool(HurdleObj hurdle, List<HurdleObj> returnPool)
    {
        hurdle.gameObject.SetActive (false);
        hurdle.transform.parent = makePoint;
        returnPool.Add(hurdle);
    }

}
