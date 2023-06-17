using System;

namespace Codebase.Logic.Entity.EnemyEntities.Catch
{
    public interface ICatchable 
    {
        bool IsCaught { get; }
        int CurrentAmountToCatch { get; }
        float MaxTimeToWaitCaught { get; }
        float CurrentTimeToWaitCaught { get; }
        void Catch();
    }
}