using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class LocalSFXManager : MonoBehaviour
    {
        [Header("Audio Clip List")]
        [SerializeField]
        private AudioClip sfx_Jump;
        [SerializeField]
        private AudioClip sfx_Fall;


        public void PlaySFX_Jump()
        {
            if (sfx_Jump != null)
            {
                MusicEngine.Instance.PlaySFX(sfx_Jump);
            }

        }

        public void PlaySFX_Fall()
        {
            if (sfx_Fall != null)
            {
                MusicEngine.Instance.PlaySFX(sfx_Fall);
            }
        }



    }
}
