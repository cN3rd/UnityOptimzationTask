using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ArrowHazard : MonoBehaviour
{
    static readonly Quaternion ArrowRotation = Quaternion.Euler(0, 180, 0);
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float shootInterval;
    [SerializeField] float maxDistance = 50f;
    readonly HashSet<(GameObject arrow, Vector3 startPos)> _activeArrows = new();

    ObjectPool<GameObject> _arrowPool;

    void Start()
    {
        InitializeArrowPool();

        StartCoroutine(ShootCoroutine());
        StartCoroutine(TrackArrowsCorotuine());
    }

    IEnumerator TrackArrowsCorotuine()
    {
        var estimatedCapacity = Mathf.CeilToInt(maxDistance / 5f); // a good estimate for now...
        var arrowsToRelease = new List<(GameObject arrow, Vector3 startPos)>(estimatedCapacity);

        while (true)
        {
            arrowsToRelease.Clear();
            foreach (var (arrow, startPos) in _activeArrows)
                if ((startPos - arrow.transform.position).sqrMagnitude >= maxDistance * maxDistance)
                    arrowsToRelease.Add((arrow, startPos));

            foreach (var tuple in arrowsToRelease) _arrowPool.Release(tuple.arrow);

            yield return new WaitForSeconds(0.5f);
        }
    }

    void InitializeArrowPool()
    {
        GameObject CreateArrowFunc()
        {
            return Instantiate(arrowPrefab, transform.position, ArrowRotation);
        }

        void OnGetArrow(GameObject arrow)
        {
            arrow.SetActive(true);
            arrow.transform.position = transform.position;
            _activeArrows.Add((arrow, transform.position));
        }

        void OnReleaseArrow(GameObject arrow)
        {
            arrow.SetActive(false);
            arrow.transform.position = transform.position;
            _activeArrows.RemoveWhere(tuple => tuple.arrow == arrow);
        }

        _arrowPool = new ObjectPool<GameObject>(CreateArrowFunc, OnGetArrow, OnReleaseArrow, Destroy);
    }

    IEnumerator ShootCoroutine()
    {
        var waitForInterval = new WaitForSeconds(shootInterval);
        while (true)
        {
            yield return waitForInterval;

            _arrowPool.Get();
        }
    }
}