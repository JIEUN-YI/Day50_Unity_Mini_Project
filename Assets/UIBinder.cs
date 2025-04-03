using System.Collections.Generic;
using UnityEngine;

// UIBinder를 사용하여 각 씬의 UI를
// 인스펙터 상에서 참조하여 관리하도록 함
public class UIBinder : MonoBehaviour
{
    // 이름(string)으로 모든 GameObject를 저장하여 미리 묶어 저장하는 gameObjectDic을 설정
    private Dictionary<string, GameObject> gameObjectDic;
    // 이름(string)으로 모든 Component를 저장하며 미리 묶어 저장하는 componentDic을 설정
    private Dictionary<string, Component> componentDic;

    protected virtual void Awake()
    {
        Bind(); // Binding을 진행
    }

    private void Bind()
    {
        // gameObjectDic를 제작하는 경우
        // 현재 GameObject의 자식들 중에서 Transform을 기준으로 가져오기
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);
        foreach (Transform t in transforms)
        {
            // GameObject의 이름과 GameObject를 저장
            gameObjectDic.TryAdd(t.gameObject.name, t.gameObject); // 동일한 GameObject의 이름이 존재할 수 있으므로 TryAdd로 추가
        }
        // componentDic을 제작하는 경우
        componentDic = new Dictionary<string, Component>();
    }

    /// <summary>
    /// 이름이 name 인 UI 게임 오브젝트를 가져오는 함수
    /// ex : GetUI("Key01") => Key01 이름의 게임오브젝트 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetUI(in string name)
    {
        gameObjectDic.TryGetValue(name, out GameObject gameObject); // name인 key값이 없을 수 있으므로 TryGetValue()사용
        return gameObject; // name인 key값이 없는 경우 null값 반환
    }
    /// <summary>
    /// 이름이 name인 UI에서 컴포넌트 T 가져오기
    /// ex : GetUI<Image>("Key01") => Key01 이름의 게임오브젝트에서 사용하는 Image 컴포넌트 가져오기
    ///      GetUI<Image>("Key01").sprite => Key01 이름의 게임오브젝트에서 사용하는 Image 컴포넌트의 sprite를 가져오기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetUI<T>(in string name) where T : Component
    {
        // componentDic를 탐방할 key 값을 생성
        string key = $"{name}_{typeof(T).Name}";
        // componentDic에 key값으로 된 component 값이 있는지 확인
        componentDic.TryGetValue(key, out Component component);

        // 1. componentDic에 이미 있는 경우 = 찾아본 적이 있는 경우 : 이미 찾았던 것을 반환
        if (component != null)
        {
            return component as T; //  T 타입으로 반환
        }
        // 2. componentDic에 아직 없는 경우 = 찾아본 적 없는 경우 : 찾은 후 Dictionary에 추가(Binding) 후 반환
        else
        {
            gameObjectDic.TryGetValue(name, out GameObject gameObject);
            if (gameObject == null) // 저장된 이름의 gameObject가 없는 경우 
            {
                return null;
            }
            else
            {
                component = gameObject.GetComponent<T>(); // gameObject의 component를 찾아서
                if (component == null)
                {
                    return null;
                }
                else
                {
                    componentDic.TryAdd(key, component); // componentDic에 key값과 component를 저장 후
                    return component as T; // T 타입으로 반환
                }
            }
        }
    }
}
