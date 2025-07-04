﻿using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;
using Employee.Domain.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Employee.Domain.Services;

public class AdminService : ValidationService, IAdminService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IValidator<AdminRequest.CreateAdminRequest> _createAdminRequestValidator; 

    public AdminService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IValidator<AdminRequest.CreateAdminRequest> createAdminRequestValidator, 
        INotificationService notificationService) : base(notificationService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _createAdminRequestValidator = createAdminRequestValidator;
    }

    public async Task<AdminResponse.CreateAdminResponse?> CreateAdminAsync(AdminRequest.CreateAdminRequest request)
    {
        if (!await ExecuteValidationsAsync(_createAdminRequestValidator, request)) return null;
            
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser != null)
        {
            AddMessage("User already exists");
            return null;
        }

        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = $"{request.Username}@admin.com",
            EmailConfirmed = true
        };
        
        var createdResult = await _userManager.CreateAsync(user, request.Password);
        if (!createdResult.Succeeded)
        {
            foreach (var error in createdResult.Errors)
                AddMessage(error.Description);

            return null;
        }

        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

        await _userManager.AddToRoleAsync(user, "Admin");

        return new AdminResponse.CreateAdminResponse()
        {
            Username = user.UserName,
            CreatedAt = DateTime.Now,
            Message = $"The admin user {user.UserName} has been created successfully!. Email: {user.Email}"
        };
    }
}