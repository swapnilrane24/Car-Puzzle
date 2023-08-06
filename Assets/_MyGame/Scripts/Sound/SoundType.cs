using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    [CreateAssetMenu(fileName = "SoundType", menuName = "Curio/SoundType")]
    public class SoundType : ScriptableObject
    {
        [SerializeField] private SoundCategory soundCategory;

        public SoundCategory SoundCategory { get => soundCategory; }
    }
}