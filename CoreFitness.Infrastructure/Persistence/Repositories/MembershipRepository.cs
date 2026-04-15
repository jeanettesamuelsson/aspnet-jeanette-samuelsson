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
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.Price = model.Price;
        entity.MonthlyClasses = model.MonthlyClasses;
        entity.Benefits = model.Benefits;
    }

    protected override string GetId(Membership model)
    {
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {
        return Membership.Rehydrate(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Benefits,
            entity.Price,
            entity.MonthlyClasses);
    }

    protected override MembershipEntity ToEntity(Membership model)
    {
        return new MembershipEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses,
            Benefits = model.Benefits

        };

        
    }

}

