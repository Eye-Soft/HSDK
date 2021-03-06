namespace EyeSoft.Domain.Transactional.Repositories.Implementations
{
    using Aggregates;

    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IAggregate
	{
		private readonly IRepository<T> repository;

		protected internal Repository(IRepository<T> repository) : base(repository)
		{
			this.repository = repository;
		}

		public override bool IsReadOnly => false;

        public virtual void Save(T entity)
		{
			repository.Save(entity);
		}

		public virtual bool Remove(T entity)
		{
			repository.Remove(entity);

			return true;
		}

		public void Commit()
		{
			repository.Commit();
		}
	}
}