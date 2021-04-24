using Exiled.API.Enums;
using Log = Exiled.API.Features.Log;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using MapEvents = Exiled.Events.Handlers.Map;
using WarheadEvents = Exiled.Events.Handlers.Warhead;
using Features = Exiled.API.Features;

namespace BroadcastPlugin {
public class BroadcastPlugin : Features.Plugin<Configs> {
  public EventHandlers EventHandlers {
    get;
    private set;
  }

  public override string Name { get; }
  = "BroadcastPlugin";

  public override string Prefix { get; }
  = "BroadcastPlugin";

  // 이름 바꾸기 참기 레밸 500
  public override string Author { get; }
  = "ombe1229";

  public override PluginPriority Priority { get; }
  = PluginPriority.Default;

  public void LoadEvents() {
    ServerEvents.RoundStarted += EventHandlers.OnRoundStarting;
    ServerEvents.RoundEnded += EventHandlers.OnRoundEnding;
    MapEvents.AnnouncingDecontamination +=
        EventHandlers.OnAnnouncingDecontamination;
    MapEvents.Decontaminating += EventHandlers.OnDecontaminating;
    ServerEvents.RespawningTeam += EventHandlers.OnRespawningTeam;
    MapEvents.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingNtfEntrance;
    PlayerEvents.Died += EventHandlers.OnDied;
    PlayerEvents.EnteringFemurBreaker += EventHandlers.OnEnteringFemurBreaker;
    MapEvents.GeneratorActivated += EventHandlers.OnGeneratorActivated;
    WarheadEvents.Starting += EventHandlers.OnWarheadStarting;
    WarheadEvents.Stopping += EventHandlers.OnWarheadStopping;
    PlayerEvents.Spawning += EventHandlers.OnSpawning;
  }

  public override void OnEnabled() {
    if (!Config.IsEnabled)
      return;

    base.OnEnabled();

    EventHandlers = new EventHandlers(this);
    LoadEvents();
    Log.Info("브로드캐스트 플러그인 활성화");
  }

  public override void OnDisabled() {
    base.OnDisabled();

    ServerEvents.RoundStarted -= EventHandlers.OnRoundStarting;
    ServerEvents.RoundEnded -= EventHandlers.OnRoundEnding;
    MapEvents.AnnouncingDecontamination -=
        EventHandlers.OnAnnouncingDecontamination;
    MapEvents.Decontaminating -= EventHandlers.OnDecontaminating;
    ServerEvents.RespawningTeam -= EventHandlers.OnRespawningTeam;
    MapEvents.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingNtfEntrance;
    PlayerEvents.Died -= EventHandlers.OnDied;
    PlayerEvents.EnteringFemurBreaker -= EventHandlers.OnEnteringFemurBreaker;
    MapEvents.GeneratorActivated -= EventHandlers.OnGeneratorActivated;
    WarheadEvents.Starting -= EventHandlers.OnWarheadStarting;
    WarheadEvents.Stopping -= EventHandlers.OnWarheadStopping;
    PlayerEvents.Spawning -= EventHandlers.OnSpawning;
    EventHandlers = null;
  }

  public override void OnReloaded() {}
}
}