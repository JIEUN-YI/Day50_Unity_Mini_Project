using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemGemPool itemGemPool = new ItemGemPool(); // ItemGemPool.cs 연동
    [SerializeField] float itemSpeed;

    [Header("ItemGemPool.cs")]
    [SerializeField] public Transform makeGemPoint;

    [Header("ItemGemObj.cs")]
    public ItemGemObj nowMakeGem; // 지금 생성된 Gem
    public List<ItemGemObj> nowMakeGemPool; // 지금 생성된 Gem의 리스트

    [Header("Item Pool List")]
    [SerializeField] public List<ItemGemObj> makeItemGemPool;

    [Header("Status")]
    [SerializeField] float makeingTime; // 아이템 생성 시간

    [Header("CheckCollision.cs")]
    CheckCollision checkCollision; // CheckCollision.cs 연동
    [SerializeField] bool checkHurdle;
    [SerializeField] bool checkGem;

    Coroutine MakeItemGemRoutin; // Item Gem을 제작하는 코루틴

    private void Awake()
    {
        makeItemGemPool = GameObject.Find("ItemGemPool").GetComponent<ItemGemPool>().itemGemPools;
        checkCollision = GameObject.FindGameObjectWithTag("MakingBoundary").GetComponent<CheckCollision>();
    }

    private void Start()
    {
        if (GameManager.instance.isGameover == false)
        {
            MakeItemGemRoutin = StartCoroutine(MakeItemGemR());
        }
    }
    private void Update()
    {
        if (GameManager.instance.isGameover == true)
        {
            StopCoroutine(MakeItemGemRoutin);
        }
    }

    IEnumerator MakeItemGemR()
    {
        checkHurdle = checkCollision.checkHurdle;
        checkGem = checkCollision.checkGem;
        while (GameManager.instance.isGameover == false)
        {
            if (checkHurdle == false && checkGem == false)
            {
                nowMakeGem = PositionGem(makeItemGemPool);
                MakeGem(nowMakeGem, nowMakeGemPool);
                yield return new WaitForSeconds(makeingTime);
            }
            else if(checkHurdle == true && checkGem == false)
            {
                yield return new WaitUntil(() => false);
            }
        }
    }
    private ItemGemObj PositionGem(List<ItemGemObj> itemGemPools)
    {
        ItemGemObj itemGem;
        float y = Random.Range(-3f, 3f);
        itemGem = itemGemPool.MakeGemPool(new Vector2(makeGemPoint.position.x, y), itemGemPools);
        nowMakeGemPool = itemGemPools;
        return itemGem;

    }
    private void MakeGem(ItemGemObj itemGemObj, List<ItemGemObj> itemGemPools)
    {
        itemGemObj.gameObject.SetActive(true);
        itemGemPools.RemoveAt(itemGemPools.Count - 1);
        ItemGem itemGem = itemGemObj.GetComponent<ItemGem>();
        itemGem.SetSpeed(itemSpeed);
    }
}
