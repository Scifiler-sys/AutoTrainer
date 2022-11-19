using AutoTrainer.Models;
using AutoTrainer.Selenium;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Commands
{
    public class WarnCommand : CommandBase
    {
        private readonly EmailBot _emailBot;

        public WarnCommand(EmailBot emailBot)
        {
            _emailBot = emailBot;
        }

        public override void Execute(object parameter)
        {
            _emailBot.SendEmail(new Associate((AssociateViewModel)parameter), EmailType.Warn);
        }
    }
}
