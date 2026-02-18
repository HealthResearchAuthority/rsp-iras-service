namespace Rsp.Service.Application.Constants;

public struct ModificationStatus
{
    public const string InDraft = "In draft";
    public const string WithSponsor = "With sponsor";
    public const string WithReviewBody = "With review body";
    public const string Approved = "Approved";
    public const string NotApproved = "Not approved";
    public const string NotAuthorised = "Not authorised";

    public const string RequestRevisions = "Request revision";
    public const string ReviseAndAuthorise = "Sponsor revises modification";
    public const string Received = "Received";
    public const string ReviewInProgress = "Review in progress";

    public struct ReviewType
    {
        public const string NoReviewRequired = "No review required";
        public const string ReviewRequired = "Review required";
    }
}