﻿namespace Shared.Models;
public class LoginResponseModel
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public int? TimeOut { get; set; }
}

