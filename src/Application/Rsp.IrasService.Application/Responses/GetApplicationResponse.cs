﻿namespace Rsp.IrasService.Application.Responses;

public record GetApplicationResponse
{
    /// <summary>
    /// The public key for the application database record
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The title of the project
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The country of the UK where the lead NHS R&amp;D office for the project is located
    /// </summary>
    //public Location? Location { get; set; }

    /// <summary>
    /// The start date of the project
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// A list of applications required for the project
    /// </summary>
    public List<string>? ApplicationCategories { get; set; }

    /// <summary>
    /// The project category
    /// </summary>
    public string? ProjectCategory { get; set; }

    /// <summary>
    /// Application Status
    /// </summary>
    public string? Status { get; set; }
}