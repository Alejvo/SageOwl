using Shared;

namespace Domain.Teams;

public class TeamErrors
{
    public static Error NotFound => new("Team.NotFound","The team was not found",ErrorType.NotFound);
    public static Error NoMembersAdded => new("Team.NoMembersAdded", "You have to add members when you create a team", ErrorType.Conflict);
    public static Error NoAdminAdded => new("Team.NoAdminAdded", "You have to add al least one admin when you create a team", ErrorType.Conflict);

}