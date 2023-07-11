using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
	public GetAuthorDetailQueryValidator()
	{
		RuleFor(query => query.AuthorId).NotNull().GreaterThan(0);
	}
}
}