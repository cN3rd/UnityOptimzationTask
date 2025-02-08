using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AssetReference sectorAsset;
    [SerializeField] private Transform targetTransform;

    private GameObject loadedSector;
    public void LoadAndGenerateSector()
    {
        AsyncOperationHandle<GameObject> asyncOperation = Addressables.InstantiateAsync(sectorAsset, targetTransform.position, Quaternion.identity);
        asyncOperation.Completed +=  AsyncOperationOnCompleted;
    }
    
    public void GenerateSector()
    {
        AsyncOperationHandle<GameObject> asyncOperation = Addressables.InstantiateAsync(loadedSector, targetTransform.position, Quaternion.identity);
        asyncOperation.Completed +=  AsyncOperationOnCompleted;
    }

    private void AsyncOperationOnCompleted(AsyncOperationHandle<GameObject> obj)
    {
       Debug.Log("Instntiate");
    }

    public void LoadSector()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(sectorAsset);
        asyncOperationHandle.Completed +=  LoadAsyncComplete;
    }

    private void LoadAsyncComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
       Debug.Log("Loading complete!");
       loadedSector = asyncOperationHandle.Result;
    }
}
