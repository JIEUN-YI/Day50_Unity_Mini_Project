using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGemPool : MonoBehaviour
{
    // Gem의 list로 ObjectPool생성
    [SerializeField] public List<ItemGemObj> itemGemPools = new List<ItemGemObj>();
    private float size = 20;
    [SerializeField] ItemGemObj gemPrefab; // GemPrefab 설정
    [SerializeField] Transform makeGemPoint; // 고정된 x값을 가져오는 생성 위치 - y값은 랜덤 출력

    private void Awake()
    {
        for (int i = 0; i<size; i++)
        {
            ItemGemObj itemGem = Instantiate(gemPrefab);
            itemGem.gameObject.SetActive(false);
            itemGem.transform.parent = transform;
            itemGemPools.Add(itemGem);
        }
        makeGemPoint = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemController>().makeGemPoint;
    }

    public ItemGemObj MakeGemPool(Vector2 position, List<ItemGemObj> itemGemPool)
    {
        if(itemGemPool.Count > 0)
        {
            ItemGemObj makedGem = itemGemPool[itemGemPool.Count - 1];
            makedGem.transform.position = position;
            makedGem.transform.parent = null;
            makedGem.itemGemPool = this;
            return makedGem;
        }
        else
        {
            return Instantiate(gemPrefab);
        }
    }

    public void ReturnItemGemPool(ItemGemObj returnItemGem, List<ItemGemObj> returnGemPool)
    {
        returnItemGem.gameObject.SetActive(false);
        returnItemGem.transform.parent = makeGemPoint;
        returnGemPool.Add(returnItemGem);
    }
}
