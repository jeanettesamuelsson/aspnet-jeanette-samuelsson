using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Entities;

public class BookingEntity
{
    public string Id { get; set; } = null!;

    public string MemberId { get; set; } = null!; //FK
    public string GymClassId { get; set; } = null!; //FK

    public DateTime BookedAt { get; set; }

    //navigation properties - member and gym class

    public MemberEntity Member { get; set; } = null!; 
    public GymClassEntity GymClass { get; set; } = null!;
}
