using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCallBack : MonoBehaviour
{
    [SerializeField] private GameObject _studioLogo;
    [SerializeField] private Material _onMaterial;
    [SerializeField] private MeshRenderer _battery;
    public void TurnOn()
    {
        StartCoroutine(CrOn());
        IEnumerator CrOn()
        {
            _battery.material = _onMaterial;
            yield return new WaitForSeconds(0.5f);
            _studioLogo.SetActive(true);
            GameEvents.PlaySFX.Invoke(SFXManager.ClipName.Start);
        }
    }
}
