using System;

public class DataManager : Singleton<DataManager>, IDataManageable {
    public UserData userData;
    public SystemData systemData;

    public void LoadAllData() {
        throw new NotImplementedException();
    }
    public void SaveAllData() {
        throw new NotImplementedException();
    }
}
