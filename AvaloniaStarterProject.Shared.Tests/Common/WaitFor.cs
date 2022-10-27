using System;
using System.Threading.Tasks;

namespace AvaloniaStarterProject.Shared.Tests.Common;

internal class WaitFor
{
    public static async Task<bool> ConditionAsync(Func<bool> condition, int delayMs = 50, int maxAttempts = 20)
    {
        for (var i = 0; i < maxAttempts; i++)
        {
            await Task.Delay(delayMs);

            if (condition())
            {
                return true;
            }
        }

        return false;
    }
}
