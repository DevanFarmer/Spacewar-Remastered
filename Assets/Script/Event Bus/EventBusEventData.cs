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
    public readonly struct OnBossEntered { }

    public readonly struct OnBossDeathLocationReached : IReachedLocationEvent 
    {
        public readonly int bossDeathState;

        public OnBossDeathLocationReached(int _bossDeathState)
        {
            bossDeathState = _bossDeathState;
        }
    }

    public readonly struct OnBossDefeated 
    {
        public readonly int scoreGain;

        public OnBossDefeated(int _scoreGain)
        {
            scoreGain = _scoreGain;
        }
    }
    #endregion
}