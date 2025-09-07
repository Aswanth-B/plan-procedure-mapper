using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Plans;
using RL.Backend.Commands.Handlers.Users;
using RL.Backend.Exceptions;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;
using System.Numerics;

namespace RL.Backend.UnitTests;

[TestClass]
public class AddUserToPlanProcedureTests
{

    [TestMethod]
    public async Task AddProcedureToPlanTests_PlanProcedureIdNotFound_ReturnsNotFound()
    {
        //Given
        var context = DbContextHelper.CreateContext();
        var sut = new AddUserToPlanProcedureCommandHandler(context);
        var request = new AddUserToPlanProcedureCommand()
        {
            PlanId = 2,
            ProcedureId = 2,
            UserId = 1
        };

        context.Plans.Add(new Plan
        {
            PlanId = 1
        });
        context.Procedures.Add(new Procedure
        {
            ProcedureId = 1
        });
        context.Users.Add(new User
        {
            UserId = 1
        });
        context.PlanProcedures.Add(new PlanProcedure
        {
            ProcedureId = 1,
            PlanId = 1
        });

        await context.SaveChangesAsync();

        //When
        var result = await sut.Handle(request, new CancellationToken());

        //Then
        result.Exception.Should().BeOfType(typeof(NotFoundException));
        result.Succeeded.Should().BeFalse();
    }


    [TestMethod]
    public async Task AddProcedureToPlanTests_AlreadyContainsProcedure_ReturnsSuccess()
    {
        //Given
        var context = DbContextHelper.CreateContext();
        var sut = new AddUserToPlanProcedureCommandHandler(context);
        var request = new AddUserToPlanProcedureCommand()
        {
            PlanId = 1,
            ProcedureId = 1,
            UserId = 1
        };

        context.Plans.Add(new Plan
        {
            PlanId = 1
        });
        context.Procedures.Add(new Procedure
        {
            ProcedureId = 1,
            ProcedureTitle = "Test Procedure"
        });
        context.Users.Add(new User
        {
            UserId = 1
        });
        context.PlanProcedures.Add(new PlanProcedure
        {
            ProcedureId = 1,
            PlanId = 1
        });
        context.PlanProcedureUsers.Add(new PlanProcedureUser
        {
            ProcedureId = 1,
            PlanId = 1,
            UserId = 1
        });
        await context.SaveChangesAsync();

        //When
        var result = await sut.Handle(request, new CancellationToken());

        //Then
        result.Value.Should().BeOfType(typeof(PlanProcedureUserDto));
        result.Succeeded.Should().BeTrue();
    }

    [TestMethod]
    public async Task AddProcedureToPlanTests_DoesntContainsProcedure_ReturnsSuccess()
    {
        //Given
        var context = DbContextHelper.CreateContext();
        var sut = new AddUserToPlanProcedureCommandHandler(context);
        var request = new AddUserToPlanProcedureCommand()
        {
            PlanId = 1,
            ProcedureId = 1,
            UserId = 1
        };

        context.Plans.Add(new Plan
        {
            PlanId = 1
        });
        context.Procedures.Add(new Procedure
        {
            ProcedureId = 1,
            ProcedureTitle = "Test Procedure"
        });
        context.Users.Add(new User
        {
            UserId = 1
        });
        context.PlanProcedures.Add(new PlanProcedure
        {
            ProcedureId = 1,
            PlanId = 1
        });
        await context.SaveChangesAsync();

        //When
        var result = await sut.Handle(request, new CancellationToken());

        //Then
        var dbPlanProcedure = await context.PlanProcedureUsers.FirstOrDefaultAsync(ppu => ppu.PlanId == 1 && ppu.ProcedureId == 1 && ppu.UserId == 1);

        dbPlanProcedure.Should().NotBeNull();

        result.Value.Should().BeOfType(typeof(PlanProcedureUserDto));
        result.Succeeded.Should().BeTrue();
    }
}