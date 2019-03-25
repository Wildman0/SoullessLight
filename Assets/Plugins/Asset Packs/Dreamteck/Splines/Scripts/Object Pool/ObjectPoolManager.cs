using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField]
    List<ObjectPool> objectPools = new List<ObjectPool>();
    Dictionary<GameObject, string> globalPool = new Dictionary<GameObject, string>();

    [SerializeField]
    bool setUpPoolsOnAwake = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);

        if (setUpPoolsOnAwake)
            SetUpPools();
    }

    private void SetUpPools()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            SetUpPoolAt(i);
        }
    }

    private void SetUpPoolAt(int i)
    {
        if(objectPools[i].objectToPool == null)
        {
            Debug.LogError(string.Format("Missing object to pool in {0}", objectPools[i].stringID));
            return;
        }

        objectPools[i].parentTransform = new GameObject(string.Format("[{0}] Pool Parent", objectPools[i].stringID)).transform;
        objectPools[i].parentTransform.parent = transform;

        for (int x = 0; x < objectPools[i].startCount; x++)
        {
            AddNewObjectToPool(i, true);
        }
    }

    private GameObject AddNewObjectToPool(int poolId, bool deactivateOnCreate = false)
    {
        GameObject clone = Instantiate(objectPools[poolId].objectToPool);

        if (deactivateOnCreate)
            DeactivateObject(poolId, clone);

        globalPool.Add(clone, objectPools[poolId].stringID);

        return clone;
    }

    public GameObject CallObject(string stringId, Transform parent, Vector3 position, Quaternion rotation, float decayTime = -1)
    {
        int idToCall = -1;

        for (int i = 0; i < objectPools.Count; i++)
        {
            if (objectPools[i].stringID == stringId)
            {
                idToCall = i;
                break;
            }
        }

        if (idToCall == -1)
            Debug.LogError(string.Format("{0} ID was not found in the object pool please check the object pool data and try again.", stringId));
        else
        {
            GameObject poolItem = (objectPools[idToCall].inactivePool.Count > 0) ? objectPools[idToCall].inactivePool[0] : AddNewObjectToPool(idToCall, objectPools[idToCall].objectToPool);

            ActivateObject(idToCall, poolItem);

            poolItem.transform.SetPositionAndRotation(position, rotation);

            poolItem.transform.parent = parent;

            if (decayTime != -1)
                StartCoroutine(DelayRecall(poolItem, new WaitForSeconds(decayTime)));

            return poolItem;
        }

        return null;
    }

    private IEnumerator DelayRecall(GameObject objectToRecall, WaitForSeconds delay)
    {
        yield return delay;
        RecallObject(objectToRecall);
    }

    public void RecallObject(GameObject objectToRecall)
    {
        string stringIdToRecallTo;
        globalPool.TryGetValue(objectToRecall, out stringIdToRecallTo);

        for (int i = 0; i < objectPools.Count; i++)
        {
            if (objectPools[i].stringID == stringIdToRecallTo)
            {
                DeactivateObject(i, objectToRecall);
                return;
            }
        }

        Debug.LogWarning(string.Format("Could not find recalled object in any pool. {0} will now be deleted", objectToRecall.name));
        Destroy(objectToRecall);
    }

    private void ActivateObject(int poolId, GameObject poolObject)
    {
        poolObject.SetActive(true);

        if (objectPools[poolId].inactivePool.Contains(poolObject))
            objectPools[poolId].inactivePool.Remove(poolObject);

        objectPools[poolId].activePool.Add(poolObject);
    }

    private void DeactivateObject(int poolId, GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (objectPools[poolId].inactivePool.Contains(poolObject))
            objectPools[poolId].activePool.Remove(poolObject);

        poolObject.transform.parent = objectPools[poolId].parentTransform;

        objectPools[poolId].inactivePool.Add(poolObject);
    }
}
