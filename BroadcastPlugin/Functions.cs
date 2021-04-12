namespace BroadcastPlugin
{
    internal static class Functions
    {
        public static bool IsEnemy(this Exiled.API.Features.Player player, Team target)
        {
            if (player.Role == RoleType.Spectator || player.Role == RoleType.None || player.Team == target)
                return false;
            return target == Team.SCP ||
                   ((player.Team != Team.MTF && player.Team != Team.RSC) || (target != Team.MTF && target != Team.RSC))
                   &&
                   ((player.Team != Team.CDP && player.Team != Team.CHI) || (target != Team.CDP && target != Team.CHI))
                ;
        }
    }
}