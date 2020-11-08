using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Respawning;

namespace BroadcastPlugin
{
    public class EventHandlers
    {
        private readonly BroadcastPlugin _pluginInstance;
        public EventHandlers(BroadcastPlugin pluginInstance) => this._pluginInstance = pluginInstance;

        internal void OnRoundStarting()
        {
            if (!_pluginInstance.Config.IsEnabled) return;
            Map.Broadcast(10,"<color=green>라운드가 시작되었습니다.</color>");
            BroadcastPlugin.IsStarted = true;
        }

        internal void OnRoundEnding(RoundEndedEventArgs ev)
        {
            Map.Broadcast(10,"<color=green>라운드가 종료되었습니다.</color>");
            BroadcastPlugin.IsStarted = false;
        }

        internal void OnRoundRestarting()
        {
            BroadcastPlugin.IsStarted = false;
        }

        internal void OnAnnouncingDecontamination(AnnouncingDecontaminationEventArgs ev)
        {
            switch (ev.Id)
            {
                case 0:
                {
                    Map.Broadcast(10,"<color=red>저위험군 격리 절차</color> 실행까지 <color=red>13분</color> 남았습니다.");
                    break;
                }
                case 1:
                {
                    Map.Broadcast(10,"<color=red>저위험군 격리 절차</color> 실행까지 <color=red>10분</color> 남았습니다.");
                    break;
                }
                case 2:
                {
                    Map.Broadcast(10,"<color=red>저위험군 격리 절차</color> 실행까지 <color=red>5분</color> 남았습니다.");
                    break;
                }
                case 3:
                {
                    Map.Broadcast(10,"<color=red>저위험군 격리 절차</color> 실행까지 <color=red>1분</color> 남았습니다.\n저위험군에 남아있는 인원은 신속히 대피하시기 바랍니다.");
                    break;
                }
                case 4:
                {
                    Map.Broadcast(10,"<color=red>저위험군 격리 절차</color> 실행까지 <color=red>30초</color> 남았습니다.\n저위험군에 남아있는 인원은 신속히 대피하시기 바랍니다.");
                    break;
                }
            }
        }

        internal void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            Map.Broadcast(10,"<color=red>저위험군 격리 절차</color>가 시작되었습니다.");
        }

        internal void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam != SpawnableTeamType.ChaosInsurgency) return;
            foreach (var player in Player.List)
            {
                if (player.Team == Team.CDP || player.Team == Team.CHI) player.Broadcast(10,"<color=army_green>혼돈의 반란</color>이 시설 내에 진입했습니다.");
            }
        }

        internal void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            Map.Broadcast(10,$"<color=cyan>기동특무부대 {ev.UnitName}-{ev.UnitNumber}</color>이 시설 내에 진입했습니다.\n재격리 대기 중인 <color=red>SCP</color>개체는 <color=red>{ev.ScpsLeft}</color>마리입니다.");
        }

        internal void OnDied(DiedEventArgs ev)
        {
            Team playerTeam = ev.Target.Team;
            int teamLeft = 0;
            Player lastplayer = null;
            if (playerTeam == Team.CDP || playerTeam == Team.CHI)
            {
                foreach (var player in Player.List)
                {
                    if (player.Team == Team.CDP || player.Team == Team.CHI && player != ev.Target)
                    {
                        teamLeft++;
                        lastplayer = player;
                    }
                }
            } 
            else if (playerTeam == Team.MTF || playerTeam == Team.RSC)
            {
                foreach (var player in Player.List)
                {
                    if (player.Team == Team.MTF || player.Team == Team.RSC && player != ev.Target)
                    {
                        teamLeft++;
                        lastplayer = player;
                    }
                }
            } 
            else if (playerTeam == Team.SCP)
            {
                foreach (var player in Player.List)
                {
                    if (player.Team == Team.SCP && player != ev.Target)
                    {
                        teamLeft++;
                        lastplayer = player;
                    }
                }
            }
            if (teamLeft == 1)
                if (lastplayer != null)
                    lastplayer.Broadcast(10, "당신이 현재 진영의 <color=red>마지막 생존자</color>입니다!");
        }

        internal void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            Map.Broadcast(10,$"<color=red>{ev.Player.Nickname}</color>님이 대퇴골 분쇄기에 진입했습니다.");
        }

        internal void OnGeneratorActivated(GeneratorActivatedEventArgs ev)
        {
            int cur = Generator079.mainGenerator.NetworktotalVoltage + 1;
            if (cur != 5)
            {
                Map.Broadcast(10,$"발전기 <color=red>5</color>개중 <color=red>{cur}</color>개가 작동되었습니다.");
            }
            else
            {
                Map.Broadcast(10,"발전기 <color=red>5</color>개가 모두 작동되었습니다.");
            }
        }

        internal void OnWarheadStarting(StartingEventArgs ev)
        {
            Map.Broadcast(10,"<color=red>알파 탄두</color> 폭파 절차가 발동되었습니다.\n남아있는 인원은 신속히 대피하시기 바랍니다.");
        }

        internal void OnWarheadStopping(StoppingEventArgs ev)
        {
            Map.Broadcast(10,"<color=red>폭파 절차</color>가 취소되었습니다. 시스템을 재가동합니다.");
        }

        internal void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.Team != Team.SCP) return;
            string scpList = "";
            int count = 0;
            foreach (var player in Player.List)
            {
                if (player.Team == Team.SCP)
                {
                    if (count != 0) scpList += " | ";
                    scpList += $"<color=red>{player.Role.ToString()}</color>";
                    count++;
                }
            }
            ev.Player.Broadcast(10,$"이번 라운드의 <color=red>SCP</color> 목록\n{scpList}");
        }
    }
}