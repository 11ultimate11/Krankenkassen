namespace Krankenkassen.Helpers.Interfaces
{
    /// <summary>
    /// Represents a timer processor that triggers an action after a specific interval and wait time.
    /// </summary>
    public interface ITimerProcessor
    {
        /// <summary>
        /// Registrieren Sie sich für den Timer-Ablauf-Ereignisrückruf
        /// </summary>
        event Action TimeExpiredCallback;
        /// <summary>
        /// Starts the timer with the specified wait time.
        /// </summary>
        void Start();
    }
}