using Microsoft.AspNetCore.Authorization;

namespace SecureApp.Authorization
{
	public class HrManagerProbationRequirement : IAuthorizationRequirement
	{
		public HrManagerProbationRequirement(int probationMonths)
		{
			ProbationMonths = probationMonths;
		}

		public int ProbationMonths { get; }
	}
}
