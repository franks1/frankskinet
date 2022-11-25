using System.Security.Claims;
using Api.Dto;
using Api.Errors;
using Api.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager,IMapper mapper,
        SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpGet(), Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userInfo =await _userManager.FindUserFromClaimsPrincipal(User);
        var userdto = new UserDto()
        {
            Displayname = userInfo.DisplayName,
            Email = userInfo.Email, Token = this._tokenService.CreateToken(userInfo)
        };
        return Ok(userdto);
    }

    [HttpGet("emailexist")]
    public async Task<ActionResult<bool>> CheckEmailAddressExist([FromQuery] string email)
    {
        return await this._userManager.FindByEmailAsync(email) != null;
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<IActionResult> GetUserAddress()
    {
        var userInfo =await _userManager.FindUserWithAddress(User);
        var addressDto = this._mapper.Map<AddressDto>(userInfo.Address);
        return Ok(addressDto);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<IActionResult> UpdateUserAddress([FromBody] Address addressDto)
    {
        var userInfo =await this._userManager.FindUserWithAddress(User);
        var address = userInfo.Address;
        this._mapper.
            Map(addressDto, address,
                typeof(AddressDto), typeof(Address));
        userInfo.Address = address;
        await this._userManager.UpdateAsync(userInfo);
        return Ok(addressDto);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var user = await this._userManager.FindByEmailAsync(login.Email);
        if (user is null) return Unauthorized(new ApiResponse(401));

        var result = await this._signInManager.CheckPasswordSignInAsync(user, login.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        var loggedinuser = new UserDto()
        {
            Email = user.Email, Displayname = user.UserName,
            Token = this._tokenService.CreateToken(user)
        };
        return Ok(loggedinuser);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        var newuser = new AppUser()
        {
            Email = register.Email,
            DisplayName = register.DisplayName,
            UserName = register.Email,
        };
        var result = await this._userManager.CreateAsync(newuser, register.Password);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        var registered = new UserDto()
        {
            Displayname = register.DisplayName,
            Email = register.Email, Token = this._tokenService.CreateToken(newuser)
        };
        return Ok(registered);
    }
}