namespace CoreFitness.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TDomainModel, TId>
    {
        Task AddAsync(TDomainModel model, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TDomainModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TDomainModel?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(TDomainModel model, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TDomainModel model, CancellationToken cancellationToken = default);
    }
}