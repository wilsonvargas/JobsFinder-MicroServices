using Events;
using Identity.Api.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace Identity.Api.Messaging.Consumers
{
    public class ApplicantAppliedEventConsumer : IConsumer<ApplicantAppliedEvent>
    {
        private readonly IIdentityRepository _identityRepository;

        public ApplicantAppliedEventConsumer(IIdentityRepository applicantRepository)
        {
            _identityRepository = applicantRepository;
        }


        public async Task Consume(ConsumeContext<ApplicantAppliedEvent> context)
        {
            await _identityRepository.UpdateUserApplicationCount(context.Message.ApplicantId.ToString());
        }
    }
}
