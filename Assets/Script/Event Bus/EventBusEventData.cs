using UnityEngine;

namespace EventBusEventData
{
    #region Game State Events
    public readonly struct OnPauseChanged
    {
        public readonly bool pauseState;

        public OnPauseChanged(bool _pauseState)
        {
            pauseState = _pauseState;
        }
    }
    #endregion

    #region Boss Events
    public readonly struct OnBossDeathLocationReached {  }
    #endregion
}