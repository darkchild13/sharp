namespace DarkEzreal
{
    using System;

    using DarkEzreal.Main;

    using LeagueSharp.SDK;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap.Init();

            Events.OnLoad += EventsOnOnLoad;
        }

        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {
            Config.Init();
        }
    }
}
