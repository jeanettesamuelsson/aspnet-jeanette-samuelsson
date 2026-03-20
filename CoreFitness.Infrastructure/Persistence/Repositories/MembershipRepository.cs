using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Models;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;


namespace CoreFitness.Infrastructure.Persistence.Repositories;

public sealed class MembershipRepository(DataContext context)
    : RepositoryBase<Membership, string, MembershipEntity, DataContext>(context)
    , IMembershipRepository


{
    protected override void ApplyPropertyUpdates(MembershipEntity entity, Membership model)
    {
        throw new NotImplementedException();
    }

    protected override string GetId(Membership model)
    {
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {

        var benefits = new List<string>();
        foreach (var item in entity.Benefits)
            benefits.Add(item.Benefit);


        var model = Membership.Rehydrate(
            entity.Id,
            entity.Title,
            entity.Description,
            benefits,
            entity.Price,
            entity.MonthlyClasses);

        return model;
    }

    protected override MembershipEntity ToEntity(Membership model)
    {
        var entity = new MembershipEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses,
            Benefits = new List<MembershipBenefitEntity>()

        };

        foreach (var benefit in model.Benefits)
        {
            entity.Benefits.Add(new MembershipBenefitEntity
            {
                Id = Guid.NewGuid().ToString(),
                MembershipId = entity.Id,
                Benefit = benefit
            });
        }

        return entity;
    }

}

