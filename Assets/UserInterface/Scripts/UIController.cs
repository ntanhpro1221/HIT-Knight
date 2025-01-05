namespace sampleUserInterface
{
    public class UIController : Singleton<UIController>
    {
        public UILogin UILogin
        {
            get { return GetComponentInChildren<UILogin>(); }
        }

        public UILobby UILobby
        {
            get { return GetComponentInChildren<UILobby>(); }
        }

        public UISelectHero UISelectHero
        {
            get { return GetComponentInChildren<UISelectHero>(); }
        }

        public UISelectMap UISelectMap
        {
            get { return GetComponentInChildren<UISelectMap>(); }
        }

        public UISelectWeapon UISelectWeapon
        {
            get { return GetComponentInChildren<UISelectWeapon>(); }
        }
    }
}
