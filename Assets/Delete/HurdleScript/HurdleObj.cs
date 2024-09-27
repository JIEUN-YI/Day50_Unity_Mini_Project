using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object Pool에서 빌려온 Hurdle에 대해 반환을 지정
public class HurdleObj : MonoBehaviour
{
    public HurdlePool hurdlePool; // HurdlePool.cs 연동

    // 각 장애물 프리팹 별로 Object Pool을 선언
    [SerializeField] public List<HurdleObj> SmallJumpHurdlePool;
    [SerializeField] public List<HurdleObj> BigJumpHurdlePool;
    [SerializeField] public List<HurdleObj> TopHurdlePool;

    [Header("HurdleController.cs")]
    List<HurdleObj> returnPool; // 반환할 리스트
    HurdleObj returnObj; // 반환할 장애물


    // 객체 활성화시 HerdleController에서 생성한 HurdleObj와 list를 가져와서 저장
    private void OnEnable()
    {
        returnObj = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().nowMakedHurdle;
        returnPool = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().nowMakedHurdlePool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    // 생성한 Hurdle객체가 DeleteZone 태그의 콜라이더와 충돌한 경우 회수 함수 시작
        if (collision.collider.tag == "DeleteZone")
        {
            ReturnHurdle(returnObj, returnPool);
        }
    }


    /// <summary>
    /// 오브젝트 회수 함수 작성
    /// </summary>
    /// <param name="returnHurdleObj"></param>
    /// <param name="returnHurdlePool"></param>
    public void ReturnHurdle(HurdleObj returnHurdleObj, List<HurdleObj> returnHurdlePool)
    {
        hurdlePool.ReturnHurdlePool(returnHurdleObj, returnHurdlePool);
    }
}
