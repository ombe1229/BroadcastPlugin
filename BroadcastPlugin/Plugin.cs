﻿using Log = Exiled.API.Features.Log;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using MapEvents = Exiled.Events.Handlers.Map;
using WarheadEvents = Exiled.Events.Handlers.Warhead;
using Features = Exiled.API.Features;

namespace BroadcastPlugin
{
    public class BroadcastPlugin : Features.Plugin<Configs>
    {
        public EventHandlers EventHandlers { get; }

        public BroadcastPlugin()
        {
            EventHandlers = new EventHandlers(this);
        }

        public void LoadEvents()
        {
            ServerEvents.RoundStarted += EventHandlers.OnRoundStarting;
            ServerEvents.RoundEnded += EventHandlers.OnRoundEnding;
            MapEvents.AnnouncingDecontamination += EventHandlers.OnAnnouncingDecontamination;
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

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;
           
            base.OnEnabled();

            try
            {
                LoadEvents();
                Log.Info("브로드캐스트 플러그인 활성화");
            } 
            catch
            {
                // 오류 발생시, Disable 후 다시 throw
                OnDisabled();
                throw;
            }
        }
        
        public override void OnDisabled()
        {
            base.OnDisabled();
            
            ServerEvents.RoundStarted -= EventHandlers.OnRoundStarting;
            ServerEvents.RoundEnded -= EventHandlers.OnRoundEnding;
            MapEvents.AnnouncingDecontamination -= EventHandlers.OnAnnouncingDecontamination;
            MapEvents.Decontaminating -= EventHandlers.OnDecontaminating;
            ServerEvents.RespawningTeam -= EventHandlers.OnRespawningTeam;
            MapEvents.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingNtfEntrance;
            PlayerEvents.Died -= EventHandlers.OnDied;
            PlayerEvents.EnteringFemurBreaker -= EventHandlers.OnEnteringFemurBreaker;
            MapEvents.GeneratorActivated -= EventHandlers.OnGeneratorActivated;
            WarheadEvents.Starting -= EventHandlers.OnWarheadStarting;
            WarheadEvents.Stopping -= EventHandlers.OnWarheadStopping;
            PlayerEvents.Spawning -= EventHandlers.OnSpawning;
        }

        public override void OnReloaded()
        {
            
        }
    }
}