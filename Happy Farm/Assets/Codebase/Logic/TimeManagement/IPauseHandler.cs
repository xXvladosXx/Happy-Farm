using System;

namespace Codebase.Logic.TimeManagement
{
    public interface IPauseHandler
    {
        void SetPaused(bool isPaused);
    }
}