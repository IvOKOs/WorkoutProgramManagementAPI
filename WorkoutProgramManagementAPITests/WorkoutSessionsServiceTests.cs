using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutProgramManagementAPI.Services.WorkoutSessions;

namespace WorkoutProgramManagementAPITests
{
    [TestFixture]
    public class WorkoutSessionsServiceTests
    {
        [Test]
        public void StartWorkoutSession_ShouldReturnNotFoundError_IfUserWithGivenIdIsNotFound()
        {
            var sessionServiceMock = new Mock<IWorkoutSessionsService>();
        }
    }
}
