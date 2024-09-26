using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGemObj : MonoBehaviour
{
    public ItemGemPool itemGemPool; // ItemGemPool.cs����

    [SerializeField] public List<ItemGemObj> itemGemList; // Gem prefab�� ObjectPool

    [Header("ItemController.cs")]
    public ItemGemObj returnGemObj; // ��ȯ�� Gem
    public List<ItemGemObj> returnGemPool; // Gem�� ��ȯ�� ����Ʈ

    private void OnEnable()
    {
        returnGemObj = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemController>().nowMakeGem;
        returnGemPool = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemController>().nowMakeGemPool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������ Hurdle��ü�� DeleteZone �±��� �ݶ��̴��� �浹�� ��� ȸ�� �Լ� ����
        if (collision.collider.tag == "DeleteZone")
        {
            ReturnItemGem(returnGemObj, returnGemPool);
        }
        // Player�� �浹 �� ȸ��
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
