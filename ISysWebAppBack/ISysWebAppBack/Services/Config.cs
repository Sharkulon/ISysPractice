﻿namespace ISysWebAppBack.Services
{
    public class Config
    {
        public const string Project = "Project";

        public static string ConnectionString { get; set; } = String.Empty;
        public static string Title { get; set; } = String.Empty;
        public static string Description { get; set; } = String.Empty;
        public static string Email { get; set; } = String.Empty;
        public static int PageNumber { get; set; }
    }
}
