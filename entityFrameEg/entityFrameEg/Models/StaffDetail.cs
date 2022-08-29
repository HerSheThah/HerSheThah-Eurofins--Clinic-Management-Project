using System;
using System.Collections.Generic;

namespace entityFrameEg.Models
{
    public partial class StaffDetail
    {
        public string Username { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? UserPassword { get; set; }
    }
}
