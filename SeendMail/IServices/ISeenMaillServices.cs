namespace SeendMail.IServices
{
	public interface ISeenMaillServices
	{
		public Task<bool> SeenMail(string emailRequest);
	}
}
