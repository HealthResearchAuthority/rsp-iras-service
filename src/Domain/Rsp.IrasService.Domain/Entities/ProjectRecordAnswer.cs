using Rsp.Service.Domain.Interfaces;

namespace Rsp.Service.Domain.Entities;

public class ProjectRecordAnswer : ProjectRecordAnswerBase, ICreatable, IUpdatable
{
    public ProjectRecord? ProjectRecord { get; set; }
}