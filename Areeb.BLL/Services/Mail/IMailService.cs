﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BLL.Services.Mail
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string eventName, string eventDate);
    }
}
