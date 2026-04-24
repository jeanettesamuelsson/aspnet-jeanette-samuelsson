using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Models;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class MemberRepository(DataContext context) :
    RepositoryBase<Member, string, MemberEntity, DataContext>(context),
    IMemberRepository
{
    public async Task<Member?> GetMemberByUserIdAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set
                .Include(x => x.CurrentMembership)
                .FirstOrDefaultAsync(x => x.UserId == userId, ct);
            return entity is null ? default : ToDomainModel(entity);

        } catch
        {
            throw;
        }
    }

    public string GetUserId(Member model)
    {
        return model.UserId;
    }

    protected override string GetId(Member model)
    {
        return model.Id;
    }


    //update entity
    protected override void ApplyPropertyUpdates(MemberEntity entity, Member model)
    {
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.PhoneNumber = model.PhoneNumber;
        entity.UpdatedAt = model.UpdatedAt;
        entity.ProfileImageUri = model.ProfileImageUri;
        entity.CurrentMembershipId = model.CurrentMembershipId;
    }

    // map to member with rehydrate method
    protected override Member ToDomainModel(MemberEntity entity)
    {
        var model = Member.Rehydrate(
            entity.Id,
            entity.UserId,
            entity.FirstName,
            entity.LastName,
            entity.PhoneNumber,
            entity.ProfileImageUri,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.CurrentMembership != null ? MapMembershipToDomain(entity.CurrentMembership) : null,
            entity.CurrentMembershipId

            );

        return model;
    }

    protected override MemberEntity ToEntity(Member model)
    {
        var entity = new MemberEntity
        {
            Id = model.Id,
            UserId = model.UserId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            ProfileImageUri = model.ProfileImageUri,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
        };

        return entity;
    }

    private Membership MapMembershipToDomain(MembershipEntity entity)
    {
        return Membership.Rehydrate(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Benefits, 
            entity.Price,
            entity.MonthlyClasses
        );
    }
}
