namespace Rsp.Service.Application.Constants;

public static class ProjectRecordConstants
{
    public const string ChiefInvestigatorFirstName = "IQA0309";
    public const string ChiefInvestigatorLastName = "IQA0310";
    public const string ChiefInvestigator = "IQA0311";
    public const string LeadNation = "IQA0005";
    public const string ParticipatingNation = "IQA0032";
    public const string ShortProjectTitle = "IQA0002";
    public const string SponsorOrganisation = "IQA0312";

    public const string ParticipatingOrgs = "OPT0318";
    public const string MajorModificationAreaOfChange = "OPT0319";
    public const string MinorModificationAreaOfChange = "OPT0321";

    public static Dictionary<string, string> NationIdMap { get; set; } = new()
    {
        { "OPT0018", "England" },
        { "OPT0019", "Northern Ireland" },
        { "OPT0020", "Scotland" },
        { "OPT0021", "Wales" },
    };
}