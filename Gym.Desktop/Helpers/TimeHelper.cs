using Gym.Desktop.Constants;
using System;

namespace Gym.Desktop.Helpers;

public class TimeHelper
{
    // All the methods of Helpers should be static

    public static DateTime GetDateTime()
    {
        var dtTime = DateTime.UtcNow;
        dtTime.AddHours(TimeConstants.UTC);
        return dtTime;
    }
}
