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
    // 생성한 Hurdle객체가 Ground와 충돌하지 않는 경우 회수 함수 시작
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            ReturnHurdle(returnObj, returnPool);
        }
    }

    // 회수 함수 작성
    public void ReturnHurdle(HurdleObj returnHurdleObj, List<HurdleObj> returnHurdlePool)
    {
        hurdlePool.ReturnHurdlePool(returnHurdleObj, returnHurdlePool);
    }
}
