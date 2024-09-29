﻿namespace SatisServer.UI.Data.Endpoints.EnumerateSessions;

public class EnumerateSessionsResponse
{
    public SessionData[] Sessions { get; set; }
    public int CurrentSessionIndex { get; set; }
    public class SessionData
    {
        public string SessionName { get; set; }
        public SaveHeader[] SaveHeaders { get; set; }
    }
    public class SaveHeader
    {
        public int SaveVersion { get; set; }
        public int BuildVersion { get; set; }
        public string SaveName { get; set; }
        public string SaveLocationInfo { get; set; }
        public string MapName { get; set; }
        public string MapOptions { get; set; }
        public string SessionName { get; set; }
        public int PlayDurationSeconds { get; set; }
        public string SaveDateTime { get; set; }
        public bool IsModdedSave { get; set; }
        public bool IsEditedSave { get; set; }
        public bool IsCreativeModeEnabled { get; set; }
    }
}
