using MagicPharm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace MagicPharm.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ApplicationDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}