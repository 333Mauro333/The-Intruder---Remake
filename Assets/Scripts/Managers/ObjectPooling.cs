using System.Collections.Generic;

using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int elementAmount;

    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] List<GameObject> elementsCreated;

    static ObjectPooling instance;


	void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

	void Start()
    {
        for (int i = 0; i < elementAmount; i++)
        {
            GameObject instanceCreated = Instantiate(prefab);

			instanceCreated.SetActive(false);
            elementsCreated.Add(instanceCreated);
        }
    }


    
    public static ObjectPooling GetInstance()
    {
        return instance;
    }
    public GameObject GetElement()
    {
        GameObject elementToReturn = null;

        for (int i = 0; i < elementsCreated.Count; i++)
        {
            if (!elementsCreated[i].activeSelf)
            {
                elementToReturn = elementsCreated[i];
			}
        }

        if (elementToReturn == null)
        {
            elementToReturn = Instantiate(prefab);
            elementToReturn.SetActive(false);

            elementsCreated.Add(elementToReturn);
        }

        return elementToReturn;
    }
}
