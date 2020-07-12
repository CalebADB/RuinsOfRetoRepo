using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    public GameObject landingPrefab;
    public GameObject movingTrailPrefab;
    public GameObject hitLeftRightPrefab;

    private Renderer playerRenderer;
    private bool playerDeathAnimation = false;
    private bool playerDeathAnimationStep1 = false;

    private UnityAction deathAnimationFinishCallback = null;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (playerDeathAnimation)
        {
            if (playerDeathAnimationStep1)
            {
                playerRenderer.material.SetFloat("_DeathGlow", playerRenderer.material.GetFloat("_DeathGlow") + Time.deltaTime * 3);
                if (playerRenderer.material.GetFloat("_DeathGlow") >= 5)
                {
                    playerDeathAnimationStep1 = false;
                }
            }
            else
            {
                playerRenderer.material.SetFloat("_Fade", playerRenderer.material.GetFloat("_Fade") - Time.deltaTime * 1);
                if (playerRenderer.material.GetFloat("_Fade") <= 0)
                {
                    deathAnimationFinishCallback?.Invoke();
                    deathAnimationFinishCallback = null;

                    playerDeathAnimation = false;
                    playerDeathAnimationStep1 = true;
                    playerRenderer.material.SetFloat("_DeathGlow", 1);
                    playerRenderer.material.SetFloat("_Fade", 1);
                }
            }
        }
    }

    public void StartEnemyDeathVFX(GameObject playerGameObject, UnityAction callback)
    {
        deathAnimationFinishCallback = callback;
        playerRenderer = playerGameObject.GetComponent<Renderer>();
        playerDeathAnimation = true;
        playerDeathAnimationStep1 = true;
    }

    public void StartHitTopVFX(Vector3 pos)
    {
        Debug.Log("HitTop");
        GameObject landingVFXGO = ObjectPool.Spawn(landingPrefab, pos);
        //landingPrefab.transform.parent = transform;
        landingVFXGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        ParticleSystem ps = landingVFXGO.GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(DespawnOverTime(landingVFXGO, 2f));
    }

    public void StartHitBottomVFX(Vector3 pos, bool isMoving)
    {
        Debug.Log("HitBottom");
        GameObject landingVFXGO = ObjectPool.Spawn((isMoving) ? movingTrailPrefab : landingPrefab, pos);
        //landingPrefab.transform.parent = transform;
        landingVFXGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        ParticleSystem ps = landingVFXGO.GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(DespawnOverTime(landingVFXGO, 2f));
    }

    public void StartHitLeftVFX(Vector3 pos)
    {
        Debug.Log("HitLeft");
        GameObject landingVFXGO = ObjectPool.Spawn(hitLeftRightPrefab, pos);
        //landingPrefab.transform.parent = transform;
        landingVFXGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        ParticleSystem ps = landingVFXGO.GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(DespawnOverTime(landingVFXGO, 2f));
    }

    public void StartHitRightVFX(Vector3 pos)
    {
        Debug.Log("HitRight");
        GameObject landingVFXGO = ObjectPool.Spawn(hitLeftRightPrefab, pos);
        //landingPrefab.transform.parent = transform;
        landingVFXGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        ParticleSystem ps = landingVFXGO.GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(DespawnOverTime(landingVFXGO, 2f));
    }

    private IEnumerator DespawnOverTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Despawn(go);
    }
}
