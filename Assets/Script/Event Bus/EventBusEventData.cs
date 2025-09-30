using UnityEngine;

namespace EventBusEventData
{
    #region Interfaces
    public interface IReachedLocationEvent { }
    public readonly struct OnLocationReachedDefault : IReachedLocationEvent { }
    #endregion

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
    public readonly struct OnBossDeathLocationReached : IReachedLocationEvent {  }

    public readonly struct OnBossDefeated { }
    #endregion
}