using System;

/// <summary>
/// Allow to perform CRUD operations on data
/// </summary>
public class DataManager : Singleton<DataManager>, IDataManager {
    /// <summary>
    /// local data of user
    /// </summary>
    public UserData userData;
    /// <summary>
    /// static data of system (read-only)
    /// </summary>
    public SystemData systemData;

    public void LoadAllData() {
        throw new NotImplementedException();
    }
    public void SaveAllData() {
        throw new NotImplementedException();
    }
}
