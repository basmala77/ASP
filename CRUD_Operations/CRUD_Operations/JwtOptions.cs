﻿namespace CRUD_Operations
{
    public class JwtOptions
    {
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int lifetime { get; set; }
            public string SigningKey { get; set; }
       

    }
}
