namespace Model.ViewModels.Main.Persisters
{
	using System.Threading.Tasks;

	public abstract class Persister<T> : IPersister<T>
	{
		private readonly IRestClientFactory restClientFactory;

		protected Persister(IRestClientFactory restClientFactory)
		{
			this.restClientFactory = restClientFactory;
		}

		public abstract void Persist(T value);


		public void Persist(object value)
		{
		    Persist((T)value);
		}

		protected void PutAsJson<TCommand>(string requestUri, TCommand command)
		{
			using (var client = restClientFactory.Create())
			{
				client.PutAsJsonAsync(requestUri, command).Wait();
			}

			//Task.Factory.StartNew(
			//	() =>
			//		{
			//		});
		}

	}
}