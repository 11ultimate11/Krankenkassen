using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krankenkassen.Helpers.Interfaces;

namespace Krankenkassen.Helpers.Processors;

/// <summary>
/// Represents a timer processor that triggers an action after a specific interval and wait time.
/// </summary>
public class TimerProcessor : ITimerProcessor
{
    /// <summary>
    /// Registrieren Sie sich für den Timer-Ablauf-Ereignisrückruf
    /// </summary>
    public event Action TimeExpiredCallback;
    private int elapsed = 0; // Keeps track of the time elapsed since the last tick.
    private readonly Timer timer; // The timer object responsible for triggering ticks.
    private bool active; // Indicates whether the timer is currently active.

    /// <summary>
    /// Initializes a new instance of the TimerProcessor class.
    /// </summary>
    public TimerProcessor()
    {
        // Create a new timer with the Tick method as the callback, no state object,
        // and an initial delay of 0 milliseconds and a periodic interval defined by AppSettings.Interval.
        timer = new Timer(Tick, null, 0, AppSettings.Interval);
    }

    /// <summary>
    /// The method called by the timer on each tick.
    /// </summary>
    /// <param name="obj">An optional state object (not used in this case).</param>
    private void Tick(object obj)
    {
        if (active)
        {
            elapsed -= AppSettings.Interval; // Decrease the elapsed time by the interval.

            // If the elapsed time has reached or exceeded 0.
            if (elapsed <= 0)
            {
                active = false; // Deactivate the timer.
                TimeExpiredCallback?.Invoke(); // Invoke the callback.
            }
        }
    }

    /// <summary>
    /// Starts the timer with the specified wait time.
    /// </summary>
    public void Start()
    {
        elapsed = AppSettings.WaitTime; // Set the elapsed time to the configured wait time.
        active = true; // Activate the timer.
    }
}
