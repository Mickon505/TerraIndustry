using System;
using Terraria.ModLoader;

namespace TerraIndustry.Core.Utilities
{
    /// <summary>
    /// Structured logging utility for the industrial systems.
    /// </summary>
    public static class IndustrialLogger
    {
        private static Mod Mod => ModContent.GetInstance<TerraIndustry>();

        public static void Info(string message)
        {
            Mod?.Logger?.Info($"[TerraIndustry] {message}");
        }

        public static void Warn(string message)
        {
            Mod?.Logger?.Warn($"[TerraIndustry] {message}");
        }

        public static void Error(string message, Exception ex = null)
        {
            if (ex != null)
                Mod?.Logger?.Error($"[TerraIndustry] {message}", ex);
            else
                Mod?.Logger?.Error($"[TerraIndustry] {message}");
        }

        public static void Debug(string message)
        {
            Mod?.Logger?.Debug($"[TerraIndustry] {message}");
        }
    }
}
