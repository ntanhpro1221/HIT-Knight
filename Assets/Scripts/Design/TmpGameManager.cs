using UnityEngine;

public class TmpGameManager : Singleton<TmpGameManager> {
    /*
     * Spawn hero, set id so it can load stats, weapon, ...
     * Connect hero with controller
     * Attach cinemachine
     */

    public string heroId = "001";
    public string weaponId = "001";
    public GameObject hero;
    public PlayerControllerInBattle controller;

    protected override void Awake() {
        base.Awake();
        //IActor actor = hero.GetComponent<IActor>();
        //TmpUserController controller = this.controller.GetComponent<TmpUserController>();
        //actor.ControlAdapter.BindController(controller);

        controller.Init(hero);
    }
}
