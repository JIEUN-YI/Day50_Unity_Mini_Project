using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGemObj : MonoBehaviour
{
    public ItemGemPool itemGemPool; // ItemGemPool.cs연동

    [SerializeField] public List<ItemGemObj> itemGemList; // Gem prefab의 ObjectPool

    [Header("ItemController.cs")]
    public ItemGemObj returnGemObj; // 반환할 Gem
    public List<ItemGemObj> returnGemPool; // Gem을 반환할 리스트

    private void OnEnable()
    {
        returnGemObj = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemController>().nowMakeGem;
        returnGemPool = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemController>().nowMakeGemPool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 생성한 Hurdle객체가 DeleteZone 태그의 콜라이더와 충돌한 경우 회수 함수 시작
        if (collision.collider.tag == "DeleteZone")
        {
            ReturnItemGem(returnGemObj, returnGemPool);
        }
        // Player와 충돌 시 회수
        else if(collision.collider.tag == "Player")
        {
            ReturnItemGem(returnGemObj, returnGemPool);
        }
    }

    public void ReturnItemGem(ItemGemObj returnItemGem, List<ItemGemObj> returnGemPool)
    {
        itemGemPool.ReturnItemGemPool(returnItemGem, returnGemPool);
    }
}
