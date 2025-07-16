using MediatR;

namespace CMS.Auth.Features.ResetPassword;

public record ResetPasswordCommand(Guid UserId, string OldPassword, string NewPassword, string ApplyNewPassword) : IRequest<ResetPasswordResponse>;