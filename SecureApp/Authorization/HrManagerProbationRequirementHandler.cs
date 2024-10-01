using Microsoft.AspNetCore.Authorization;

namespace SecureApp.Authorization
{
	public class HrManagerProbationRequirementHandler : AuthorizationHandler<HrManagerProbationRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HrManagerProbationRequirement requirement)
		{
			if (DateTime.TryParse(context.User.FindFirst(x => x.Type == "StartedDate")?.Value, out DateTime startedDate)) {
				var period = DateTime.Now - startedDate;
				if (period.Days * 30 > requirement.ProbationMonths)
				{
					context.Succeed(requirement);
				}
			}

			return Task.CompletedTask;
		}
	}
}
