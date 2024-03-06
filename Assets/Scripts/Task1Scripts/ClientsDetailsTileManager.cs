using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ClientsDetailsTileManager : MonoBehaviour
{
    #region Singleton Implementation

    public static ClientsDetailsTileManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameObject clientDetailsPanel;
    public List<DetailsPrefabManager> listOfDetailPrefabs = new List<DetailsPrefabManager>();

    private Tween fadeTween;
    public CanvasGroup clientDetailsCanvasGroup;

    public void LoadPanelData(string name, int points, string address)
    {
        foreach (DetailsPrefabManager prefab in listOfDetailPrefabs)
        {
            switch(prefab.detailType)
            {
                case DetailsPrefabManager.DetailType.NAME:
                    {
                        prefab.SetValueTextDetails(name);
                        break;
                    }
                case DetailsPrefabManager.DetailType.POINTS:
                    {
                        prefab.SetValueTextDetails(points.ToString());
                        break;
                    }
                case DetailsPrefabManager.DetailType.ADDRESS:
                    {
                        prefab.SetValueTextDetails(address);
                        break;
                    }
            }
        }

        clientDetailsPanel.SetActive(true);
        FadeInClientDetailsPanel();
    }

    public void DeactivatePanel()
    {
        clientDetailsPanel.SetActive(false);
    }
    
    public void FadeInClientDetailsPanel()
    {
        Fade(1f,0.5f, EnableInteractivity);
    }

    public void FadeOutClientDetailsPanel()
    {
        Fade(0f, 0.5f, DisableInteractivity);
    }

    public void EnableInteractivity()
    {
        clientDetailsCanvasGroup.interactable = true;
        clientDetailsCanvasGroup.blocksRaycasts = true;
    }

    public void DisableInteractivity()
    {
        clientDetailsCanvasGroup.interactable = false;
        clientDetailsCanvasGroup.blocksRaycasts = false;
        DeactivatePanel();
    }

    public void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
            fadeTween.Kill(false);

        fadeTween = clientDetailsCanvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete = onEnd;
    }

}